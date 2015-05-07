using UnityEngine;
using System.Collections;

public class PropPiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.y, 0));
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;


	}
}
