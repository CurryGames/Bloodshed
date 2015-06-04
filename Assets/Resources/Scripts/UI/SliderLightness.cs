using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderLightness : MonoBehaviour {

    Slider slider;
    DataLogic dataLogic;

	// Use this for initialization
	void Start () 
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
                GetComponent<DataLogic>();
        slider = GetComponent<Slider>();
        slider.value = dataLogic.ligthnessIntensity;
	}
	
	// Update is called once per frame
	void Update ()
    {
        dataLogic.ligthnessIntensity = slider.value;
	}
}
