using UnityEngine;
using System.Collections;

public class BarrelExplosion : MonoBehaviour {
	
	public float radius = 2.5f;
	public float power = 7.0F;
	public float upwardModifier = 0.0f;
	public ForceMode forceMode;
	private float distanceModifier = 0;
	public GameObject explosionFX;
    private DataLogic dataLogic;
    public AudioClip explosion;
    AudioSource audiSor;

	void Start() 
	{
		//radius = 7.5F;
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
        GetComponent<DataLogic>();
        audiSor = dataLogic.gameObject.AddComponent<AudioSource>();

	}

	/*void Update(){
		if (Input.GetButtonDown(button)){
		foreach (Collider col in Physics.OverlapSphere( transform.position, radius))
		{
			if (col.rigidbody != null)
			{
				col.rigidbody.AddExplosionForce(power, transform.position, radius, upwardModifier,forceMode);
					Debug.Log("booooooooommmm");
			}

			}

		}

	}*/

	public void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Bullet" || other.tag == "BulletSHOTGUN" || other.tag == "BulletRIFLE" || (other.tag == "GatlingBullet"))
		{
            dataLogic.strike = 0;
			Destroy (other.gameObject);
			foreach (Collider col in Physics.OverlapSphere( transform.position, radius))
			{
				if (col.GetComponent<Rigidbody>() != null)
				{
					if (col.tag != "Bullet" && col.tag != "enemyBullet" && col.tag !="BulletSHOTGUN" && col.tag !="BulletRIFLE")
                    {
                        col.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, upwardModifier, forceMode);
                    }
				}
				
                if(col.tag == "Enemy")
                {
                    EnemyStats enemy = col.GetComponent<EnemyStats>();
					distanceModifier = 1 - 1/ (radius / Vector3.Distance (enemy.transform.position, transform.position));
                    enemy.death = EnemyStats.Death.EXPLOITED;
                    enemy.GetDamage(250);
                }

				if(col.tag == "Player")
				{
					PlayerStats player = col.GetComponent<PlayerStats>();
					distanceModifier = 1 - 1/(radius / Vector3.Distance (player.transform.position, transform.position));
					player.GetDamage((int)(120 * distanceModifier));
				}

                if (col.tag == "DestructibleProp")
                {
                    DestructibleProp destProp = col.GetComponent<DestructibleProp>();
                    destProp.GetDestroyed();
                }

                if (col.tag == "ShootableProp")
                {
                    ShootableProp destProp = col.GetComponent<ShootableProp>();
                    destProp.GetDestroyed();
                }

                if (col.tag == "Can")
                {
                    AudioSource audiSor = col.gameObject.AddComponent<AudioSource>();
                    dataLogic.Play(dataLogic.can, audiSor, dataLogic.volumFx);
                }

                if (col.tag == "SecretRoom")
                {
                    SecretRoom destProp = col.GetComponent<SecretRoom>();
                    destProp.ShowRoom();
                } 
			}

			Explode ();
		}
	}

	public void Explode()
	{
        dataLogic.Play(explosion, audiSor, dataLogic.volumFx);
		Destroy (gameObject);
		GameObject FX = (GameObject) Instantiate(explosionFX, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler( new Vector3(90, 0, 0)));
		Destroy (FX, 4);
	}


}





