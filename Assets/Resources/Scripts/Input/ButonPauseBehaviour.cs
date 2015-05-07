using UnityEngine;
using System.Collections;

public class ButonPauseBehaviour : MonoBehaviour {

    public PauseLogic pauselogic;

    void Start()
    {
		pauselogic = GameObject.FindGameObjectWithTag("pause").
			GetComponent<PauseLogic>();
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
        pauselogic.Pause = false;
	}	
}
