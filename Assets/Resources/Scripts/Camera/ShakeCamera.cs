using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour 
{	
	private bool shake;
    public bool startShake;	
	//private Vector3 originPosition;
	//private Quaternion originRotation;
    
   
    public bool endShake; // Check if shake is end.
	public bool isShaking; // Check if camera is shaking right now.
	public float shakingForce; // Initial force.
	public float shakeDecay; // Decay force.
	
	// Update is called once per frame
	void Update () 
	{
		//SHAKE
		if(startShake)
		{
			isShaking = true;
			//originPosition = transform.position;
			//originRotation = transform.rotation;
			startShake = false;
			shake = true;
		}
		else if(shake && !endShake)
		{
			if(shakingForce > 0){
				transform.position = transform.position + Random.insideUnitSphere * shakingForce;
				shakingForce -= shakeDecay;
			}
			else endShake = true;
		}
		else if(endShake)
		{
			shake = false;
			isShaking = false;

			//transform.position = originPosition;
			//transform.rotation = originRotation;

			endShake = false;
		}
	}
} 