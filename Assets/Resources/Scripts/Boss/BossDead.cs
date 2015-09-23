using UnityEngine;
using System.Collections;

public class BossDead : MonoBehaviour {

    public GameObject headshotFX;
    public GameObject bloodSplash;
    private float counter;
    private bool done;
    public Transform playerTrans;
    private Quaternion rot;
    //private GameObject dead;

	// Use this for initialization
	void Start () {
        rot = Quaternion.Euler(new Vector3(90, 245, 0));
        counter = 0;
        Instantiate(headshotFX, new Vector3(transform.position.x, 1.5f, transform.position.z), Quaternion.Euler(playerTrans.rotation.x + rot.x, playerTrans.rotation.y + rot.y, playerTrans.rotation.z + rot.z));
        done = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!done) counter += Time.deltaTime;
        if (counter >= 0.18f)
        {
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            counter = 0;
            done = true;
        }    
            


	}
}
