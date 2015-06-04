using UnityEngine;
using System.Collections;

public class Ligthness : MonoBehaviour {

    private DataLogic dataLogic;
    private Light ligth;

	// Use this for initialization
	void Start () 
    {
        ligth = GetComponent<Light>();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();

        
	}
	
	// Update is called once per frame
	void Update () {
        ligth.intensity = dataLogic.ligthnessIntensity;
	}
}
