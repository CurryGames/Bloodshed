using UnityEngine;
using System.Collections;

public class IntroLogic : MonoBehaviour {

	public TextMesh introText1;
	public TextMesh introText2;
	//public TextMesh introText3;

	private string text1;
	private string text2;
	//private string text3;

	private float currentTime;
	private bool changeText = false;

	// Use this for initialization
	void Start () 
	{
		text1 = introText1.text;
		text2 = introText2.text;
		//text3 = introText3.text;
		StartCoroutine(Scrolltext());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(changeText == false)
		{
			currentTime += Time.deltaTime;
			introText2.text = "";
			if(currentTime >= 7)
			{
				StartCoroutine(Scrolltext2());
				changeText = true;
			}
		}
	}	

	IEnumerator Scrolltext () 
	{
		for (int i = 0; i <= text1.Length; i ++)
		{
			introText1.text = text1.Substring(0, i);
			yield return new WaitForSeconds (0.05f);
		}
	}

	IEnumerator Scrolltext2 () 
	{
		for (int i = 0; i <= text2.Length; i ++)
		{
			introText2.text = text2.Substring(0, i);
			yield return new WaitForSeconds (0.05f);
		}
	}

/*	IEnumerator Scrolltext3 () 
	{
		for (int i = 0; i <= text3.Length; i ++)
		{
			introText2.text = text2.Substring(0, i);
			yield return new WaitForSeconds (0.05f);
		}
	}*/
}
