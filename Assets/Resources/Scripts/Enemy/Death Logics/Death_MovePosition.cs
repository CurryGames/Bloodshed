using UnityEngine;
using System.Collections;

public class Death_MovePosition : MonoBehaviour {

    public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (force > 0.1)
        {
            transform.Translate(Vector3.up * Time.deltaTime * force);
            force *= 0.9f;
        }
        else force = 0;
	}
}
