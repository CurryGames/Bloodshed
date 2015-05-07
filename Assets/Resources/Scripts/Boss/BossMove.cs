using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour {

	private GameObject player;
	private float shootTimer;
	private float throwTimer;
	private float stunTimer;
	private BossStats bossStats;
	public GameObject rocket;
	public GameObject bulletONE;
	public GameObject grenade;
	public float dist;
	public float shootRange = 25f; 
	public float timeBetweenBullets;
	private DataLogic dataLogic;
	public float statesTimer;
	private Rigidbody bossRB;

	Vector3 destination;

	// States
	public bool staticShoot = false;
	private bool aimingPlayer;
	public bool throwingGrenade = false;
	public bool onCharge = false;
	public bool stunt = false;

	// Use this for initialization
	void Awake () {
		aimingPlayer = true;
		player = GameObject.FindWithTag ("Player");
		bossStats = GetComponent<BossStats> ();
		bossStats.stage = BossStats.Stage.ONE;
		dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
		bossRB = GetComponent<Rigidbody> ();


		statesTimer = 0;

	
	}
	
	// Update is called once per frame
	void Update () {
		// Distance between target and enemy
		dist = Vector3.Distance( player.transform.position, transform.position);
		statesTimer += Time.deltaTime;
		if (aimingPlayer)
		{
			if (dist > 1) transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
		}

		if (dist <= shootRange)
		{
			switch (bossStats.stage)
			{
			case BossStats.Stage.ONE:
				if (staticShoot)
				{
					shootRange = 45f;
					shootTimer += Time.deltaTime;
					timeBetweenBullets = 0.1f;
					if (shootTimer >= timeBetweenBullets)
					{
						Shooting();
						AudioSource audiSorc = gameObject.AddComponent<AudioSource>();
						dataLogic.Play(dataLogic.riffle, audiSorc, dataLogic.volumFx);
					}

					if (statesTimer >= 3) 
					{
						staticShoot = false;
						statesTimer = 0;
						GetDir();
					}
				}
				else 
				{
					Relocate ();
					if (statesTimer >= 2) 
					{
						staticShoot = true;
						statesTimer = 0;
					}

				}
				break;
			case BossStats.Stage.TWO:

                if (transform.position != new Vector3(0, transform.position.y, 0)) Relocate();
				shootTimer += Time.deltaTime;
				timeBetweenBullets = 2f;
				if (shootTimer >= timeBetweenBullets)
				{
					Shooting();
					AudioSource audiSorc = gameObject.AddComponent<AudioSource>();
					dataLogic.Play(dataLogic.shootGun, audiSorc, dataLogic.volumFx);
				}

				/*if (throwingGrenade)
				{
					shootTimer += Time.deltaTime;
					timeBetweenBullets = 2f;
					if (shootTimer >= timeBetweenBullets)
					{
						Shooting();
						AudioSource audiSorc = gameObject.AddComponent<AudioSource>();
						dataLogic.Play(dataLogic.shootGun, audiSorc, dataLogic.volumFx);
					}

					if (statesTimer >= 3) 
					{
						throwingGrenade = true;
						statesTimer = 0;
					}
				}
				else 
				{
					Relocate ();
					throwTimer += Time.deltaTime;

					if (throwTimer >= 1.5f)
					{
						ThrowGrenade(20);
						throwTimer = 0;
					}

					if (statesTimer >= 2.5) 
					{
						throwingGrenade = false;
						statesTimer = 0;
						throwTimer = 0;
					}
				}*/
				break;
			case BossStats.Stage.THREE:
				if (onCharge)
				{
					aimingPlayer = false;
					bossRB.AddRelativeForce (Vector3.forward * 500);
				}
				else if (stunt)
				{
					//transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
					stunTimer += Time.deltaTime;
					if (stunTimer >= 2.5) 
					{
						stunt = false;
						stunTimer = 0;
						statesTimer = 0;
						GetDir ();
					}
				}
				else 
				{	
					Relocate ();
					transform.rotation = Quaternion.LookRotation(destination - transform.position);
					if (statesTimer > 3)
					{
						aimingPlayer = true;
						onCharge = true;
						statesTimer = 0;
						GetDir ();
					}
				}
				

				break;
			case BossStats.Stage.DEAD:
				break;
			}
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Wall" && bossStats.stage == BossStats.Stage.THREE && onCharge)
		{
			onCharge = false;
			stunt = true;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player2.0" && bossStats.stage == BossStats.Stage.THREE)
		{
			PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
			playerStats.GetDamage(120);
			onCharge = false;
		}
	}

	void Shooting ()
	{
		// Reset the timer.
		shootTimer = 0f;
	
		switch (bossStats.stage) {
		case BossStats.Stage.ONE:
			GameObject bulletGO = (GameObject)Instantiate (bulletONE, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
			Destroy (bulletGO, 2);
			break;
		case BossStats.Stage.TWO:
			GameObject rocketGO = (GameObject) Instantiate(rocket, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
			Destroy (rocketGO, 4);
			break;
			
		}
	}

	void GetDir()
	{
		float minx, minz, maxz, maxx;

		if (transform.position.x < 0)
		{
			minx = -1*(9 + transform.position.x);
			maxx = -1*(transform.position.x - 9);
		}
		else 
		{
			minx = -1*(transform.position.x + 9);
			maxx = 9 - transform.position.x;
		}

		if (transform.position.z < 0)
		{
			maxz = -1*(transform.position.z - 12);
			minz = -1*(12 + transform.position.z);
		}
		else
		{
			minz = -1*(transform.position.z + 12);
			maxz = 12 - transform.position.z;
		}
		destination = new Vector3 (transform.position.x + Random.Range (minx, maxx), transform.position.y, transform.position.z + Random.Range (minz, maxz));
	}

	void Relocate()
	{
		switch (bossStats.stage)
		{
			case BossStats.Stage.ONE:
			transform.position = Vector3.MoveTowards(transform.position, destination, 9 * Time.deltaTime);

			break;
			case BossStats.Stage.TWO:
			transform.position = Vector3.MoveTowards(transform.position, new Vector3( 0, transform.position.y, 0), 9 * Time.deltaTime);

		 	break;
			case BossStats.Stage.THREE:
			transform.position = Vector3.MoveTowards(transform.position, destination, 9 * Time.deltaTime);

			break;
		}
	}

	public void ThrowGrenade(float force)
	{
		GameObject grenadeGO = (GameObject)Instantiate (grenade, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f), transform.rotation);
		grenadeGO.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * force);
		Physics.IgnoreCollision (grenadeGO.GetComponent<Collider>(), this.GetComponent<Collider>());
	}


    /*
    public void setRun()
    {
        bossAnim.Play("BossRun");
    }

    public void setIddle()
    {
        bossAnim.Play("BossIddle");
    }*/


}
