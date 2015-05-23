using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

    private Text textTutorial;
    public string textMouse;
    public string textJoystick;


	// Use this for initialization
	void Start () 
    {
        textTutorial = GetComponent<Text>();

        // NO FUNCIONA EN UNITY5
        /*if ((Input.GetJoystickNames().Length > 1)) textTutorial.text = textJoystick;
        else textTutorial.text = textMouse;*/

        if (string.IsNullOrEmpty(Input.GetJoystickNames()[0])) textTutorial.text = textMouse;
        else textTutorial.text = textJoystick;

        //Debug.Log(Input.GetJoystickNames().Length);
	}
	
	// Update is called once per frame
	void Update () 
    {
       
        
	}
}
