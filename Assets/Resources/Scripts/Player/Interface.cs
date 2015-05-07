using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

    // background image that is 256 x 32
    public Texture2D bgImage;
	private PlayerStats playerStats;

    // foreground image that is 256 x 32
    public Texture2D fgImage;
	public Texture2D fgBrutalityImage;
    private Texture2D life;
    public Texture2D key;

    // a float between 0.0 and 1.0
    public float playerEnergy = 1.0f;
	public float playerBrutality = 1.0f;
    bool onkey;

	void Awake () 
	{

		playerStats = GameObject.FindGameObjectWithTag("Player").
			GetComponent<PlayerStats>();
		
	}

	void Update ()
	{
		playerEnergy = playerStats.currentHealth;
		playerBrutality = playerStats.currentBrutality;
        onkey = playerStats.onKey;
	}

    void OnGUI()
    {
        // Create one Group to contain both images
        // Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup(new Rect(0, 0, 256, 32));

        // Draw the background image
		GUI.Box(new Rect(0, 0, 256, 32), bgImage);

        // Create a second Group which will be clipped
        // We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup(new Rect(0, 0, playerEnergy * 1, 32));

        // Draw the foreground image
		GUI.Box(new Rect(0, 0, 256, 32), fgImage);

        // End both Groups

		GUI.EndGroup();

        GUI.EndGroup();


		GUI.BeginGroup(new Rect(0, 32, 256, 32));
		
		// Draw the background image
		GUI.Box(new Rect(0, 0, 256, 32), bgImage);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup(new Rect(0, 0, playerBrutality * 1, 32));
		
		// Draw the foreground image
		GUI.Box(new Rect(0, 0, 256, 32), fgBrutalityImage);
		
		// End both Groups
		
		GUI.EndGroup();
		
		GUI.EndGroup();

        if (onkey) GUI.Box(new Rect(0, 64, 32, 32), key);
	}

}