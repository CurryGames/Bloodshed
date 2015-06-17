using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public float maxHealth;
    public float currentHealth { get; set; }
	public float speed;
    public float currentBrutality { get; set; }
    public int deathNumber { get; set; }
	public int damage;
    public int currentMunition;
    public int maxMunition;
	public int currentGrenades;
	private GodMode godMode;
	public GameObject EndLevelScreen;
    public GameObject scoreMessage;
    public GameObject unlockMessage;
    public GameObject[] brutalMessage;
    public Slider HealthBar;
    public Slider BrutalityBar;
	private PauseLogic pauseLogic;
    public Text bullets;
    public Text grenades;
    public GameObject keyText;
    private TextMesh points;
    private DataLogic dataLogic;
    private LoadingScreen loadingScreen;
    private PlayerMovement playerMov;
    private PlayerShooting playerShoot;
    //public ShakeUI shakeUI;
    public int riffleBullets { get; set; }
    public int shotgunBullets { get; set; }
    public int score { get; set; }
    public int multiply { get; set; }
    public float multiplyTemp { get; set; }
    public bool onCombo { get; set; }
    private int counter = 0;
    private float counterScore = 0;
    private float keyTimmer;
    private int calculateScore;
    public GameObject gameOverScreen;
    private GameObject scrMsm;
    //private EnemyNavMesh enemyNav;
    private Text scoreText;
    private Text multiplyText;
    private Grayscale grayscale;

	private bool alive = true;
    private bool brutalMsm;
    public bool onBoss;
    public bool onKey;
    public bool brutalMode;
	public bool levelCleared;
    private bool keyShowMessage;
    private bool go;

    private float keyCounter;
    private float brutalTimmer;
    public AudioSource audiSorMusic;
    public AudioSource audiSorBrutal;
    //public AudioSource audiSorChainsaw;
    public AudioSource audioSorTension;
    public MultiplySize multiplyAnim;

	private Animator animation;
    private Animator animationLegs;
    private AchievementManager achievementManager;
    private DamageAnimation damageAnimaion;
    private AudioReverbFilter audioReberb;
    private GameObject brutalityFire;
    private GameObject keyUI;
	public GameObject bossCamera;

	// Use this for initialization
	void Start () 
	{
		godMode = GetComponent<GodMode> (); 
		animation = GetComponentInChildren<Animator> ();
        animationLegs = GameObject.FindGameObjectWithTag("Legs").GetComponent<Animator>();
		pauseLogic = GameObject.FindGameObjectWithTag ("pause").GetComponent<PauseLogic> ();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").
            GetComponent<LoadingScreen>();
        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent <Text>();
        multiplyText = GameObject.FindGameObjectWithTag("multiplyText").GetComponent<Text>();
        achievementManager = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<AchievementManager>();
        multiplyAnim = GameObject.FindGameObjectWithTag("multiplyText").GetComponent<MultiplySize>();
        bullets = GameObject.FindGameObjectWithTag("BulletText").GetComponent<Text>();
        grenades = GameObject.FindGameObjectWithTag("GrenadesText").GetComponent<Text>();
        playerMov = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShooting>();
        grayscale = Camera.main.GetComponent<Grayscale>();
        brutalityFire = GameObject.FindGameObjectWithTag("BrutalityFire");
        damageAnimaion = GameObject.FindGameObjectWithTag("Damage").GetComponent<DamageAnimation>();
        keyUI = GameObject.FindGameObjectWithTag("KeyUI");
        //enemyNav = GetComponent<EnemyNavMesh>();
		speed = 7f;
		maxHealth = 256;
        riffleBullets = dataLogic.iniRiffleAmmo;
        shotgunBullets = dataLogic.iniShotgunAmmo;
		currentGrenades = dataLogic.iniGrenades;
		levelCleared = false;
        brutalMode = false;
        go = true;
        brutalMsm = false;
        damage = 6;
        multiply = 1;
        grayscale.enabled = false;
		score = dataLogic.iniScore;
        currentBrutality = dataLogic.iniBrutality;
		currentHealth = dataLogic.iniHealth;
		//GameOverScreen.SetActive (false);
		//EndLevelScreen.SetActive (false);
        audiSorMusic = gameObject.AddComponent<AudioSource>();
        audiSorBrutal = gameObject.AddComponent<AudioSource>();
        //audiSorChainsaw = gameObject.AddComponent<AudioSource>();
        audioSorTension = gameObject.AddComponent<AudioSource>();

        foreach (Transform t in Camera.main.transform) if (t.name == "AudioListener") audioReberb = t.gameObject.GetComponent<AudioReverbFilter>();

        if (onBoss == false) dataLogic.PlayLoop(dataLogic.music, audiSorMusic, dataLogic.volumMusic);
        else
        {
            dataLogic.PlayLoop(dataLogic.heart, audiSorMusic, dataLogic.volumMusic);
            dataLogic.PlayLoop(dataLogic.tension, audioSorTension, dataLogic.volumMusic);
        }
        dataLogic.PlayLoop(dataLogic.musicBrutal, audiSorBrutal, dataLogic.volumMusic);
        //dataLogic.PlayLoop(dataLogic.chainsaw, audiSorChainsaw, dataLogic.volumFx);
        audiSorBrutal.Pause();
        //audiSorChainsaw.Pause();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreText != null)scoreText.text = score.ToString();

        keyUI.SetActive(onKey);

        audiSorMusic.volume = dataLogic.volumMusic;
        audiSorBrutal.volume = dataLogic.volumMusic;
        audioSorTension.volume = dataLogic.volumMusic;

        if (HealthBar != null)HealthBar.value = currentHealth / maxHealth;
        if (BrutalityBar != null) BrutalityBar.value = currentBrutality / 256;

        if (keyShowMessage)
        {
            keyTimmer += Time.deltaTime;
            if (keyTimmer >= 7.0f)
            {
                keyShowMessage = false;
                keyTimmer = 0;
            }
        }

		if (currentHealth >= maxHealth) currentHealth = maxHealth;
        if (currentBrutality >= 256)
        {
            currentBrutality = 256;
            brutalityFire.SetActive(true);
        }
        else brutalityFire.SetActive(false);
		//if (Input.GetKeyDown (KeyCode.E)) Application.Quit ();

        if (onCombo == true) 
        {
            if (multiplyText != null) multiplyText.text = "X" + multiply.ToString();
            multiplyTemp += Time.deltaTime;
            if (multiplyAnim != null) multiplyAnim.animActive = true;
            achievementManager.SetProgressToAchievement("x20", (float)multiply);

            if (multiplyTemp >= 5.0f)
            {
                onCombo = false;
                multiply = 1;
                if (multiplyAnim != null) multiplyAnim.animActive = false;
                //shakeUI.endShake = true;
            }
        }

        else if (multiplyText != null) multiplyText.text = "";

        if (currentHealth <= maxHealth / 4 && !brutalMode && alive && !levelCleared)
        {
            grayscale.enabled = true;
            audioReberb.enabled = true;
        }
        else 
        {
            grayscale.enabled = false;
            audioReberb.enabled = false;
        }

        if (brutalMsm)
        {
            brutalTimmer += Time.deltaTime;
            if(brutalTimmer >= 1.0f)
            {
                brutalTimmer = 0;
                brutalMsm = false;
            }
        }

        if (currentHealth <= 0 && alive) 
		{
			currentHealth = 0;
            GameOver();
			
		}

        if (riffleBullets <= 0)
        {
            riffleBullets = 0;
        }

        if (shotgunBullets <= 0)
        {
            shotgunBullets = 0;
        }

        if (currentGrenades >= 3)
        {
            currentGrenades = 3;
        }

        if(currentGrenades <= 0)
        {
            currentGrenades = 0;
            
        }

		if (!alive)
		{
            keyCounter += Time.deltaTime;
            if (Input.anyKeyDown && keyCounter >= 2f)
            {
                loadingScreen.loadCurrentScreen = true;
                pauseLogic.enabled = true;
            }
            

		}

        if (levelCleared == true)
        {
            go = false;
            keyCounter += Time.deltaTime;
            if (scoreMessage != null)
            {
                counterScore ++;
                if (counterScore <= 2.5f*60)
                {
                    calculateScore = (int)Easing.Linear(counterScore, 0, score, 2.5f*60);
                    
                }


                if (dataLogic.currentWeapon == 0)points.text = calculateScore.ToString() + "/" + dataLogic.unlockRifle.ToString();
                else points.text = calculateScore.ToString() + "/" + dataLogic.unlockFlamethrower.ToString();

                if (Input.anyKeyDown && counterScore >= 2.5f* 60)
                {
                    loadingScreen.loadNextScreen = true;
                    dataLogic.iniScore = 0;
                    pauseLogic.enabled = true;
                    //dataLogic.currentWeapon++;
                }
                //else calculateScore = score;

                if (calculateScore >= dataLogic.unlockRifle && counter < 1) 
                {
                    Instantiate(unlockMessage, Camera.main.transform.position, Quaternion.Euler(new Vector3(90,0,0)));
                    counter++;
                }

                
                
            }
            else
            {
                if (Input.anyKeyDown && keyCounter >= 2f)
                {
                    loadingScreen.loadNextScreen = true;
                    dataLogic.iniScore = 0;
                    pauseLogic.enabled = true;
                }
            }
        }
        

        grenades.text = currentGrenades.ToString();
	}

	void OnTriggerEnter (Collider col)
	{
		//Debug.Log("COLISION: "+col.name);

		if(col.gameObject.tag == "enemyBullet")
		{	
			Destroy(col.gameObject);
			if (godMode.godmode == false)
			{
				GetDamage(damage);
			}
			else GetDamage (0);
		}

		if(col.gameObject.tag == "enemyBulletSHOTGUN")
		{	
			Destroy(col.gameObject);
			if (godMode.godmode == false)
			{
				GetDamage(damage);
			}
			else GetDamage (0);
		}

        if ((col.gameObject.tag == "Medicine") && (currentHealth < maxHealth))
        {
            Destroy(col.gameObject);
            GetHealth(100);

        }

        if ((col.gameObject.tag == "MedicineLil") && (currentHealth < maxHealth))
        {
            Destroy(col.gameObject);
            GetHealth(100);

        }

        if ((col.gameObject.tag == "riffleAmmo") && dataLogic.riffleActive == true)
        {
            Destroy(col.gameObject);
            GetAmmoRiffle(100);

        }

        if ((col.gameObject.tag == "shotgunAmmo"))
        {
            Destroy(col.gameObject);
            GetAmmoShotgun(10);
        }

        if(col.gameObject.tag == "grenadesBox")
        {
            currentGrenades++;
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.ammo, audiSor, dataLogic.volumFx);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "grenadesBoxInfinite")
        {
            currentGrenades++;
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.ammo, audiSor, dataLogic.volumFx);
        }

        if ((col.tag == "pointB") && onKey)
        {
            levelCleared = true;
            onKey = false;
            dataLogic.iniHealth = currentHealth;
            dataLogic.iniBrutality = currentBrutality;
            dataLogic.iniRiffleAmmo = riffleBullets;
            dataLogic.iniShotgunAmmo = shotgunBullets;
            dataLogic.iniGrenades = currentGrenades;
        }

        if ((col.tag == "levelEnding") && !levelCleared)
        {
            LevelEnd();
        }

        if ((col.tag == "ScreenEnding"))
        {
            loadingScreen.loadNextScreen = true;
            brutalMode = false;
			//playerMov.enabled = false;
            dataLogic.iniScore = score;
            dataLogic.iniHealth = currentHealth;
            dataLogic.iniBrutality = currentBrutality;
            dataLogic.iniRiffleAmmo = riffleBullets;
            dataLogic.iniShotgunAmmo = shotgunBullets;
			dataLogic.iniGrenades = currentGrenades;
        }

        if ((col.tag == "ScreenEndingKey") && onKey)
        {
            loadingScreen.loadNextScreen = true;
            brutalMode = false;
			//playerMov.enabled = false;
            dataLogic.iniScore = score;
            dataLogic.iniHealth = currentHealth;
            dataLogic.iniBrutality = currentBrutality;
            dataLogic.iniRiffleAmmo = riffleBullets;
            dataLogic.iniShotgunAmmo = shotgunBullets;
            dataLogic.iniGrenades = currentGrenades;
        }

        if ((col.tag == "keyDoor") && onKey == false && !keyShowMessage)
        {
            GameObject keymessage = (GameObject)Instantiate(keyText, transform.position, transform.rotation);
            keyShowMessage = true;

            Destroy(keymessage, 9);
        }

        if ((col.tag == "keyDoor") && onKey)
        {

            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.door, audiSor, dataLogic.volumFx);
            Destroy(col.gameObject);
            onKey = false;
        }

        if (col.tag == "Key")
        {
            onKey = true;
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.ammo, audiSor, dataLogic.volumFx);
            Destroy(col.gameObject);
        }

        if (col.tag == "Can")
        {
            AudioSource audiSor = col.gameObject.AddComponent<AudioSource>();
            achievementManager.AddProgressToAchievement("Messi", 1.0f);
            dataLogic.Play(dataLogic.can, audiSor, dataLogic.volumFx);
        }

        if (col.tag == "BossStage" && onBoss == true)
        {
            
           
            Invoke("ActivateBossCam", 0.75f);
            DeactivatePlayer();
			onBoss = false;
        } 
	}


	public void GetDamage(int dmg)
	{
		currentHealth -= dmg;

        if (damageAnimaion.animActive == true)
        {
            damageAnimaion.ResetAnim();
        }
        else damageAnimaion.animActive = true;
	}

    void GetHealth(int hlth)
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
        dataLogic.Play(dataLogic.health, audiSor, dataLogic.volumFx);
        currentHealth += hlth;
    }

    void GetAmmoShotgun(int bulletNum)
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
        dataLogic.Play(dataLogic.ammo, audiSor, dataLogic.volumFx);
        shotgunBullets += bulletNum;
    }

    void GetAmmoRiffle(int bulletNum)
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
        dataLogic.Play(dataLogic.ammo, audiSor, dataLogic.volumFx);
        riffleBullets += bulletNum;
    }

	// ANIMATIONS
	
	public void setRiffle(){
	// REPRODUCIR LA ANIMACION DE Riffle
		animation.Play ("Riffle");
	}
	
	// ANIMACION DE CORRER HACIA LA DERECHA
	public void setShootgun(){
		// REPRODUCIMOS LA ANIMACION DE Shotgun
		animation.Play ("Shootgun");
		
	}
	
	public void setChainsaw(){
		// REPRODUCIMOS LA ANIMACION DE Chainsaw
		animation.Play ("Chainsaw");
	}

    public void setGun()
    {
        // REPRODUCIMOS LA ANIMACION DE Chainsaw
        animation.Play("Gun");
    }

    public void setRun()
    {
        // REPRODUCIMOS LA ANIMACION DE Chainsaw
        animationLegs.Play("Run");
    }

    public void setIddle()
    {
        // REPRODUCIMOS LA ANIMACION DE Chainsaw
        animationLegs.Play("Iddle");
    }

	public void GameOver(){

		//GameOverScreen.SetActive (true);
        alive = false;
        playerMov.enabled = false;
		//playerMov.enabled = false;
		//pauseLogic.enabled = false;
        GameObject gOS = (GameObject)Instantiate(gameOverScreen, new Vector3(Camera.main.transform.position.x, 55, Camera.main.transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
        gOS.transform.parent = Camera.main.transform;
        //gOS.transform.position = new Vector3(0, 0, 0);

	}
	public void LevelEnd(){
		
		//EndLevelScreen.SetActive (true);
        brutalMode = false;
        playerMov.enabled = false;
        GameObject gOS = (GameObject)Instantiate(EndLevelScreen, Camera.main.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        levelCleared = true;
        setIddle();
        go = false;
        if (scoreMessage != null)
        {
            scrMsm = (GameObject)Instantiate(scoreMessage, new Vector3(Camera.main.transform.position.x, 55, Camera.main.transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
            points = scrMsm.GetComponent<TextMesh>();
        }
        if (score >= dataLogic.unlockRifle && dataLogic.currentWeapon == 0)
        {
            dataLogic.riffleActive = true;
            achievementManager.SetProgressToAchievement("Riffle", 1.0f);
        }

        if (score >= dataLogic.unlockFlamethrower && dataLogic.currentWeapon == 1)
        {
            dataLogic.riffleActive = true;
            //achievementManager.SetProgressToAchievement("Riffle", 1.0f);
        }
        dataLogic.iniHealth = currentHealth;
        dataLogic.iniBrutality = currentBrutality;
        dataLogic.iniRiffleAmmo = riffleBullets;
        dataLogic.iniShotgunAmmo = shotgunBullets;
        dataLogic.iniGrenades = currentGrenades;
	}

    public void enemyKill(int puntuation)
    {
        score += puntuation;
        onCombo = true;
        multiply++;
        multiplyTemp = 0.0f;
        if (multiplyAnim.animActive == true && multiplyAnim != null)
        {
            multiplyAnim.ResetAnim();
        }
        deathNumber++;
    }

    public void DeactivatePlayer()
    {
        playerMov.enabled = false;
        playerShoot.enabled = false;
        setIddle();
    }

    public void ActivateBossCam()
    {
        bossCamera.SetActive(true);
    }

    public void BrutalMessageInstantiate(GameObject msm)
    {
        if (!brutalMsm)
        {
            GameObject brMsm = (GameObject)Instantiate(msm, new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z), Quaternion.identity);
            brMsm.transform.parent = Camera.main.transform;
            brutalMsm = true;
            Destroy(brMsm, 7.0f);
        }
    }
}
