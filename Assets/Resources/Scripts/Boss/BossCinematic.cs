using UnityEngine;
using System.Collections;

public class BossCinematic : MonoBehaviour {

    public enum CineBehaviour { IDLE, SHOOT, GETGUN}
    public CineBehaviour behav;
    private float counter = 0;
    private BossCinematic boss;
    private BossStats bStats;
    private BossMove bMove;
    private Vector3 gunPos;
	// Use this for initialization
	void Start () {
        behav = CineBehaviour.IDLE;
        boss = GetComponent<BossCinematic>();
        bStats = GetComponent<BossStats>();
        bMove = GetComponent<BossMove>();

        gunPos = new Vector3( -2.1f, transform.position.y, 12);
        bMove.enabled = false;
        bStats.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
            switch (behav)
            {
                case CineBehaviour.IDLE:
                {
                    counter += Time.deltaTime;
                    if (counter >= 3)
                    {
                        behav = CineBehaviour.SHOOT;
                        counter = 0;
                    }
                       
                } break;

            case CineBehaviour.SHOOT:
                {
                    counter += Time.deltaTime;
                    Debug.Log("SHOOTING");
                    if (counter >= 2)
                    {
                        behav = CineBehaviour.GETGUN;
                        counter = 0;
                    }
                } break;

            case CineBehaviour.GETGUN:
                {
                    counter += Time.deltaTime;

                    //transform.Rotate(new Vector3(transform.rotation.x, 180, transform.rotation.z) * Time.deltaTime);
                    //Vector3.RotateTowards(transform.forward, (transform.position - gunPos), step, 0.0F);
                    transform.position = Vector3.MoveTowards(transform.position, gunPos, 3f * Time.deltaTime);
                    if (counter >= 1)
                    {
                        boss.enabled = false;
                        counter = 0;
                    }
                } break;
            default: break;
            }
	}

   
}
