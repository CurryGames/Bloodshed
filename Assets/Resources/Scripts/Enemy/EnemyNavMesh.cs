
using UnityEngine;
using System.Collections;

public class EnemyNavMesh : MonoBehaviour
{

    public GameObject target;
    public GameObject anim;
    private GameObject legs;
    private NavMeshAgent agent;
    private EnemyMoveBehaviour enemyMove;
    private RangedEnemy enemyRang;
    private EnemyStats enemyStats;
	public enum EnemyType { CHASE, IDDLE, PATROL, IMMOBILE}
	public EnemyType enemyType;
	private float rotationSpeed;
    public bool chasing;
	public bool isStop = true;
	public bool resume = false;
	private bool immobileRange;
    private Animator animationLegs;
	public float patrolTime;
	public bool patroling = false;
    //private bool onPatrol;
    private float patrolCounter;
    private bool behindWall;
	NavMeshHit hit;


    void Start()
    {
		rotationSpeed = 10;
        agent = GetComponent<NavMeshAgent>();
        enemyMove = GetComponent<EnemyMoveBehaviour>();
        foreach (Transform t in anim.transform) if (t.name == "Legs") legs = t.gameObject;
        animationLegs = legs.GetComponent<Animator>();
        enemyRang = GetComponent<RangedEnemy>();
        target = GameObject.FindGameObjectWithTag ("Player");
        setIddle();
        //if (enemyType == EnemyType.PATROL) onPatrol = true;
        behindWall = true;
        //Debug.Log(enemyType);
        //agent.speed = enemyStats.speed;	

    }

    // Update is called once per frame
    void Update()
    {
        //if (onPatrol) enemyType = EnemyType.PATROL;

        if (enemyRang.dist <= 35)
        {
            switch (enemyType)
            {
                case EnemyType.CHASE:
                    {
                        if (chasing)
                        {
                            if (isStop)
                                isStop = false;
                            if (target != null)
                            {
                                agent.SetDestination(target.transform.position);

                                if (!resume)
                                {
                                    agent.Resume();
                                    resume = true;
                                }
                            }

                            if (!OnSight())
                            {
                                agent.stoppingDistance = 0.5f;

                            }
                            else
                            {
                                agent.stoppingDistance = 8;
                                RotateTowards(target.transform);
                            }
                        }
                        else
                        {
                            if (!isStop)
                            {
                                agent.Stop();
                                isStop = true;
                                resume = false;
                            }
                        }

                        if (enemyRang.dist <= agent.stoppingDistance)
                        {
                            setIddle();
                        }
                        else if (enemyRang.dist > agent.stoppingDistance && chasing == true)
                        {
                            setRun();
                        }

                    } break;

                case EnemyType.PATROL:
                    {          
                        RaycastHit hit;
                        if ((enemyRang.dist <= enemyRang.detectDistance) && (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit) && hit.transform.tag == "Player"))
                        {

                            enemyType = EnemyType.CHASE;
                            setIddle();
                            //onPatrol = false;
                            Debug.Log("player not on sight");
                        }
                        else
                        {
                            setRun();
                            Patrol(patrolTime); 
                        }
                    } break;

                case EnemyType.IMMOBILE:
                    {
                        if (enemyRang.dist <= enemyRang.detectDistance)
                        {
                            RotateTowards(target.transform);
                            setIddle();
                        }
                    } break;

            }
        }
		
    }

    public void setRun()
    {
        animationLegs.Play("Legs");
    }

    public void setIddle()
    {
        animationLegs.Play("EnemyIdle");
    }

	public bool OnSight()
	{
        //if (agent.Raycast (target.transform.position, out hit)) 
        RaycastHit hit;
        if ((Physics.Raycast(transform.position, target.transform.position - transform.position, out hit) && hit.transform.tag == "Player"))		
		{
			//Debug.Log ("NOT VISIBLE");
			return true;
		}
		else return false;
	}

	private void RotateTowards (Transform target) 
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}

    public void SetChasing()
    {
            //patroling = false;
            enemyType = EnemyType.CHASE;
            chasing = true; 
    }

	public void Patrol(float duration)
	{
		if (!patroling)
		{
			patrolCounter = duration;
			patroling = true;
		}
		else
		{
            //Debug.Log("patrooool");
            patrolCounter -= Time.deltaTime;
			transform.Translate (Vector3.forward * (agent.speed - 1) * Time.deltaTime);
            if (patrolCounter <= 0) 
			{
				//Debug.Log("rotateeeeeeeee");
				transform.Rotate (0, 180, 0);
				patroling = false;
			}
		}
	}
}
