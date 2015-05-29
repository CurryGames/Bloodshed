using UnityEngine;
using System.Collections;

public class Blood_particles : MonoBehaviour {

    private float posX, posZ, rotY;
    private bool located;
    public int showChance;
    private int chance;
	// Use this for initialization
	void Start () {
        posX = Random.Range(-2.4f, 2.4f);
        posZ = Random.Range(-2f, 2f);
        rotY = Random.Range(0, 360);
        located = false;
        chance = Random.Range(0, 100);
        if (chance >= showChance) enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (!located)
        {
            transform.position = this.transform.position + new Vector3(posX, 0, posZ);
            transform.rotation = Quaternion.Euler(new Vector3(90, rotY, 0));
            located = true;
        }

        
	}
}
