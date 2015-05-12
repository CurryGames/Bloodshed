using UnityEngine;
using System.Collections;

public class IntroLogic : MonoBehaviour {

	public enum IntroState { TEXT1, TEXT2, TEXT3}

	private IntroState introState;

	public TextMesh introText1;
	public TextMesh introText2;
	public TextMesh introText3;

	private string text1;
	private string text2;
	private string text3;

	private LoadingScreen loadingScreen;
	private DataLogic dataLogic;
	private AudioSource audioSor;
	private AudioSource audioSource;

	private float currentTime;
	//private bool changeText = false;

	// Use this for initialization
	void Start () 
	{
		text1 = introText1.text;
		text2 = introText2.text;
		text3 = introText3.text;
		StartCoroutine(Scrolltext(text1, introText1));
		loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen"). GetComponent<LoadingScreen>();
		dataLogic = GameObject.FindGameObjectWithTag("DataLogic").
			GetComponent<DataLogic>();
		audioSor = GetComponent<AudioSource> ();
		audioSource = GetComponent<AudioSource> ();
		audioSor.volume = dataLogic.volumMusic;
		audioSource.volume = dataLogic.volumMusic;
		introState = IntroState.TEXT1;
	 
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(introState)
		{
		case IntroState.TEXT1:
			currentTime += Time.deltaTime;
			introText2.text = "";
			introText3.text = "";
			if(currentTime >= 5)
			{
				StartCoroutine(Scrolltext(text2, introText2));
				introState = IntroState.TEXT2;
				currentTime = 0.0f;
			}
		
		break;

		case IntroState.TEXT2:
			currentTime += Time.deltaTime;
			introText3.text = "";
			if(currentTime >= 7)
			{
				StartCoroutine(Scrolltext(text3, introText3));
				introState = IntroState.TEXT3;
				currentTime = 0.0f;
			}
			break;
		case IntroState.TEXT3:
			currentTime += Time.deltaTime;
			if(currentTime >= 5)
			{
				loadingScreen.loadNextScreen = true;
				currentTime = 0.0f;
			}
			break;

		}
	}

	IEnumerator Scrolltext ( string text, TextMesh textMesh) 
	{
		for (int i = 0; i <= text.Length; i ++)
		{
			textMesh.text = text.Substring(0, i);
			yield return new WaitForSeconds (0.05f);
		}
	}
}
