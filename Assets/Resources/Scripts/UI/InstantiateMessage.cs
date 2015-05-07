using UnityEngine;
using System.Collections;

public class InstantiateMessage : MonoBehaviour {

    public GameObject message;
    public float destroyTemp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("COLISION: "+col);

        GameObject msm = (GameObject)Instantiate(message, new Vector3 (0, 0, 0), transform.rotation);
        Destroy(msm, destroyTemp);
        Destroy(this.gameObject);
        
    }
}
