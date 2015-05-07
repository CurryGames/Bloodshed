using UnityEngine;
using System.Collections;

public class CreditsLogic : MonoBehaviour {

    private DataLogic dataLogic;
    private LoadingScreen loadingScreen;
    private AudioSource audioSor;

    private float temp;


	// Use this for initialization
	void Start () 
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").
            GetComponent<LoadingScreen>();
        audioSor = GetComponent<AudioSource>();

        audioSor.volume = dataLogic.volumFx;
	}
	
	// Update is called once per frame
	void Update () 
    {
        temp += Time.deltaTime;

        if (temp >= 35f)
        {
            audioSor.volume -= 0.1f;
            loadingScreen.loadNextScreen = true;
        }
	}
}
