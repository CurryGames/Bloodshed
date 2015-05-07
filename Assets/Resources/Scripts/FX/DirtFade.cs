using UnityEngine;
using System.Collections;

public class DirtFade : MonoBehaviour {

	public float fadingTime;
	public int secondsToFade;
	private bool fading;
	private Color color;
	public float temp;
	public GameObject GO;

	// Use this for initialization
	void Start () 
	{
		fadingTime = 5;
		secondsToFade = 10;
		temp = fadingTime;
		color = GetComponent<Renderer>().material.color;
		Invoke ("Fading", secondsToFade);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fading)
		{
			color.a = Mathf.Lerp (0, 1, temp/fadingTime);
			GetComponent<Renderer>().material.color = color;
			temp -= Time.deltaTime;
		}

		if (temp <= 0) Destroy (GO);
	}

	void Fading()
	{
		fading = true;
	}
}
