using UnityEngine;
using System.Collections;

public class OpenDoorEnemy : MonoBehaviour {

    public GameObject door;
    private bool open;

	// Use this for initialization
	void Start () {
        open = false;
	}
	
	// Update is called once per frame
	void Update () {
        door.SetActive(!open);
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy.doorCounter == 0 && open == false) open = true;
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            
            if (enemy.doorCounter == 0 && open == true) open = false;
            enemy.doorCounter++;

        }
    }
}
