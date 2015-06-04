using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageAnimation : MonoBehaviour {

    private float finalValueAlpha;
    private float timeDuration;
    private float timeCounter;
    private float initAlpha;
    private float fadeOutDuration;
    private Color colorImage;
    private Image image;

    public bool animActive = false;
    private bool fadeIn = true;


    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        colorImage = image.color;
        initAlpha = 0;
        finalValueAlpha = colorImage.a;
        timeDuration = 0.2f;
        fadeOutDuration = 1.0f;
        
        timeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animActive)
        {
            if (fadeIn)
            {
                if (timeCounter <= timeDuration)
                {
                    timeCounter += Time.deltaTime;
                    float currentAlpha = (float)Easing.CubicEaseIn(timeCounter, initAlpha, (finalValueAlpha - initAlpha), timeDuration);

                    colorImage.a = currentAlpha;
                    image.color = colorImage;

                }
                else
                {
                    fadeIn = false;
                    timeCounter = 0;
                }
            }
            else
            {
                if (timeCounter <= fadeOutDuration)
                {
                    timeCounter += Time.deltaTime;
                    float currentAlpha = (float)Easing.CubicEaseIn(timeCounter, finalValueAlpha, (initAlpha - finalValueAlpha), fadeOutDuration);

                    colorImage.a = currentAlpha;
                    image.color = colorImage;

                }
                else animActive = false;
            }
        }
        else
        {
            IntialValue();
        }

    }

    public void ResetAnim()
    {
        timeCounter = 0;
        colorImage.a = finalValueAlpha;
        image.color = colorImage;
        fadeIn = false;
    }

    private void IntialValue()
    {
        timeCounter = 0;
        colorImage.a = 0;
        image.color = colorImage;
        fadeIn = true;
    }

}
