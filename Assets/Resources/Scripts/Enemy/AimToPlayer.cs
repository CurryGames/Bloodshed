using UnityEngine;
using System.Collections;

public class AimToPlayer : MonoBehaviour {

	private GameObject player;
	private EnemyMoveBehaviour enemyMove;
	Quaternion myRotation;
	Quaternion targetRotation;

	// Use this for initialization
	void Awake () 
	{
		player = GameObject.FindWithTag ("Player");
		enemyMove = GetComponentInParent<EnemyMoveBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (enemyMove.chasing) 
		{	
			//transform.rotation = Quaternion.LookRotation(player.transform.position - enemyMove.transform.position);

			//TODO: APPLY LERP
			//transform.rotation = Quaternion.Lerp(enemyMove.transform.position, (player.transform.position - enemyMove.transform.position), Time.deltaTime * 1);
		}
	}
}
