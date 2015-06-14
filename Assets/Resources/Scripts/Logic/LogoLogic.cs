using UnityEngine;
using System.Collections;

public class LogoLogic : MonoBehaviour {

	public float temp;
	
	public float tempInit;

    private float timeDelay;
    private float maxDelay;
	
	public Color color;
	
	public bool down;
	
	void Start(){
		
		temp = tempInit;
		down = true;
        maxDelay = 1.0f;

		
		color = GetComponent<Renderer>().material.color;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		
		if (down) {
			
			color.a = Mathf.Lerp (1, 0, temp / tempInit);
			GetComponent<Renderer>().material.color = color;
			temp -= Time.deltaTime;
			if (temp <= 0) {
				down = false;
				temp = 0;
			}
			
		}
		
		
		if (!down){

            timeDelay += Time.deltaTime;

            if (timeDelay >= maxDelay)
            {
                color.a = Mathf.Lerp(1, 0, temp / tempInit);
                GetComponent<Renderer>().material.color = color;
                temp += Time.deltaTime;

                if (temp > tempInit)
                {
                    Application.LoadLevel("Menu");
                    down = true;
                    temp = tempInit;
                }
            }
			
		}
		
		
	}
}
