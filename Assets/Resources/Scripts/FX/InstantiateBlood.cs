using UnityEngine;
using System.Collections;

public class InstantiateBlood : MonoBehaviour {

	public float instantiateTime;
	public GameObject blood;

	// Use this for initialization
	void Start () {
		StartCoroutine (Blood (instantiateTime));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Blood ( float time) 
	{
		Instantiate (blood, transform.position, transform.rotation);
		yield return new WaitForSeconds (time);
		
	}
}
