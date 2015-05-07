using UnityEngine;
using System.Collections;

public class ShakeUI : MonoBehaviour 
{	
	private bool shake;
    public bool startShake;	
	private Vector3 originPosition;
	private Quaternion originRotation;
    private RectTransform recTransform;
    
   
    public bool endShake; // Check if shake is end.
	public bool isShaking; // Check if camera is shaking right now.
	public float shakingForce; // Initial force.
	public float shakeDecay; // Decay force.

    void Start()
    {
        recTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		//SHAKE
		if(startShake)
		{
			isShaking = true;
			originPosition = recTransform.position;
            originRotation = recTransform.rotation;
			startShake = false;
			shake = true;
		}
		else if(shake && !endShake)
		{
			if(shakingForce > 0){
                recTransform.position = recTransform.position + Random.insideUnitSphere * shakingForce;
				shakingForce -= shakeDecay;
			}
			else endShake = true;
		}
		else if(endShake)
		{
			shake = false;
			isShaking = false;
            /*
    		recTransform.position = new Vector3(0, 0, 0);
            recTransform.rotation = originRotation;*/

			endShake = false;
		}
	}
} 