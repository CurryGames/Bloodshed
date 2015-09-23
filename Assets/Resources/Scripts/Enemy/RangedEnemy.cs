
using UnityEngine;
using System.Collections;

public class RangedEnemy : MonoBehaviour
{

    public float timeBetweenBullets;        // The time between each shot.       
    public GameObject ShotgunBullet;
    public GameObject bullet;
    private GameObject player;
    private EnemyNavMesh enemyMove;
    //private PlayerStats playerStats;
    private DataLogic dataLogic;
    public enum Weapon { RIFLE, SHOTGUN }
    public Weapon weapon;
    public float detectDistance = 50f;
    public float shootRange = 12f;

    float timer;
    public float dist;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        //weapon = Weapon.RIFLE;
        enemyMove = GetComponent<EnemyNavMesh>();
        //playerStats = player.GetComponent<PlayerStats>();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
        GetComponent<DataLogic>();
		//Physics.IgnoreLayerCollision(9, 9, true);
    }

    // Update is called once per frame
    void Update()
    {
        // Distance between target and enemy
        dist = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log (dist);

        if (dist <= detectDistance)
        {
            if (enemyMove.enemyType == EnemyNavMesh.EnemyType.TUTORIAL) enemyMove.changeSprite();
            else enemyMove.chasing = true;
        }

        if ((dist <= shootRange) && (enemyMove.chasing) && enemyMove.OnSight())
        {
            switch (weapon)
            {
                case Weapon.RIFLE:
                    timer += Time.deltaTime;
                    timeBetweenBullets = 0.55f;
                    if (timer >= timeBetweenBullets)
                    {
                        Shooting();
                        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                        dataLogic.Play(dataLogic.enemyGun, audiSor, dataLogic.volumFx);
                    }
                    break;
                case Weapon.SHOTGUN:
                    timer += Time.deltaTime;
                    timeBetweenBullets = 1f;
                    if (timer >= timeBetweenBullets)
                    {
                        Shooting();
                        AudioSource audiSorc = gameObject.AddComponent<AudioSource>();
                        dataLogic.Play(dataLogic.enemyShootGun, audiSorc, dataLogic.volumFx);
                    }
                    break;
            }
        }
    }

    void Shooting()
    {
        // Reset the timer.
        timer = 0f;


        // ... set the second position of the line renderer to the fullest extent of the gun's range.
        switch (weapon)
        {
            case Weapon.RIFLE:
                GameObject bulletGO = (GameObject)Instantiate(bullet, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
                //GameObject bulletGO = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
                Destroy(bulletGO, 2);
                break;
            case Weapon.SHOTGUN:
                GameObject ShotgunBulletGO = (GameObject)Instantiate(ShotgunBullet, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
                Destroy(ShotgunBulletGO, 2);
                break;

        }
    }

}
