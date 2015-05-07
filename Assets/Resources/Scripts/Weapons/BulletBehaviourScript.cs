using UnityEngine;
using System.Collections;

public class BulletBehaviourScript : MonoBehaviour {
	
	public float speedBullet;
	public float minError = 0.5f;
	public float maxError = 0.5f;
	public enum Shooter { PLAYER, ENEMY}
	public GameObject hitWallFX;
	public Shooter shooter;

	// Use this for initialization
	void Start () {
		transform.Translate (0, 0.5f, 0.4f);
		transform.Rotate (new Vector3 (0f, Random.Range (minError, maxError), 0f));
	}
	
	// Update is called once per frame
	void Update () {
		// Bullet is moving in its Z+ position
		transform.Translate (0, 0, Time.deltaTime * speedBullet);
        

	}

	public void OnTriggerEnter(Collider other)
    {


        // Any case, Destroy bullet if collides with Wall
		if (other.tag == "Wall") 
		{
			GameObject hitWall = (GameObject) Instantiate(hitWallFX, transform.position, transform.rotation); 
			Destroy (hitWall, 0.4f);
			Destroy (gameObject);
		}

        if(other.tag == "ShootableProp")
		{
			ShootableProp destProp = other.GetComponent<ShootableProp>();
			destProp.GetDestroyed();
		}
		}
		
	}


