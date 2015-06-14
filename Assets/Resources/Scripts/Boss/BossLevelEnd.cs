using UnityEngine;
using System.Collections;

public class BossLevelEnd : MonoBehaviour {

    private float currentTemp;
    private bool levelCleared;
    private PlayerStats playerStats;


	// Use this for initialization
	void Start () 
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        levelCleared = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!levelCleared)
        {
            currentTemp += Time.deltaTime;
            if (currentTemp >= 1.1f)
            {
                playerStats.LevelEnd();
                levelCleared = true;
            }
        }
    }
}
