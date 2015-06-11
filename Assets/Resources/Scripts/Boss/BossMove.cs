using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour {

	private GameObject player;
	private float shootTimer;
	private float throwTimer = 0;
	private float stunTimer;
	private BossStats bossStats;
	private BossCinematic bossCine;
	public GameObject rocket;
	public GameObject bulletONE;
	public GameObject grenade;
    public Animator bossAnim;
	public float dist;
	public float shootRange = 25f; 
	public float timeBetweenBullets;
	private DataLogic dataLogic;
	public float statesTimer;
	private Rigidbody bossRB;
	public bool hasWeapon;

	Vector3 destination;

	// States
	public bool staticShoot = false;
	private bool aimingPlayer;
	public bool throwingGrenade = false;
	public bool onCharge = false;
	public bool stunt = false;
    public bool active;

	// Use this for initialization
	void Awake () {
		aimingPlayer = true;
		player = GameObject.FindWithTag ("Player");
		bossStats = GetComponent<BossStats> ();
		bossCine = GetComponent<BossCinematic> ();
		bossStats.stage = BossStats.Stage.ONE;
		dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
		bossRB = GetComponent<Rigidbody> ();
        hasWeapon = true;
        active = false;
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

		if (active)
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
				if (!hasWeapon)
				{
					throwTimer++;
					ThrowMachinegun();
					if (throwTimer >= 1) 
					{
						Relocate();
						if (transform.position.x == -1 && transform.position.z == 12) hasWeapon = true;
					}
				}
				else
				{
                    Relocate();
	                SetBazooka();
	                if (transform.position != new Vector3(0, transform.position.y, 0)) Relocate();
					shootTimer += Time.deltaTime;
					timeBetweenBullets = 2f;
					if (shootTimer >= timeBetweenBullets)
					{
						Shooting();
						AudioSource audiSorc = gameObject.AddComponent<AudioSource>();
						dataLogic.Play(dataLogic.shootGun, audiSorc, dataLogic.volumFx);
					}
					statesTimer = 0.0f;
				}
                
				break;

			case BossStats.Stage.THREE:
				if (onCharge)
				{
                    SetCharge();
					aimingPlayer = false;
					bossRB.AddRelativeForce (Vector3.forward * 500);
				}
				else if (stunt)
				{
                    SetStoned();
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
                    SetIddle();
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
			case BossStats.Stage.CRAWL:
                SetDead();
				transform.position = Vector3.MoveTowards(transform.position, destination, 1.4f * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(destination - transform.position);
                if (!bossRB.isKinematic) bossRB.isKinematic = true;
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
		if (col.gameObject.name == "Player2.0" && bossStats.stage == BossStats.Stage.THREE && onCharge)
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
			GameObject bulletGO = (GameObject)Instantiate (bulletONE, transform.position , Quaternion.LookRotation(player.transform.position - transform.position));
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
            if (transform.position.x != 0 && transform.position.z != 0)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, 0), 9 * Time.deltaTime);
			/*if (!hasWeapon) Vector3.MoveTowards(transform.position, new Vector3( -1f, transform.position.y, 12), 9 * Time.deltaTime);
			else if (transform.position.x != 0 && transform.position.z != 0 && hasWeapon) 
				transform.position = Vector3.MoveTowards(transform.position, new Vector3( 0, transform.position.y, 0), 9 * Time.deltaTime);*/

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

	public void StartCinematic()
	{
		bossStats.enabled = false;
		bossCine.enabled = true;
		this.GetComponent<BossMove>().enabled = false;
	}

	private void ThrowMachinegun()
	{		 
		SetGetGun ();
		//instatiate gun
	}

    private void SetBazooka()
    {
        bossAnim.Play("BossBazooka");
    }

    private void SetCharge()
    {
        bossAnim.Play("BossCharge");
    }

    private void SetStoned()
    {
        bossAnim.Play("BossStoned");
    }

    private void SetIddle()
    {
        bossAnim.Play("BossIddle");
    }

    private void SetDead()
    {
        bossAnim.Play("Dead");
    }

	private void SetGetGun()
	{
		bossAnim.Play ("BossGetGun");
	}
}
