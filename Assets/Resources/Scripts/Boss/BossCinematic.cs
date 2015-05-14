using UnityEngine;
using System.Collections;

public class BossCinematic : MonoBehaviour {

    public enum CineBehaviour { IDLE, SHOOT, GETGUN}
    public CineBehaviour behav;
    private float counter = 0;
    private BossStats bStats;
    private BossMove bMove;
    private Vector3 gunPos;
	public GameObject headshotFX;
	// Use this for initialization
	void Start () {
        behav = CineBehaviour.IDLE;
        bStats = GetComponent<BossStats>();
        bMove = GetComponent<BossMove>();

        gunPos = new Vector3( -2.1f, transform.position.y, 12);
        bMove.enabled = false;
        bStats.enabled = false;;
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
					
                    if (counter >= 2)
                    {
						BossShooting();
                        behav = CineBehaviour.GETGUN;
                        counter = 0;
                    }
                } break;

            case CineBehaviour.GETGUN:
                {
                    counter += Time.deltaTime;

                    //transform.Rotate(new Vector3(transform.rotation.x, 180, transform.rotation.z) * Time.deltaTime);
                    transform.rotation = Quaternion.Euler( Vector3.zero);
                    transform.position = Vector3.MoveTowards(transform.position, gunPos, 3f * Time.deltaTime);
                    if (counter >= 3)
                    {
						transform.rotation = Quaternion.Euler( new Vector3(0, 180, 0));
						transform.position = Vector3.MoveTowards(transform.position, new Vector3 (0, 0, 7), 3f * Time.deltaTime);
                    }
                } break;
            default: break;
            }
	}

   	public void BossShooting()
	{
		Instantiate ( headshotFX, (transform.position + new Vector3(-0.5f , 1.3f, -2f)), Quaternion.Euler (new Vector3 (-90, 45, 0)));
	}
}
