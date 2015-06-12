using UnityEngine;
using System.Collections;

public class FX_rot : MonoBehaviour {


    public Transform playerTrans;
    private Quaternion rot;
    // Use this for initialization
    void Start()
    {
        rot =  Quaternion.Euler(new Vector3(90, 245, 0));
        transform.rotation = Quaternion.Euler(playerTrans.rotation.x + rot.x, playerTrans.rotation.y + rot.y, playerTrans.rotation.z + rot.z);

    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = playerTrans.rotation;
	}
}
