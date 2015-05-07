using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageAnimation : MonoBehaviour {

    float initialValueY;
    float finalValue;
    float timeDuration;
    float timeCounter;
    float timeCounter2;
    float timeDelay;

    public bool animActive = false;

    RectTransform recTransform;


    // Use this for initialization
    void Start()
    {
        recTransform = GetComponent<RectTransform>();
        initialValueY = recTransform.transform.position.y - recTransform.rect.height;
        finalValue = recTransform.transform.position.y;
        timeDuration = 2.0f;
        timeCounter = 0f;
        timeDelay = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeDelay -= Time.deltaTime;

        if (timeCounter <= timeDuration)
        {
            timeCounter += Time.deltaTime;
            float currentPositionY = (float)Easing.CubicEaseOut(timeCounter, initialValueY, (finalValue - initialValueY), timeDuration);


            recTransform.transform.position = new Vector3(recTransform.transform.position.x, currentPositionY, recTransform.transform.position.z);
        }

        if(timeDelay <= 0)
        {
            if (timeCounter2 <= timeDuration)
            {
                timeCounter2 += Time.deltaTime;
                float currentPositionY = (float)Easing.CubicEaseOut(timeCounter2, finalValue, (initialValueY - finalValue), timeDuration);


                recTransform.transform.position = new Vector3(recTransform.transform.position.x, currentPositionY, recTransform.transform.position.z);
            }

        }


    }
}