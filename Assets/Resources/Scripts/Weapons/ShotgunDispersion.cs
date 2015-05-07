using UnityEngine;
using System.Collections;

public class ShotgunDispersion : MonoBehaviour {
	public float minError = 0.5f;
	public float maxError = 0.5f;

	// Use this for initialization
	void Start () {
		transform.Rotate (new Vector3 (0f, Random.Range (minError, maxError), 0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
