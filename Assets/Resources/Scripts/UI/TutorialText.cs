using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

    private DataLogic dataLogic;
    private Text textTutorial;
    public string textMouse;
    public string textJoystick;


	// Use this for initialization
	void Start () 
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        textTutorial = GetComponent<Text>();
        if ((Input.GetJoystickNames().Length == 0)) textTutorial.text = textMouse;
        else textTutorial.text = textJoystick;
	}
	
	// Update is called once per frame
	void Update () 
    {
       
        
	}
}
