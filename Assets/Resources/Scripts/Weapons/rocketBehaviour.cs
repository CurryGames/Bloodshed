using UnityEngine;
using System.Collections;

public class rocketBehaviour : MonoBehaviour {

	public GameObject explosionFX;
	public ShakeCamera m_camera;
	public float speed;
	public float speedMod;
	public float radius = 5.0F;
	public float power = 10.0F;
	public float upwardModifier = 0.0f;
	public ForceMode forceMode;
	private float distanceModifier = 0;
	private DataLogic dataLogic;
	public AudioClip explosionSound;
	AudioSource audiSor;

	// Use this for initialization
	void Start () {
		dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
		m_camera = Camera.main.GetComponent <ShakeCamera>();
		audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, Time.deltaTime * speed);
		speed += speedMod;

	}

	public void OnTriggerEnter(Collider other)
	{
		
		
		// Any case, Destroy bullet if collides with Wall
		if (other.tag == "Wall" || other.tag == "Player") 
		{	
			Instantiate(explosionFX, transform.position, transform.rotation); 
			dataLogic.Play(explosionSound, audiSor, dataLogic.volumFx);
			Explode();
			Shake ();
			Destroy (this.gameObject);
		}
		
		if(other.tag == "ShootableProp")
		{
			ShootableProp destProp = other.GetComponent<ShootableProp>();
			destProp.GetDestroyed();
		}
	}

	void Explode()
	{
		foreach (Collider col in Physics.OverlapSphere( transform.position, radius))
		{
			if (col.GetComponent<Rigidbody>() != null)
			{
				if (col.tag != "Bullet" && col.tag != "enemyBullet")
				{
					col.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, upwardModifier, forceMode);
				}
			}
			
			if(col.tag == "Enemy")
			{
				EnemyStats enemy = col.GetComponent<EnemyStats>();
				distanceModifier = 1 - 1/ (radius / Vector3.Distance (enemy.transform.position, transform.position));
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

	public void Shake()
	{
		if(!m_camera.isShaking)
		{
			m_camera.shakingForce = 0.5F;
			m_camera.shakeDecay = 0.013F;
			m_camera.startShake = true;
		}
	}

}
