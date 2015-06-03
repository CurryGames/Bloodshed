using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour
{

    private NavMeshAgent agent;
    private EnemyNavMesh enemyNav;
    public enum Death { SHOOTEDGUN, EXPLOITED, SHOOTEDSHOTGUN, SHOOTEDSHOTGUNCLOSE, CARVED }

    public int maxHealth;
    //public Transform blood;
    float brutalPoints;
    float temp = 0.5f;
    float tempIni = 0.5f;
    float tempHit = 0;
    public int currentHealth;
    public float speed;
    public float speedOnChase;
    private PlayerStats playerStats;
    private PlayerShooting playerShooting;
    private RangedEnemy ranged;
    public Death death;
    public GameObject aim;
    public GameObject enemySprite;
    public GameObject blood;
    public GameObject[] deathshotedGun;
    public GameObject[] deathshotedShotgunClose;
    public GameObject[] deathshotedShotgunFar;
    public GameObject[] deathshotedRiffle;
    public GameObject deathExploited;
	public GameObject puntuationText;
    public int doorCounter { get; set; }
    //public AudioClip death;
    public float distanceModifier;
    private DataLogic dataLogic;
	private int puntuation;
    public Color color;
	private Vector3 pushDirection;
    bool alive = true;
    bool down = true;
    bool hit = false;

    private AchievementManager achievementManager;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        color = enemySprite.GetComponent<Renderer>().material.color;
        achievementManager = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<AchievementManager>();
        enemyNav = GetComponent<EnemyNavMesh>();
        ranged = GetComponent<RangedEnemy>();

        speedOnChase = agent.speed;
        speed = 4f;
        maxHealth = 300;
        //currentHealth = maxHealth;
        brutalPoints = 40;
        distanceModifier = 1;

    }

    void Start()
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic"). GetComponent<DataLogic>();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerStats.currentHealth == 0 || playerStats.levelCleared == true) Destroy(this.gameObject);
        if (currentHealth >= maxHealth) currentHealth = maxHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            alive = false;
        }

        if (!alive)
        {
            switch (death)
            {

                case Death.SHOOTEDGUN:
                    Instantiate(deathshotedGun[Random.Range(0, deathshotedGun.GetLength(0))], transform.position, aim.transform.rotation);
                    break;
                case Death.SHOOTEDSHOTGUN:
                    Instantiate(deathshotedShotgunFar[Random.Range(0, deathshotedShotgunFar.GetLength(0))], transform.position, aim.transform.rotation);
                    break;
                case Death.SHOOTEDSHOTGUNCLOSE:
                    Instantiate(deathshotedShotgunClose[Random.Range(0, deathshotedShotgunClose.GetLength(0))], transform.position, aim.transform.rotation);
                    break;
                case Death.EXPLOITED:
                    Instantiate(deathExploited, transform.position, aim.transform.rotation);
                    dataLogic.strike++;
                    achievementManager.SetProgressToAchievement("Strike", (float)dataLogic.strike);
                    break;
                case Death.CARVED:
                    Instantiate(deathshotedGun[Random.Range(0, deathshotedGun.GetLength(0))], transform.position, aim.transform.rotation);
                    break;
            }
            if (playerShooting.weapon != PlayerShooting.Weapon.CHAINSAW) playerStats.currentBrutality += brutalPoints;
            //GameObject bld= (GameObject)Instantiate(blood.gameObject,transform.position,Quaternion.identity);
            //Destroy(bld,2);
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.death, audiSor, dataLogic.volumFx);
            AudioSource audiSor2 = dataLogic.gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.enemyScream[Random.Range(0, dataLogic.enemyScream.Length)], audiSor2, dataLogic.volumFx);
            achievementManager.AddProgressToAchievement("Carnage", 1.0f);
            
			puntuation =  100 * playerStats.multiply;
            playerStats.enemyKill(puntuation);
			GameObject pText = (GameObject)Instantiate(puntuationText, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.Euler(new Vector3 (90, 0, 0)));
			TextMesh punText = pText.GetComponent <TextMesh>();
			punText.text = puntuation.ToString();
			Destroy(pText, 1.5f);

            if(playerStats.multiply == 5)
            {
                playerStats.BrutalMessageInstantiate(playerStats.brutalMessage[Random.Range(0,4)]);
            }
            else if (playerStats.multiply == 8)
            {
                playerStats.BrutalMessageInstantiate(playerStats.brutalMessage[Random.Range(5, 7)]);
            }
            else if (playerStats.multiply == 13)
            {
                playerStats.BrutalMessageInstantiate(playerStats.brutalMessage[8]);
            }

            Destroy(gameObject);
        }
		if (hit) 
		{
			HitAnim ();
			tempHit += Time.deltaTime;
			if (tempHit >= 0.5f) {
					hit = false;
					tempHit = 0;
			}
		} 
		else 
		{
			color = new Color (1, 1, 1, 1);
			enemySprite.GetComponent<Renderer>().material.color = color;
		}
    }

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Bullet"))
        {
            Destroy(col.gameObject);
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            death = Death.SHOOTEDGUN;
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
            dataLogic.Play(dataLogic.hit, audiSor, dataLogic.volumFx);
            GetDamage(100);			
        }

		if ((col.gameObject.tag == "BulletSHOTGUN"))
		{
            if (ranged.dist >= 10)
            {
                distanceModifier = 1;
                death = Death.SHOOTEDGUN;
            }
            else if (ranged.dist <= 1)
            {
                distanceModifier = 2;
                death = Death.SHOOTEDSHOTGUNCLOSE;
            }
            else
            {
                distanceModifier = 1 + ranged.dist * (0.1f);
                death = Death.SHOOTEDSHOTGUN;
            }
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
			Destroy(col.gameObject);
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            //death = Death.SHOOTEDSHOTGUNCLOSE;

            dataLogic.Play(dataLogic.hit, audiSor, dataLogic.volumFx);
			GetDamage((int)(140*distanceModifier));	
		} 

		if ((col.gameObject.tag == "BulletRIFLE"))
		{
			Destroy(col.gameObject);
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
            death = Death.SHOOTEDGUN;
            dataLogic.Play(dataLogic.hit, audiSor, dataLogic.volumFx);
			GetDamage(140);			
		}

        if (col.gameObject.tag == "Chainsaw")
        {
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            death = Death.CARVED;
            dataLogic.Play(dataLogic.hit, audiSor, dataLogic.volumFx);
            GetDamage(500);
        }
    }


    public void GetDamage(int dmg)
    {
        currentHealth -= dmg;
        if (hit == false) hit = true;
        //if (enemyNav.enemyType == EnemyNavMesh.EnemyType.PATROL) enemyNav.enemyType = EnemyNavMesh.EnemyType.CHASE;
        enemyNav.SetChasing();
    }

    void HitAnim()
    {
        if (down)
        {
            color.g = Mathf.Lerp(0F, 1F, temp / tempIni);
            color.b = Mathf.Lerp(0F, 1F, temp / tempIni);
			enemySprite.GetComponent<Renderer>().material.color = color;
            temp -= Time.deltaTime;

            if (temp <= 0)
            {
                down = false;
                temp = 0;
            }
        }

        if (!down)
        {
            color.g = Mathf.Lerp(0F, 1F, temp / tempIni);
            color.b = Mathf.Lerp(0F, 1F, temp / tempIni);
			enemySprite.GetComponent<Renderer>().material.color = color;
            temp += Time.deltaTime;

            if (temp > tempIni)
            {
                down = true;
                temp = tempIni;
            }
        }
    }
}
