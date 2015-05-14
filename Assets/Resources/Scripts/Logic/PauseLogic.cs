using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseLogic : MonoBehaviour {


    public bool Pause = false;
    public GameObject pause;
	//private PlayerShooting playerShot;
    private LoadingScreen loadingScreen;
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
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            Pause = !Pause;
        }
        
        if (Pause)
        {
            if (pause != null)
            {
                pause.SetActive(true);
                //playerShot.enabled = false;
                Time.timeScale = 0;
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

    
}
