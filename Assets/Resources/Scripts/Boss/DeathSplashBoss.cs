using UnityEngine;
using System.Collections;

public class DeathSplashBoss : MonoBehaviour {

    public Transform playerTrans;
	// Use this for initialization
	void Start () {

        transform.rotation = playerTrans.rotation;
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
