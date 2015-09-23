using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PlayerShooting : MonoBehaviour
{
    public float timeBetweenBullets;        // The time between each shot.
	public float range = 10f;                      // The distance the gun can fire.
	public GameObject ShotgunBullet;
	private BoxCollider colliderSaw;
	public GameObject bullet;
	public GameObject rifleBullet;
    public GameObject gatlingBullet;
	public GameObject pipe;
    public GameObject chainsaw;
    public GameObject fire;
    public GameObject fireShotgun;
    public GameObject fireGatling;
    private GameObject gunUI, shotgunUI, riffleUI, gatlingUI;
	public Rigidbody grenade;
    private ShakeCamera scamera;
    //private ColorCorrectionCurves colorCorrection;
    public PlayerStats playerStats;
	public PlayerMovement playerMov;
    public GameObject ghost;
    private PauseLogic pauseLogic;
    private DataLogic dataLogic;
    private float clockGunTimer;
    private float timmerGatling;
    private bool clockGun;
    private bool loadGatling;
	float timer;                                    // A timer to determine when to fire.				
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.

	public enum Weapon {GUN, RIFLE, SHOTGUN, CHAINSAW}
	//public enum AttackStates { ATTACK, REST }
	public Weapon weapon;
	//public AttackStates atkState;

	//LineRenderer gunLine;                           // Reference to the line renderer.
                                
	//float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	
	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
        chainsaw.SetActive (false);

	} 
	void Start ()
	{
		colliderSaw = transform.FindChild ("colliderSaw").GetComponent<BoxCollider> ();
		colliderSaw.enabled = false;
        loadGatling = false;
		playerStats = GetComponent <PlayerStats> ();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
		playerMov = GetComponent<PlayerMovement> ();
        pauseLogic = GameObject.FindGameObjectWithTag("pause").GetComponent<PauseLogic>();
        scamera = Camera.main.GetComponent<ShakeCamera>();
        gunUI = GameObject.FindGameObjectWithTag("gunUI");
        shotgunUI = GameObject.FindGameObjectWithTag("shotgunUI");
        riffleUI = GameObject.FindGameObjectWithTag("riffleUI");
        gatlingUI = GameObject.FindGameObjectWithTag("GatlingUI");
        //colorCorrection = Camera.main.GetComponent<ColorCorrectionCurves>();
        //colorCorrection.enabled = false;
        clockGun = false;
	}
	void Update ()
	{
		// Add the time since Update was last called to the timer.

		if(pauseLogic == null) pauseLogic = GameObject.FindGameObjectWithTag("pause").GetComponent<PauseLogic>();

        if ((playerStats.currentBrutality >= 256) && (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("BrutalJoystick")))
        {
            weapon = Weapon.CHAINSAW;
            playerStats.brutalMode = true;
            //colorCorrection.enabled = true;
            if (playerStats.onBoss == false)
            {
                playerStats.audiSorMusic.Pause();
                playerStats.audiSorBrutal.Play();
                //playerStats.audiSorChainsaw.Play();
            }
        }

		switch (weapon) {
            case Weapon.GUN:
                playerStats.setGun();
                gunUI.SetActive(true);
                shotgunUI.SetActive(false);
                riffleUI.SetActive(false);
                gatlingUI.SetActive(false);
                playerStats.bullets.text = "Inf.";
                timer += Time.deltaTime;
                timeBetweenBullets = 0.45f;
                // If the Fire1 button is being press and it's time to fire...
                if ((Input.GetButton("Fire1") || Input.GetAxis("FireJoy") < 0) && timer >= timeBetweenBullets && (playerStats.currentHealth > 0) && pauseLogic.Pause == false)
                {
                    // ... shoot the gun.
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    dataLogic.Play(dataLogic.gun, audiSor, dataLogic.volumFx);
                    //clockGun = true;
                    Shoot();
                    Shake();
                }



                // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
                /*if(timer >= timeBetweenBullets * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }*/
                break;
		case Weapon.RIFLE:
			playerStats.setRiffle ();
                gunUI.SetActive(false);
                shotgunUI.SetActive(false);
                riffleUI.SetActive(true);
                gatlingUI.SetActive(false);
            playerStats.bullets.text = playerStats.riffleBullets.ToString();
			timer += Time.deltaTime;
			timeBetweenBullets = 0.15f;
					// If the Fire1 button is being press and it's time to fire...
            if ((Input.GetButton("Fire1") || Input.GetAxis("FireJoy") < 0) && timer >= timeBetweenBullets && playerStats.riffleBullets > 0 && (playerStats.currentHealth > 0) && pauseLogic.Pause == false)
            {
								// ... shoot the gun.
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                dataLogic.Play(dataLogic.riffle, audiSor, dataLogic.volumFx);
                playerStats.riffleBullets--;
				Shoot ();
                Shake();
			}
            else if ((Input.GetButton("Fire1") || Input.GetAxis("FireJoy") < 0) && timer >= timeBetweenBullets && playerStats.riffleBullets == 0 && (playerStats.currentHealth > 0) && pauseLogic.Pause == false)
            {
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                dataLogic.Play(dataLogic.emptyGun, audiSor, dataLogic.volumFx);
            }
									// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
									/*if(timer >= timeBetweenBullets * effectsDisplayTime)
								{
									// ... disable the effects.
									DisableEffects ();
								}*/
			break;
        case Weapon.CHAINSAW:

            playerStats.bullets.text = "Inf.";
            StartCoroutine(SpawnGhost());
            if (playerStats.currentHealth > 0)
            {
                chainsaw.SetActive(true);
                colliderSaw.enabled = false;
            }
            else
            {
                chainsaw.SetActive(false);
                colliderSaw.enabled = false;
            }
			playerStats.currentBrutality -= 20 * Time.deltaTime;
			playerStats.setChainsaw ();
			playerStats.damage = 1;
            playerStats.speed = 10;
            gunUI.SetActive(false);
            shotgunUI.SetActive(false);
            riffleUI.SetActive(false);
            gatlingUI.SetActive(true);
            if (Input.GetButton("Fire1") || Input.GetAxis("FireJoy") < 0)
            {
                timmerGatling += Time.deltaTime;
                if (!loadGatling)
                {
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    dataLogic.Play(dataLogic.gatlingLoad, audiSor, dataLogic.volumFx);
                    loadGatling = true;
                }

                if (timmerGatling >= dataLogic.gatlingLoad.length)
                {
                    timer += Time.deltaTime;
                    //playerMov.onCharge = true;
                    timeBetweenBullets = 0.15f;
                    if (timer >= timeBetweenBullets)
                    {
                        // ... shoot the gun.
                        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                        dataLogic.Play(dataLogic.gatlingShoot, audiSor, dataLogic.volumFx);
                        Shoot();
                        ShakeShotgun();
                    }
                }
            }
            else if (Input.GetButtonUp("Fire1") || Input.GetAxis("FireJoy") == 0)
            {
                if (loadGatling)
                {
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    dataLogic.Play(dataLogic.gatlingUnload, audiSor, dataLogic.volumFx);
                    timmerGatling = 0;
                    loadGatling = false;
                }
            }
			//else playerMov.onCharge = false;

			if (playerStats.currentBrutality <= 0 || !playerStats.brutalMode)
			{
				chainsaw.SetActive(false);
				colliderSaw.enabled = false;
				weapon = Weapon.GUN;
				playerStats.damage = 12;
				playerStats.speed = 8;
				playerStats.brutalMode = false;
				playerMov.onCharge = false;
                //colorCorrection.enabled = false;
                if (playerStats.onBoss == false)
                {
                    playerStats.audiSorMusic.Play();
                    playerStats.audiSorBrutal.Pause();
                    //playerStats.audiSorChainsaw.Pause();
                }
			}

            break;
		case Weapon.SHOTGUN:
			playerStats.setShootgun ();
            gunUI.SetActive(false);
            shotgunUI.SetActive(true);
            riffleUI.SetActive(false);
            gatlingUI.SetActive(false);
            playerStats.bullets.text = playerStats.shotgunBullets.ToString();
			timer += Time.deltaTime;
			timeBetweenBullets = 0.85f;
			// If the Fire1 button is being press and it's time to fire...
            if ((Input.GetButton("Fire1") || Input.GetAxis("FireJoy") < 0) && timer >= timeBetweenBullets && playerStats.shotgunBullets > 0 && (playerStats.currentHealth > 0) && pauseLogic.Pause == false)
            {
				// ... shoot the gun.
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                dataLogic.Play(dataLogic.shootGun, audiSor, dataLogic.volumFx);
                playerStats.shotgunBullets--;
				Shoot ();
                clockGun = true;
				ShakeShotgun();
			}
            else if ((Input.GetButton("Fire1") || Input.GetAxis("FireJoy") < 0) && timer >= timeBetweenBullets && playerStats.shotgunBullets == 0 && (playerStats.currentHealth > 0) && pauseLogic.Pause == false)
            {
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                dataLogic.Play(dataLogic.emptyGun, audiSor, dataLogic.volumFx);
                timer = 0;
            }

            if (clockGun)
            {
                clockGunTimer += Time.deltaTime;
                if (clockGunTimer >= 0.4f)
                {
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    dataLogic.Play(dataLogic.shootGunClock, audiSor, dataLogic.volumFx);
                    clockGunTimer = 0;
                    clockGun = false;
                }
            }
			break;
		}
	}
	
	/*public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;

	}*/

	void Shoot ()
	{
		// Reset the timer.
		timer = 0f;
		// Enable the line renderer and set it's first position to be the end of the gun.
		//gunLine.enabled = true;
		//gunLine.SetPosition (0, transform.position);
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		
	
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			//gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		switch (weapon) 
		{
		/*case Weapon.MELEE:
			GameObject pipeGO = (GameObject) Instantiate (pipe, transform.position, transform.rotation);
			Destroy (pipeGO, 0.3f);
			break;*/
        case Weapon.GUN:
            GameObject bulletGO = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            //GameObject bullet = (GameObject) Instantiate(bulletPrefab.gameObject, transform.position, transform.rotation);
            Destroy(bulletGO, 2);
            GameObject fr = (GameObject)Instantiate(fire, transform.position, transform.rotation);
            fr.transform.parent = transform;
            //GameObject bullet = (GameObject) Instantiate(bulletPrefab.gameObject, transform.position, transform.rotation);
            Destroy(fr, 0.1f);
            break;
		case Weapon.RIFLE:
			GameObject RifleBulletGO = (GameObject)Instantiate (rifleBullet, transform.position, transform.rotation);
		//GameObject bullet = (GameObject) Instantiate(bulletPrefab.gameObject, transform.position, transform.rotation);
            Destroy(RifleBulletGO, 2);
            GameObject frRf = (GameObject)Instantiate(fire, transform.position, transform.rotation);
            frRf.transform.parent = transform;
            //GameObject bullet = (GameObject) Instantiate(bulletPrefab.gameObject, transform.position, transform.rotation);
            Destroy(frRf, 0.1f);
			break;
        case Weapon.CHAINSAW:
            GameObject GatlingBulletGo = (GameObject)Instantiate(gatlingBullet, transform.position, transform.rotation);
            //GameObject bullet = (GameObject) Instantiate(bulletPrefab.gameObject, transform.position, transform.rotation);
            Destroy(GatlingBulletGo, 2);
            GameObject frGt = (GameObject)Instantiate(fireGatling, transform.position, transform.rotation);
            frGt.transform.parent = transform;
            //GameObject bullet = (GameObject) Instantiate(bulletPrefab.gameObject, transform.position, transform.rotation);
            Destroy(frGt, 0.1f);
            break;
		case Weapon.SHOTGUN:
			GameObject ShotgunBulletGO = (GameObject) Instantiate(ShotgunBullet, transform.position, transform.rotation);
			Destroy (ShotgunBulletGO, 2);
            GameObject frS = (GameObject)Instantiate(fireShotgun, transform.position, transform.rotation);
            frS.transform.parent = transform;
            Destroy(frS, 0.1f);
			break;
				 
		}
	}

	public void ThrowGrenade(float force)
	{
		GameObject grenadeGO = (GameObject)Instantiate (grenade, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f), transform.rotation);
		grenadeGO.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * force);
        Collider gr = grenadeGO.GetComponent<Collider>();
        Collider pl = this.GetComponent<Collider>();
		Physics.IgnoreCollision (gr, pl);
	}

    public void Shake()
    {

        scamera.shakingForce = 0.1F;
        scamera.shakeDecay = 0.05F;
        scamera.startShake = true;
        
    }

	public void ShakeShotgun()
	{
		
		scamera.shakingForce = 0.3F;
		scamera.shakeDecay = 0.05F;
		scamera.startShake = true;
		
	}

    IEnumerator SpawnGhost()
    {
        GameObject gst = (GameObject)Instantiate(ghost, transform.position, transform.rotation);
        Destroy(gst, 0.3f);
        yield return new WaitForSeconds(0.2f);
        
    }

}