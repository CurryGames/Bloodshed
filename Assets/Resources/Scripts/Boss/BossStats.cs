using UnityEngine;
using System.Collections;

public class BossStats : MonoBehaviour {


	public int maxHealthONE;
	public int maxHealthTWO;
	public int maxHealthTHREE;
	public int currentHealth;
    public GameObject blood;
    public GameObject death;
    private PlayerStats playerStats;
	public bool speed;
	private float crawlTimer = 0;
	bool down = true;
	bool hit = false;
	private DataLogic dataLogic;
	public Color color;
	
	public enum Stage { ONE, TWO, THREE, CRAWL, DEAD}
	public Stage stage;

	// Use this for initialization
	void Awake () {
		stage = Stage.ONE;
		currentHealth = maxHealthONE;
		dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(stage)
		{
		case Stage.ONE:
		
			if(currentHealth <= 0)
			{
				stage = Stage.TWO;
				currentHealth = maxHealthTWO;
			}
		 break;

		case Stage.TWO:	

			if(currentHealth <= 0)
			{
				stage = Stage.THREE;
				currentHealth = maxHealthTHREE;
			}
		 break;
		 
		case Stage.THREE:
			if(currentHealth <= 0) stage = Stage.CRAWL;
		 break;

		case Stage.CRAWL:
			crawlTimer += Time.deltaTime;
			if (crawlTimer >= 3.5f) stage = Stage.DEAD;
			break;
		case Stage.DEAD:
         playerStats.LevelEnd();
         GameObject dead = (GameObject)Instantiate(death.gameObject, transform.position, Quaternion.identity);
                AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.death, audiSor, dataLogic.volumFx);
         Destroy(this.gameObject);
		break;	
		default: break;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Bullet"))
		{
			Destroy(col.gameObject);        
			//dataLogic.Play(death, audiSor, dataLogic.volumFx);
			GetDamage(60);			
		}
		
		if ((col.gameObject.tag == "BulletSHOTGUN"))
		{
			Destroy(col.gameObject);
			GetDamage(100);	
		} 
		
		if ((col.gameObject.tag == "BulletRIFLE"))
		{
			Destroy(col.gameObject);
			GetDamage(60);			
		}		
	}

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Chainsaw")
		{
			GetDamage(20);
		}
	}
	
	
	public void GetDamage(int dmg)
	{
		currentHealth -= dmg;
        GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3 (transform.position.x, 0.2f, transform.position.z) , Quaternion.identity);
        AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
        dataLogic.Play(dataLogic.hit, audiSor, dataLogic.volumFx);
		if (hit == false) hit = true;
	}

	/*void HitAnim()
	{
		if (down)
		{
			color.g = Mathf.Lerp(0F, 1F, temp / tempIni);
			color.b = Mathf.Lerp(0F, 1F, temp / tempIni);
			enemySprite.GetComponent<Renderer>().material.color = color;
			temp -= Time.deltaTime;
			
			if (temp <= 0)
			{
				down = false;
				temp = 0;
			}
		}
		
		if (!down)
		{
			color.g = Mathf.Lerp(0F, 1F, temp / tempIni);
			color.b = Mathf.Lerp(0F, 1F, temp / tempIni);
			enemySprite.GetComponent<Renderer>().material.color = color;
			temp += Time.deltaTime;
			
			if (temp > tempIni)
			{
				down = true;
				temp = tempIni;
			}
		}
	}*/
}
