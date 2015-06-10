using UnityEngine;
using System.Collections;

public class BossSceneCamera : MonoBehaviour {

    public enum CinematicState { INTRO, OUTRO}
    public CinematicState cinematic;
	private Transform playerPos;
    private Vector3 currentPos;
	private Vector3 targetPositionIn;
	private Vector3 targetPositionOut;
	private Camera cinematicCamera;
	private GameObject mainCam;
	public GameObject interfaz;
	public BossMove bossMove;
    private float currentTime = 0;
    private PlayerMovement playerMov;
    private PlayerShooting playerShoot;
	// Use this for initialization
	void Start () {
		cinematicCamera = GetComponent<Camera> ();
		cinematicCamera.enabled = true;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCam.SetActive(false);
		interfaz.SetActive (false);
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerShoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		gameObject.transform.position = new Vector3 (playerPos.position.x, transform.position.y, playerPos.position.z);
		targetPositionIn = new Vector3 (0, transform.position.y, 8);
        targetPositionOut = playerPos.position + new Vector3(0, 63.8f, 0);
        cinematic = CinematicState.INTRO;
		bossMove = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossMove>();
		bossMove.StartCinematic();
	}
	
	// Update is called once per frame
	void Update () 
	{
        switch (cinematic)
        {
            case CinematicState.INTRO:
            {               
                currentTime += Time.deltaTime;
                if (currentTime >= 1.5) SmoothCinematicIn();
                else transform.position = playerPos.position + new Vector3(0, 63.8f, 0);

                if (currentTime >= 8)
                {
                    cinematic = CinematicState.OUTRO;
                    currentTime = 0;
                }
            } break;

            case CinematicState.OUTRO:
            {
                currentTime += Time.deltaTime;
                if (currentTime >= 1) SmoothCinematicOut();
                if (currentTime >= 3.5f) EndOfAnimation();
            } break;
        }	
	}

	public void SmoothCinematicIn()
	{
        transform.position = Vector3.Lerp(transform.position, targetPositionIn, 2f * Time.deltaTime);
	}

	public void SmoothCinematicOut()
	{
       // transform.position = Vector3.SmoothDamp(targetPositionIn, playerPos.position, ref velocity, smoothTime);
        transform.position = Vector3.Lerp(transform.position, targetPositionOut, 2f * Time.deltaTime);
	}

	public void EndOfAnimation()
	{
        mainCam.SetActive(true);
        this.gameObject.SetActive(false);
        playerMov.enabled = true;
        playerShoot.enabled = true;
        interfaz.SetActive(true);
        bossMove.active = true;
	}
}
