using UnityEngine;
using System.Collections;

public class Atack : MonoBehaviour {

    public enum AtackStates { HIT, REPOSE }
    private BoxCollider SwordCollider;
    private float speed;
    public AtackStates State;
    private float AtackTime = 0.5F;


	// Use this for initialization
	void Start () {
        State = AtackStates.REPOSE;
        SwordCollider = transform.FindChild("Cube").
            GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (State)
        {
            case AtackStates.HIT:
                transform.Rotate(0, Time.deltaTime * (-150), 0);
                SwordCollider.enabled = true;
                AtackTime -= Time.deltaTime;
                if (AtackTime <= 0)
                {
                    State = AtackStates.REPOSE;
                }
                break;
            case AtackStates.REPOSE:
                transform.eulerAngles = new Vector3(0, 25, 0);
                SwordCollider.enabled = false;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AtackTime = 0.5F;
                    State = AtackStates.HIT;
                }
                break;
        }
	
	}
}
