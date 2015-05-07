
using UnityEngine;
using System.Collections;

public class EnemyNavMesh : MonoBehaviour
{

    public GameObject target;
    //public GameObject legs;
    private NavMeshAgent agent;
    private EnemyMoveBehaviour enemyMove;
    private RangedEnemy enemyRang;
    private EnemyStats enemyStats;
	public enum EnemyType { SPAWN, IDDLE, PATROL, IMMOBILE}
	public EnemyType enemyType;
	private float rotationSpeed;
    public bool chasing;
    private Animator animationLegs;
	NavMeshHit hit;


    void Start()
    {
		rotationSpeed = 10;
        agent = GetComponent<NavMeshAgent>();
        enemyMove = GetComponent<EnemyMoveBehaviour>();
        //animationLegs = legs.GetComponent<Animator>();
        enemyRang = GetComponent<RangedEnemy>();
        target = GameObject.FindGameObjectWithTag ("Player");
        setIddle();

        //agent.speed = enemyStats.speed;	

    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination (target.transform.position);
        /*if (chasing)
        {
			
            if(target!=null){
                Debug.Log("CHASING");
                agent.SetDestination (target.transform.position);
                //agent.SetDestination(new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z));
				
            }
        }*/

        if (chasing)
        {

            if (target != null)
            {
                
                agent.SetDestination(target.transform.position);
				agent.Resume();

                //agent.SetDestination(new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z));
            }

			if (!OnSight ())
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
            agent.Stop();

        }

        if (enemyRang.dist <= agent.stoppingDistance)
        {
            setIddle();
        }
        else if (enemyRang.dist > agent.stoppingDistance && chasing == true)
        {
            setRun();
        }


    }

    public void setRun()
    {
        // animationLegs.Play("Legs");
    }

    public void setIddle()
    {
        // animationLegs.Play("EnemyIdle");
    }

	public bool OnSight()
	{
		if (agent.Raycast (target.transform.position, out hit)) 
		{
			Debug.Log ("NOT VISIBLE");
			return false;
		}
		else return true;
	}

	private void RotateTowards (Transform target) 
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}
}
