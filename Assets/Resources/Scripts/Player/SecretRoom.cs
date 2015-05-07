using UnityEngine;
using System.Collections;

public class SecretRoom : MonoBehaviour {

    private float initTemp;
    private float temp;
    private bool visible;
    public GameObject plane;
    private Color color;
    private BoxCollider boxCol;

	// Use this for initialization
	void Start () 
    {
        visible = false;
        temp = 1f;
        initTemp = 1f;
        color = plane.GetComponent<Renderer>().material.color;
        boxCol = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(visible == true)
        {
            color.a = Mathf.Lerp(0F, 1F, temp / initTemp);
            plane.GetComponent<Renderer>().material.color = color;
            temp -= Time.deltaTime;

            if (temp <= 0)
            {
                visible = false;
                temp = 0;
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Player") && visible == false)
        {
            visible = true;
            boxCol.enabled = false;
        }     
    }
}
