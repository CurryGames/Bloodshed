using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplySize : MonoBehaviour {

    float initialValueX;
    float initialValueY;
    float finalValue;
    float finalValueAlpha;
    float timeDuration;
    float timeCounter;
    float initAlpha;
    Color colorText;
    Color colorOutline;
    Text text;
    Outline outline;

    public bool animActive = false;

    RectTransform recTransform; 


    // Use this for initialization
    void Start()
    {
        recTransform = GetComponent<RectTransform>();
        initialValueX = recTransform.transform.localScale.x;
        initialValueY = recTransform.transform.localScale.y;
        text = GetComponent<Text>();
        outline = GetComponent<Outline>();
        colorText = text.color;
        colorOutline = outline.effectColor;
        initAlpha = colorText.a;
        finalValue = 0.5f;
        finalValueAlpha = 0;
        timeDuration = 5.0f;
        timeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animActive)
        {
            if (timeCounter <= timeDuration)
            {
                timeCounter += Time.deltaTime;
                float currentPositionX = (float)Easing.Linear(timeCounter, initialValueX, (finalValue - initialValueX), timeDuration);
                float currentPositionY = (float)Easing.Linear(timeCounter, initialValueY, (finalValue - initialValueY), timeDuration);
                float currentAlpha = (float)Easing.CubicEaseIn(timeCounter, initAlpha, (finalValueAlpha - initAlpha), timeDuration);

                recTransform.transform.localScale = new Vector3(currentPositionX, currentPositionY, recTransform.transform.localScale.z);
                colorText.a = currentAlpha;
                colorOutline.a = currentAlpha;
                text.color = colorText;
                outline.effectColor = colorOutline;

            }
            else animActive = false;
        }
        else
        {
            ResetAnim();
        }

    }

    public void ResetAnim()
    {
        timeCounter = 0;
        recTransform.transform.localScale = new Vector3(initialValueX, initialValueY, 1);
        colorText.a = initAlpha;
        colorOutline.a = initAlpha;
        text.color = colorText;
        outline.effectColor = colorOutline;
    }
}
