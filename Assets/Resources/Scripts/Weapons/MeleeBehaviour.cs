using UnityEngine;
using System.Collections;

public class MeleeBehaviour : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {

		transform.Translate (0, 1.3f, 0);
		transform.Rotate (0, 30, 0);

		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (0, -230 * Time.deltaTime, 0);
		transform.position = new Vector3(player.transform.position.x, 1.3f, player.transform.position.z);
	}
}
