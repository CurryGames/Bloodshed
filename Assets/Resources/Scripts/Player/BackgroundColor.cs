using UnityEngine;
using System.Collections;

public class BackgroundColor : MonoBehaviour {

    public Color32 InitNormalColor;
    public Color32 FinalNormalColor;
    public Color32 InitBrutalColor;
    public Color32 FinalBrutalColor;

    public float duration;
    public float brutalDuration;

    private Color32 currentColor;
    private int currentTime;
    private int currentBrutalityTime;
    private bool fadeOut;

    private PlayerStats playerStats;

	// Use this for initialization
	void Start () 
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        currentColor.a = 255;
        fadeOut = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!playerStats.brutalMode) NormalColorTransition();
        else if (playerStats.brutalMode) BrutalityColorTransition();

        GetComponent<Renderer>().material.color = (Color)currentColor;
        //renderer.material.color = currentColor;
	}

    void NormalColorTransition()
    {
        //currentBrutalityTime = 0;
        if (!fadeOut)
        {
            currentTime++;

            if (currentTime <= duration*60)
            {
                currentColor.r = (byte)Easing.Linear((double)currentTime, (double)InitNormalColor.r, (double)(FinalNormalColor.r - InitNormalColor.r), (double)duration * 60);
                currentColor.g = (byte)Easing.Linear((double)currentTime, (double)InitNormalColor.g, (double)(FinalNormalColor.g - InitNormalColor.g), (double)duration * 60);
                currentColor.b = (byte)Easing.Linear((double)currentTime, (double)InitNormalColor.b, (double)(FinalNormalColor.b - InitNormalColor.b), (double)duration * 60);
            }
            else
            {
                fadeOut = true;
                currentTime = 0;
            }
        }
        else
        {
            currentTime++;

            if (currentTime <= duration*60)
            {
                currentColor.r = (byte)Easing.Linear((double)currentTime, (double)FinalNormalColor.r, (double)(InitNormalColor.r - FinalNormalColor.r), (double)duration * 60);
                currentColor.g = (byte)Easing.Linear((double)currentTime, (double)FinalNormalColor.g, (double)(InitNormalColor.g - FinalNormalColor.g), (double)duration * 60);
                currentColor.b = (byte)Easing.Linear((double)currentTime, (double)FinalNormalColor.b, (double)(InitNormalColor.b - FinalNormalColor.b), (double)duration * 60);
            }
            else
            {
                fadeOut = false;
                currentTime = 0;
            }
        }

    }

    void BrutalityColorTransition()
    {
        //currentTime = 0;
        if (!fadeOut)
        {
            currentBrutalityTime++;

            if (currentBrutalityTime <= brutalDuration * 60)
            {
                currentColor.r = (byte)Easing.Linear((double)currentBrutalityTime, (double)InitBrutalColor.r, (double)(FinalBrutalColor.r - InitBrutalColor.r), (double)brutalDuration * 60);
                currentColor.g = (byte)Easing.Linear((double)currentBrutalityTime, (double)InitBrutalColor.g, (double)(FinalBrutalColor.g - InitBrutalColor.g), (double)brutalDuration * 60);
                currentColor.b = (byte)Easing.Linear((double)currentBrutalityTime, (double)InitBrutalColor.b, (double)(FinalBrutalColor.b - InitBrutalColor.b), (double)brutalDuration * 60);
            }
            else
            {
                fadeOut = true;
                currentBrutalityTime = 0;
            }
        }
        else if (fadeOut)
        {
            currentBrutalityTime++;

            if (currentBrutalityTime <= brutalDuration * 60)
            {
                currentColor.r = (byte)Easing.Linear((double)currentBrutalityTime, (double)FinalBrutalColor.r, (double)(InitBrutalColor.r - FinalBrutalColor.r), (double)brutalDuration * 60);
                currentColor.g = (byte)Easing.Linear((double)currentBrutalityTime, (double)FinalBrutalColor.g, (double)(InitBrutalColor.g - FinalBrutalColor.g), (double)brutalDuration * 60);
                currentColor.b = (byte)Easing.Linear((double)currentBrutalityTime, (double)FinalBrutalColor.b, (double)(InitBrutalColor.b - FinalBrutalColor.b), (double)brutalDuration * 60);
            }
            else
            {
                fadeOut = false;
                currentBrutalityTime = 0;
            }
        }

    }
}
