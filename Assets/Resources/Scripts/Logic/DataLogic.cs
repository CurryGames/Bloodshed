using UnityEngine;
using System.Collections;

public class DataLogic : MonoBehaviour {

	// AUDIOCLIP JUMP
	//public AudioClip jump;

	// VARIABLE CURRENT LEVEL
	private int currentLevel;
	private int nextLevel;
    private int lastLevel;
    public float iniHealth { get; set; }
    public float iniBrutality { get; set; }
    public int iniRiffleAmmo { get; set; }
    public int iniShotgunAmmo { get; set; }
    public int iniTime { get; set; }
    public int currentTime { get; set; }
	public int iniGrenades { get; set; }
    public int unlockRifle { get; set; }
    public int strike { get; set; }
    public bool riffleActive { get; set; }
    public AudioClip riffle;
    public AudioClip gun;
    public AudioClip gunClock;
    public AudioClip shootGun;
    public AudioClip shootGunClock;
    public AudioClip music;
    public AudioClip musicBrutal;
    public AudioClip chainsaw;
    public AudioClip health;
    public AudioClip ammo;
    public AudioClip can;
    public AudioClip glass;
    public AudioClip door;
    public AudioClip hit;
    public AudioClip bossMusic;
    public AudioClip heart;
    public AudioClip death;
    public AudioClip thunder;


    private int sreenShot;
    public float volumFx;
    public float volumMusic;
    public bool on;

	// SET 
	public void setCurrentLevel(int levelAux){
		currentLevel = levelAux;
	}

	public void setNextLevel(int levelAux){
		nextLevel = levelAux;
	}

    public void setBackLevel(int levelAux)
    {
        lastLevel = levelAux;
    }
	// GET
	public int getCurrentLevel(){
		return currentLevel;
	}
	public int getNextLevel(){
		return nextLevel;
	}
    public int getBackLevel()
    {
        return lastLevel;
    }
	// SE EJECUTA ANTES DE QUE EL ESCENARIO SE CARGUE
	void Awake(){

		// EL OBJETO NO SE DESTRUYE ENTRE ESCENAS
        sreenShot = 0;
        iniHealth = 256;
        iniBrutality = 0;
        iniRiffleAmmo = 200;
        iniShotgunAmmo = 20;
        unlockRifle = 1000;
        riffleActive = false;
        QualitySettings.antiAliasing = 0;
		DontDestroyOnLoad(transform.gameObject);
        Screen.SetResolution(1280, 720, true, 60);
	}

	// Use this for initialization
	void Start () {
		// CARGAMOS EL MENU
        if (!on) Application.LoadLevel("Logo");
        
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.CaptureScreenshot("ScreenShoot" + sreenShot.ToString("000") + ".png");
            sreenShot++;
        }
	}

	// FUNCION PLAY: REPRODUCE UN SONIDO 
	public void Play(AudioClip audio, AudioSource audioSource, float volum){

		// AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
		//AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
		// CARGAMOS EL CLIP
		audioSource.clip = audio;
		// PONEMOS EL VOLUMEN A TOPE
		audioSource.volume = volum;
		// REPRODUCIMOS EL SONIDO
		audioSource.Play ();
		// DESTRUIMOS EL AUDIOSOURCE UNA VEZ ACABADO EL SONIDO
        Destroy(audioSource, audio.length);
	}

    public void PlayLoop(AudioClip audio, AudioSource audioSource, float volum)
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        //AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = audio;
        audioSource.loop = true;
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = volum;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();

    }

}
