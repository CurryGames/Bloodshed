using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour {

	public float temp;
	
	public float tempInit;
	
	public Color color;
	
	public bool down;
	
	void Start(){
		
		temp = tempInit;
		down = true;
		
		color = GetComponent<Renderer>().material.color;
		
	}
	
	// Update is called once per frame
	void Update () {
				
		if (down) {
			
			color.a = Mathf.Lerp (0, 1, temp / tempInit);
			GetComponent<Renderer>().material.color = color;
			temp -= Time.deltaTime;
			if (temp <= 0) {
				down = false;
				temp = 0;
			}
			
		}
		
		if (!down){
			
			color.a = Mathf.Lerp (0, 1, temp / tempInit);
			GetComponent<Renderer>().material.color = color;
			temp += Time.deltaTime;
			
			if (temp > tempInit) {
				down = true;
				temp = tempInit;
			}
		}
	}
}
