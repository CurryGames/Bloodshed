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
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (dataLogic.joystickActive) textTutorial.text = textJoystick;
        else if (!dataLogic.joystickActive) textTutorial.text = textMouse;
	}
}
