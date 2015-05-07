using UnityEngine;
using System.Collections;

public class DestructibleProp : MonoBehaviour {
	
	public GameObject piece1;
	

	public void GetDestroyed()
	{
        GameObject piece1GO = (GameObject)Instantiate(piece1, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
