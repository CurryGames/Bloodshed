using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

    public bool keyObjective;

    private PlayerStats playerStats;
    public GameObject arrow;

	// Use this for initialization
	void Start () {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //foreach (Transform trans in this.gameObject.transform) if (trans.name == "Arrow") arrow = trans.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(keyObjective)
        {
            if (!playerStats.onKey) arrow.SetActive(true);
            else arrow.SetActive(false);
        }
        else
        {
            if (playerStats.onKey) arrow.SetActive(true);
            else arrow.SetActive(false);
        }
	}
}
