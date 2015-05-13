using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {
	
	public float radius = 5.0F;
	public float power = 10.0F;
	public float upwardModifier = 0.0f;
	public ForceMode forceMode;
	private float distanceModifier = 0;
	private float timer;
	public float explosionTime;
	public GameObject grenadeGO;
	public GameObject grenadeFX;
	public GameObject dirtFX;
    private DataLogic dataLogic;
    public AudioClip explosion;
	private ShakeCamera camera;
    AudioSource audiSor;

	void Start() 
	{
		radius = 6.5F;
		explosionTime = 1.5f;
		Invoke ("Explode", explosionTime);  
		Invoke ("PropExplosion", explosionTime);
		Invoke ("Shake", explosionTime);
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();
		camera = Camera.main.GetComponent <ShakeCamera>();
        dataLogic.strike = 0;
        audiSor = dataLogic.gameObject.AddComponent<AudioSource>();

	}

	void Update()
	{


	}


	void Explode()
	{
		foreach (Collider col in Physics.OverlapSphere( transform.position, radius))
		{
			if (col.GetComponent<Rigidbody>() != null)
			{
				if (col.tag != "Bullet" || col.tag != "enemyBullet")
				{
					col.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, upwardModifier, forceMode);
				}
			}
			
			if(col.tag == "Enemy")
			{
				EnemyStats enemy = col.GetComponent<EnemyStats>();
				distanceModifier = 1 - 1/ (radius / Vector3.Distance (enemy.transform.position, transform.position));
                enemy.death = EnemyStats.Death.EXPLOITED;
				enemy.GetDamage(300);
			}
			
			if(col.tag == "Player")
			{
				PlayerStats player = col.GetComponent<PlayerStats>();
				distanceModifier = 1 - 1/(radius / Vector3.Distance (player.transform.position, transform.position));
				player.GetDamage((int)(120 * distanceModifier));
			}

			if(col.tag == "Barrel")
			{
				BarrelExplosion barrel = col.GetComponent<BarrelExplosion>();
				barrel.Explode();
			}

			if(col.tag == "DestructibleProp")
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
		}


	}

	void PropExplosion()
	{
		foreach (Collider col in Physics.OverlapSphere( transform.position, radius))
		{	
			if (col.GetComponent<Rigidbody>() != null)
			{
				if (col.tag == "PropPieces")
				{
					col.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, 0, forceMode);
				}
			}		
		}
		
		Destroy (gameObject);
		GameObject FX = (GameObject) Instantiate(grenadeFX, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler( new Vector3(90, 0, 0)));
		GameObject dirt = (GameObject) Instantiate(dirtFX, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(new Vector3(transform.rotation.x, Random.Range(0, 360), transform.rotation.z)));
		dataLogic.Play(explosion, audiSor, dataLogic.volumFx);
		Destroy (FX, 5);
	}

	private void Shake()
	{
		if(!camera.isShaking)
		{
			camera.shakingForce = 0.5F;
			camera.shakeDecay = 0.05F;
			camera.startShake = true;
		}
	}
}

	







