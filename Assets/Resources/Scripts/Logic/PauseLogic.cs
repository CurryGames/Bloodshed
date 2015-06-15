using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseLogic : MonoBehaviour {

    public enum State { MENU, OPTIONS }
    public enum ScrResolution { HD, UXGA, FULLHD }

    public bool Pause = false;
    public GameObject pause;
    public GameObject menu;
    public GameObject options;

    public Button hd, cuatrotercios, fullhd;
    public Scrollbar musicVolume, fxVolume;
    public Toggle fullScr;

    private State state;
    private ScrResolution scrResolution;
	//private PlayerShooting playerShot;
    private LoadingScreen loadingScreen;
    private DataLogic dataLogic;
    //public bool fullScr;
    static PauseLogic instance;

    void Awake()
    {
        //destroy the already existing instance, if any
        if (instance)
        {
			Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);

		if(fullScr != null)fullScr.isOn = Screen.fullScreen;
    }

	// Use this for initialization
	void Start () {
		if (pause != null) {
			pause.SetActive (false);
		}
		/*playerShot = GameObject.FindGameObjectWithTag("Player").
			GetComponent<PlayerShooting>();*/
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").
            GetComponent<LoadingScreen>();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();
        if (dataLogic.currentResolution == 0) scrResolution = ScrResolution.HD;
        else if (dataLogic.currentResolution == 1) scrResolution = ScrResolution.UXGA;
        else if (dataLogic.currentResolution == 2) scrResolution = ScrResolution.FULLHD;

		if(musicVolume != null)musicVolume.value = dataLogic.volumMusic;
		if(fxVolume != null)fxVolume.value = dataLogic.volumFx;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            Pause = !Pause;
            state = State.MENU;

        }

		if(dataLogic == null) dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        
        if (Pause)
        {
            if (pause != null)
            {
                pause.SetActive(true);
                //playerShot.enabled = false;
                Time.timeScale = 0;
            }

            switch(state)
            {
                case State.MENU:
                    if (menu != null) menu.SetActive(true);
                    if (options != null) options.SetActive(false);

                    break;
                case State.OPTIONS:
                    if (menu != null) menu.SetActive(false);
                    if (options != null) options.SetActive(true);

                    switch (scrResolution)
                    {

                        case ScrResolution.HD:
                            hd.Select();
                            break;
                        case ScrResolution.FULLHD:
                            fullhd.Select();
                            break;
                        case ScrResolution.UXGA:   
                            cuatrotercios.Select();

                            break;
                    }
                    break;
            }
        }

        else if (!Pause)
        {
			if(pause!=null)
            {
                pause.SetActive(false);
			    //playerShot.enabled = true;
                Time.timeScale = 1;
			}
        }

        if (Application.loadedLevel == 2 || Application.loadedLevel == 9) Destroy(this.gameObject);

        if(musicVolume != null)dataLogic.volumMusic = musicVolume.value;
		if(fxVolume != null)dataLogic.volumFx = fxVolume.value;
	}

    public void	ResumeButton()
    {

        Pause = false;
    }

	public void	ExitButton()
    { 
        //Application.Quit ();
        loadingScreen.loadMenu = true;
        Pause = false;
    }

    public void OptionButton()
    {
        state = State.OPTIONS;
    }

    public void BackButton()
    {
        state = State.MENU;
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
