using UnityEngine;
using System.Collections;

public class InstantiateBlood : MonoBehaviour {

	public float instantiateTime;
	public GameObject blood;

	private float currentTime;
	private bool instanteBlood = false;

	// Use this for initialization
	void Start () {
		//StartCoroutine (Blood (2));
	}
	
	// Update is called once per frame
	void Update () {
		if(!instanteBlood)
		{
			currentTime += Time.deltaTime;
			if(currentTime >= instantiateTime)
			{
				GameObject bl = (GameObject)Instantiate (blood, transform.position, transform.rotation);
				bl.transform.parent = transform;
				instanteBlood = true;
			}
		}
	}
	/*
	IEnumerator Blood ( float time) 
	{
		GameObject bl = (GameObject)Instantiate (blood, transform.position, transform.rotation);
        bl.transform.parent = transform;
		yield return new WaitForSeconds (time);
		
	}*/
}
