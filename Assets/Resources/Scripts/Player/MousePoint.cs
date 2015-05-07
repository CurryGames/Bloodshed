using UnityEngine;
using System.Collections;

public class MousePoint : MonoBehaviour {

	RaycastHit hit;

	
	// Update is called once per frame
	void Update () {

		GameObject Mirilla = GameObject.Find ("Mirilla");
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 5)){

			if (hit.collider.name == "MousePositionDetector") Mirilla.transform.position = hit.point;
		}
	
		if (Input.GetKey ("f")) gameObject.SetActive (false);
		if (Input.GetKey ("g")) GameObject.Find ("Mirilla").SetActive(true);

	}
}
