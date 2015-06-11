using UnityEngine;
using System.Collections;

public class BossHeadMark : MonoBehaviour {

    public BossStats bossStats;
    public Transform bossTrans;
    private Transform myTransform;
	// Use this for initialization
	void Start () {
        myTransform = this.gameObject.transform;
;
	}
	
	// Update is called once per frame
	void Update () {


        //myTransform.TransformPoint(bossTrans.position);
        myTransform = bossTrans;

        Debug.Log(myTransform.position);
        Debug.Log(bossTrans.position);
	}
}
