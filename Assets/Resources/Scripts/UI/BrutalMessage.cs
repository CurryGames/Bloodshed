using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrutalMessage : MonoBehaviour {

    private Text messageText;
    private string message;

	// Use this for initialization
	void Start () 
    {
        messageText = GetComponent<Text>();
        message = messageText.text;
        StartCoroutine(Scrolltext(message, messageText));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Scrolltext(string text, Text textMesh)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            textMesh.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ScrolltextBack(string text, Text textMesh)
    {
        for (int i = text.Length; i > 0; i--)
        {
            textMesh.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
