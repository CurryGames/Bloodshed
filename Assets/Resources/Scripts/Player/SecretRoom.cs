using UnityEngine;
using System.Collections;
using DaikonForge.Tween.Components;

public class SecretRoom : MonoBehaviour {

    private float initTemp;
    private float temp;
    private bool visible;
    public GameObject plane;
    private Color color;
    private BoxCollider boxCol;
    private DataLogic dataLogic;
    private BackgroundColor tweenMaterial;

	// Use this for initialization
	void Start () 
    {
        visible = false;
        temp = 1f;
        initTemp = 1f;
        color = plane.GetComponent<Renderer>().material.color;
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic"). GetComponent<DataLogic>();
        boxCol = GetComponent<BoxCollider>();

        tweenMaterial = plane.GetComponent<BackgroundColor>();
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
            ShowRoom();
        }
    }

    public void ShowRoom()
    {
        visible = true;
        boxCol.enabled = false;
        tweenMaterial.enabled = false;
        StartCoroutine(Scrolltext());
    }

    IEnumerator Scrolltext()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
        dataLogic.Play(dataLogic.tada, audiSor, dataLogic.volumFx);
       
    }

}
