using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuLogic : MonoBehaviour {

	public enum State {START, MENU, OPTIONS, ACHIEVEMENTS}
	public enum ScrResolution { HD, UXGA, FULLHD }

	public State screen;
	public ScrResolution scrResolution;
	public GameObject start; 
	public GameObject menu;
    public GameObject backButon;
    public AudioClip shoot;
    public AudioClip music;
	public GameObject options;
	private LoadingScreen loadingScreen;
    private DataLogic dataLogic;
    public Scrollbar scrollMusic;
    public Scrollbar scrollFx;
    private float currentTemp;
    private AchievementManager achManager;
    public GameObject lightning;
    public Button play;
	public Button hd;
	public Button cuatrotecios;
	public Button fullhd;
    public Slider slider;
	public Toggle fullScr;
    private bool down;
    private float temp;

	// Use this for initialization
	void Start () 
    {
		screen = State.START;
		scrResolution = ScrResolution.HD;
		loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").
			GetComponent<LoadingScreen>();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();
        achManager = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<AchievementManager>();
        down = false;
        dataLogic.iniHealth = 256;
        dataLogic.iniBrutality = 0;
        dataLogic.iniRiffleAmmo = 200;
        dataLogic.iniShotgunAmmo = 20;
        //dataLogic.PlayLoop(music, audioSource, dataLogic.volumMusic);
        scrollMusic.value = 0.5f;
        scrollFx.value = 0.5f;
		fullScr.isOn = Screen.fullScreen;
        currentTemp = 5.0f;
		hd.Select ();
	}
	
	// Update is called once per frame
	void Update () {


		switch (screen) {
	
		case State.START:

                temp += Time.deltaTime;

            if (!down)
            {
                start.SetActive(true);

                if (temp >= 0.5f)
                {
                    down = true;
                    temp = 0;
                }
            }
            else
            {
                start.SetActive(false);

                if (temp >= 0.5f)
                {
                    down = false;
                    temp = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

			menu.SetActive(false);
			options.SetActive(false);
            backButon.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Start"))
			{
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                play.Select();
                screen = State.MENU;
                dataLogic.Play(shoot, audiSor, scrollFx.value);
			}
			break;
		case State.MENU:
			start.SetActive(false);
			menu.SetActive(true);
			options.SetActive(false);
            backButon.SetActive(false);
            achManager.onMenu = false;
                
			break;
		case State.OPTIONS:
			start.SetActive(false);
			menu.SetActive(false);
			options.SetActive(true);
            switch (scrResolution)
            {

                case ScrResolution.HD:
                    hd.Select();
                    dataLogic.currentResolution = 0;
                    break;
                case ScrResolution.FULLHD:
                    fullhd.Select();
                    dataLogic.currentResolution = 1;
                    break;
                case ScrResolution.UXGA:
                    dataLogic.currentResolution = 2;
                    cuatrotecios.Select();
                    break;
            }
            break;
        case State.ACHIEVEMENTS:
            start.SetActive(false);
			menu.SetActive(false);
			options.SetActive(false);
            backButon.SetActive(true);
            achManager.onMenu = true;
            break;
		}

        currentTemp -= Time.deltaTime;
        if(currentTemp <= 0)
        {
            GameObject lght = (GameObject)Instantiate(lightning, transform.position, transform.rotation);
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.thunder, audiSor, dataLogic.volumFx);
            Destroy(lght, 0.6f);
            currentTemp = Random.Range (10.0f, 20.0f);
        }

        dataLogic.volumMusic = scrollMusic.value;
        dataLogic.volumFx = scrollFx.value;
        dataLogic.ligthnessIntensity = slider.value;
		Screen.fullScreen = fullScr.isOn;
	}

    public void PlayButton ()
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
		loadingScreen.loadNextScreen = true;
        dataLogic.Play(shoot, audiSor, scrollFx.value);

	}

    public void OptoinsButton()
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
        screen = State.OPTIONS;
        dataLogic.Play(shoot, audiSor, scrollFx.value);
    }

    public void BackButton()
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
        screen = State.MENU;
        dataLogic.Play(shoot, audiSor, scrollFx.value);
    }

    public void AchievementButton()
    {
        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
        screen = State.ACHIEVEMENTS;
        dataLogic.Play(shoot, audiSor, scrollFx.value);
    }

    public void ExitButton()
    {

        Application.Quit();
    }

	public void HDButton ()
	{
		Screen.SetResolution (1280, 720, fullScr);
        scrResolution = ScrResolution.HD;

	}
	public void UXGAButton ()
	{
		Screen.SetResolution (1600, 1200, fullScr);
        scrResolution = ScrResolution.UXGA;

	}
	public void FULLHDButton ()
	{
        Screen.SetResolution(1920, 1080, fullScr);
        scrResolution = ScrResolution.FULLHD;
	}
}
