using UnityEngine;
using System.Collections;

public class BrutalityInterface : MonoBehaviour {

	// background image that is 256 x 32
	public Texture2D bgImage;
	private PlayerStats playerStats;
	
	// foreground image that is 256 x 32
	public Texture2D fgImage;
	
	// a float between 0.0 and 1.0
	public float playerBrutality = 1.0f;
	
	void Awake () 
	{
		
		playerStats = GameObject.FindGameObjectWithTag("Player").
			GetComponent<PlayerStats>();
		
	}
	
	void Update ()
	{
		playerBrutality = playerStats.currentBrutality;
		
	}
	
	void OnGUI()
	{
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup(new Rect(0, 40, 300, 80));
		
		// Draw the background image
		GUI.Box(new Rect(0, 0, 300, 20), bgImage);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup(new Rect(0, 0, playerBrutality * 1, 20));
		
		// Draw the foreground image
		GUI.Box(new Rect(0, 0, 300, 20), fgImage);
		
		// End both Groups
		
		GUI.EndGroup();
		
		GUI.EndGroup();
	}
	
}