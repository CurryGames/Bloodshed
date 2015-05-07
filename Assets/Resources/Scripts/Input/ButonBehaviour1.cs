using UnityEngine;
using System.Collections;


public class ButonBehaviour1 : MonoBehaviour {

    private LoadingScreen loadingScreen;

    void Start()
    {
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").
            GetComponent<LoadingScreen>();
    }
	// ACTIVAMOS EL EVENTO ONDOWN
	void OnEnable(){
		GetComponent<InputBehaviour>().onDown += onDown;
	}
	
	// DESACTIVAMOS EL EVENTO ONDOWN
	void OnDisable(){
		GetComponent<InputBehaviour>().onDown -= onDown;
	}

	// QUÉ HACE EL EVENTO ONDOWN ???
	void onDown(int number){
        loadingScreen.loadNextScreen = true;
	}	
}
