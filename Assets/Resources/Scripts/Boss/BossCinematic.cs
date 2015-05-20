using UnityEngine;
using System.Collections;

public class BossCinematic : MonoBehaviour {

    public enum CineBehaviour { IDLE, SHOOT, GETGUN, GAMEPLAY}
    public CineBehaviour behav;
    private float counter = 0;
    private BossStats bStats;
    private BossMove bMove;
	private BossCinematic bCine;
    private Vector3 gunPos;
	public GameObject headshotFX;
	public GameObject wifeDead;
	public GameObject wife;
	public GameObject bloodSplash;
	public GameObject machinegun;
	public GameObject bazooka;
    public Animator bossAnim;
	private Collider bossCol;
    private PlayerStats playerStats;
    private DataLogic dataLogic;
	private bool shot = false;
	// Use this for initialization
	void Start () {
        behav = CineBehaviour.IDLE;
        bStats = GetComponent<BossStats>();
        bMove = GetComponent<BossMove>();
		bossCol = GetComponent<BoxCollider> ();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();

        gunPos = new Vector3( -2.1f, transform.position.y, 12);
        bMove.enabled = false;
        bStats.enabled = false;
		bCine = GetComponent<BossCinematic> ();
	}
	
	// Update is called once per frame
	void Update () {
	
            switch (behav)
            {
                case CineBehaviour.IDLE:
                {
					// Set iddle
                    SetIddle();
                    counter += Time.deltaTime;
                    if (counter >= 3)
                    {
						// Set Shooting
                        SetShoot();
                        behav = CineBehaviour.SHOOT;
                        counter = 0;
                    }
                       
                } break;

            case CineBehaviour.SHOOT:
                {
                    counter += Time.deltaTime;
					
                    if (counter >= 2 && counter < 3)
                    {
						if (!shot)
						{
							BossShooting();
							shot = true;
                            Invoke("WifeDead", 0.12f);

                            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                            dataLogic.Play(dataLogic.shootGun, audiSor, dataLogic.volumFx);
						}
						
                       
                    }
					else if (counter >= 3)
					{
						// Set iddle
                        SetIddle();
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
					if (counter >= 1 && counter < 1.5f)
					{
						//set shooting (grab weapon)
				SetGetWeapon();
					}
                    if (counter >= 1.5f)
                    {
						// Set machinegun
                        SetMachinegun();
						DestroyMachinegun();

						transform.rotation = Quaternion.Euler( new Vector3(0, 180, 0));
						transform.position = Vector3.MoveTowards(transform.position, new Vector3 (0, 0, 7), 3f * Time.deltaTime);
						counter = 0;
						behav = CineBehaviour.GAMEPLAY;
                    }
                } break;

			case CineBehaviour.GAMEPLAY:
			{
				counter +=Time.deltaTime;
				if (counter >= 1) 
				{
					bMove.enabled = true;
					bStats.enabled = true;
					bCine.enabled = false;
					bossCol.enabled = true;
				}
			} break;

            default: break;
            }
	}

   	public void BossShooting()
	{
		GameObject bldFx = (GameObject)Instantiate ( headshotFX, (transform.position + new Vector3(-0.5f , 1.3f, -2.5f)), Quaternion.Euler (new Vector3 (-90, 65, 0)));

        Destroy(bldFx, 1.5f);
	}

	public void WifeDead()
	{
		Instantiate ( wifeDead, (new Vector3(-1.37f , 0.5f, 7.77f)), Quaternion.Euler (new Vector3 (90, 0, 0)));
		Destroy (wife, 0);
		Instantiate ( bloodSplash, (new Vector3(-1.37f , 0.2f, 5.77f)), Quaternion.Euler (new Vector3 (90, 0, 0))) ;
	}

	private void DestroyMachinegun()
	{
		Destroy (machinegun, 0);
	}

	private void DestroyBazooka()
	{
		Destroy (bazooka, 0);
	}

    private void SetIddle()
    {
        bossAnim.Play("BossIdle");
    }

    private void SetShoot()
    {
        bossAnim.Play("BossShooting");
    }

	private void SetGetWeapon()
	{
		bossAnim.Play ("BossGetGun");
	}

    private void SetMachinegun()
    {
        bossAnim.Play("BossMachinegun");
        playerStats.audiSorMusic.Stop();
        playerStats.audioSorTension.Stop();
        AudioSource audiSor = playerStats.gameObject.AddComponent<AudioSource>();
        dataLogic.PlayLoop(dataLogic.bossMusic, audiSor, dataLogic.volumMusic);
    }
}
