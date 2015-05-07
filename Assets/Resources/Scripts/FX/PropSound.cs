using UnityEngine;
using System.Collections;

public class PropSound : MonoBehaviour {

    private DataLogic dataLogic;
    private AudioSource audioSor;

	// Use this for initialization
	void Start ()
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<DataLogic>();
        audioSor = gameObject.AddComponent<AudioSource>();
        audioSor.volume = dataLogic.volumFx;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
