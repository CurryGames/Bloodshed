using UnityEngine;
using System.Collections;

public class BossSceneCamera : MonoBehaviour {

	private Transform playerPos;
	private Vector3 targetPositionIn;
	private Vector3 targetPositionOut;
	private Camera cinematicCamera;
	private GameObject mainCam;
	public GameObject interfaz;
	private Vector3 velocity = Vector3.zero;
	public float smoothTime = 0.6F;
	private bool smoothIn = true;
	// Use this for initialization
	void Start () {
		cinematicCamera = GetComponent<Camera> ();
		cinematicCamera.enabled = true;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCam.SetActive(false);
		interfaz.SetActive (false);

		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		gameObject.transform.position = new Vector3 (playerPos.position.x, transform.position.y, playerPos.position.z);
		targetPositionIn = new Vector3 (0, transform.position.y, 4);
		targetPositionOut = new Vector3 ( playerPos.position.x, transform.position.y, playerPos.position.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (smoothIn) Invoke ("SmoothCinematicIn", 2);
		Invoke ("SmoothCinematicOut", 6);

	}

	public void SmoothCinematicIn()
	{
		transform.position = Vector3.SmoothDamp(transform.position, targetPositionIn, ref velocity, smoothTime);
		//Linear easing 
	}

	public void SmoothCinematicOut()
	{
		smoothIn = false;
		transform.position = Vector3.SmoothDamp(transform.position, targetPositionOut, ref velocity, smoothTime);
	}

	public void EndOfAnimation()
	{
		cinematicCamera.enabled = false;
		mainCam.SetActive (true);
	}
}
