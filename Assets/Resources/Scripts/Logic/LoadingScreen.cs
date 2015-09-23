using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    public enum State { FADEIN, LOADING, FADEOUT }
    public State state;
    public float temp;
    public bool loadCurrentScreen;
    public bool loadNextScreen;
    public bool loadMenu;
    public bool loadTutorial, loadLevel1, loadLevel2, loadBoss;
    private int myLevel;
    private LevelLogic levelLogic;
    //private DataLogic dataLogic;
    public float tempInit = 1f;
    public Color color;

	// Use this for initialization
	void Start () {
        state = State.FADEOUT;        
        temp = tempInit;
        loadCurrentScreen = false;
        loadNextScreen = false;
        color = GetComponent<Renderer>().material.color;
        levelLogic = GameObject.FindGameObjectWithTag("LevelLogic").
            GetComponent<LevelLogic>();
        /*dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();*/

        DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        if (loadCurrentScreen)
        {
            myLevel = Application.loadedLevel;
            loadingNexttLevel();
        }
        if (loadNextScreen)
        {
            myLevel = Application.loadedLevel + 1;
            loadingNexttLevel();
        }
        if (loadMenu)
        {
            myLevel = 2;
            loadingNexttLevel();
        }
        if (loadTutorial)
        {
            myLevel = 4;
            loadingNexttLevel();
        }
        if (loadLevel1)
        {
            myLevel = 5;
            loadingNexttLevel();
        }
        if (loadLevel2)
        {
            myLevel = 6;
            loadingNexttLevel();
        }
        if (loadBoss)
        {
            myLevel = 7;
            loadingNexttLevel();
        }

        if (levelLogic == null) levelLogic = GameObject.FindGameObjectWithTag("LevelLogic").GetComponent<LevelLogic>();
	}


    void loadingNexttLevel()
    {
        switch (state){
        
            case State.FADEOUT:
            color.a = Mathf.Lerp(1, 0, temp / tempInit);
            GetComponent<Renderer>().material.color = color;
            temp -= Time.deltaTime;
			if (temp <= 0) {
                state = State.LOADING;
				temp = 0;
                Debug.Log("your level is loading");
			}
             break;

            case State.LOADING:
            
            Application.LoadLevel(myLevel);

             break;
            case State.FADEIN:

            
            color.a = Mathf.Lerp(0, 1, temp / tempInit);
            GetComponent<Renderer>().material.color = color;
            temp -= Time.deltaTime;  
            if (temp < 0)
            {

                state = State.FADEOUT; 
                temp = tempInit;
                loadCurrentScreen = false;
                loadNextScreen = false;
                loadMenu = false;
                loadTutorial = false;
                loadLevel1 = false;
                loadLevel2 = false;
                loadBoss = false;
                
                //myBool = false;
                //return myBool;
                //Destroy(this.gameObject);
            }
            break;
        }
    }


    void OnLevelWasLoaded(int level)
    {

        if ((level == myLevel) && state == State.LOADING)
        {
            temp = tempInit;
            state = State.FADEIN;
            Debug.Log("your level was loaded!!!");
        }
    }

}
