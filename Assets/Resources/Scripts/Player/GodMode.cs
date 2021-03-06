﻿using UnityEngine;
using System.Collections;

public class GodMode : MonoBehaviour {

	public bool godmode;
	private BoxCollider playerCollider;
    private PlayerStats playerStats;
	public GameObject godSprite;
    private LevelLogic levelLogic;
	private DataLogic datalogic;

	// Use this for initialization
	void Start () {
		godmode = false;
		playerCollider = GetComponent <BoxCollider> ();
        playerStats = GetComponent<PlayerStats>();
        levelLogic = GameObject.FindGameObjectWithTag("LevelLogic").
            GetComponent<LevelLogic>();
		datalogic = GameObject.FindGameObjectWithTag("DataLogic").
			GetComponent<DataLogic>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.G) && Input.GetKey(KeyCode.AltGr)) godmode = !godmode;
        if (Input.GetKeyUp(KeyCode.N) && Input.GetKey(KeyCode.AltGr)) levelLogic.loadNextLevel();
        if (Input.GetKeyUp(KeyCode.B) && Input.GetKey(KeyCode.AltGr)) levelLogic.loadBackLevel();
		if (godmode == false) 
		{
			//playerCollider.enabled = true;
			godSprite.SetActive (false);

		}
		else
		{
			//playerCollider.enabled = false;
			godSprite.SetActive (true);
            playerStats.currentBrutality += 300;
            playerStats.currentGrenades += 3;
			datalogic.riffleActive = true;
		}

        if (Input.GetKeyUp(KeyCode.J) && Input.GetKey(KeyCode.AltGr)) playerCollider.enabled = !playerCollider.enabled;
        
	
	}
}
