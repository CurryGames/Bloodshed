using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossStats : MonoBehaviour {


	public float maxHealthONE;
	public float maxHealthTWO;
	public float maxHealthTHREE;
	public float currentHealth;
    public GameObject blood;
    public GameObject death;
	public GameObject bossHealthBar;
    private PlayerStats playerStats;
	public bool speed;
	bool down = true;
	bool hit = false;
	private DataLogic dataLogic;
	public Color color;
    public GameObject bullseye;
    public GameObject headCol;
	public Slider healthBar1, healthBar2, healthBar3;
    private float deadCounter = 0;
	
	public enum Stage { ONE, TWO, THREE, CRAWL, DEAD}
	public Stage stage;

	// Use this for initialization
	void Awake () {
		stage = Stage.ONE;
		currentHealth = maxHealthONE;
		dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
		//bossHealthBar.SetActive (true);
	
	}

	// Update is called once per frame
	void Update () 
	{

		//bossHealthBar.SetActive (true);

		switch(stage)
		{
		case Stage.ONE:
		
			healthBar1.value = currentHealth/maxHealthONE;
			if(currentHealth <= 0)
			{
				stage = Stage.TWO;
				currentHealth = maxHealthTWO;
			}
		 break;

		case Stage.TWO:	
			healthBar2.value = currentHealth/maxHealthTWO;
			if(currentHealth <= 0)
			{
				stage = Stage.THREE;
				currentHealth = maxHealthTHREE;
			}
		 break;
		 
		case Stage.THREE:
			healthBar3.value = currentHealth/maxHealthTHREE;
			if(currentHealth <= 0)
            {
                stage = Stage.CRAWL;
                GetComponentInChildren<Collider>().enabled = true;
                GetComponent<Collider>().enabled = false;
            }
                
		 break;

		case Stage.CRAWL:
			//crawlTimer += Time.deltaTime;
            bullseye.SetActive(true);
            headCol.SetActive(true);
			break;
		case Stage.DEAD:
            //GetComponent<PlayerMovement>().enabled = false;
            GameObject dead = (GameObject)Instantiate(death.gameObject, transform.position, Quaternion.Euler (new Vector3 (transform.rotation.x, transform.rotation.y, transform.rotation.z)));
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.death, audiSor, dataLogic.volumFx);
            Destroy(this.gameObject);
            //StartCoroutine(EndLevel());
            //Invoke("playerStats.LevelEnd", 1.0f);

   
		break;	
		default: break;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Bullet"))
		{
			Destroy(col.gameObject);
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
			//dataLogic.Play(death, audiSor, dataLogic.volumFx);
			GetDamage(60);			
		}
		
		if ((col.gameObject.tag == "BulletSHOTGUN"))
		{
			Destroy(col.gameObject);
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
			GetDamage(100);	
		} 
		
		if ((col.gameObject.tag == "BulletRIFLE"))
		{
			Destroy(col.gameObject);
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
			GetDamage(60);			
		}

        if ((col.gameObject.tag == "GatlingBullet"))
		{
			Destroy(col.gameObject);
            GameObject bld = (GameObject)Instantiate(blood.gameObject, new Vector3(transform.position.x, 0.2f, transform.position.z), col.transform.rotation);
			GetDamage(100);			
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
        if (stage != BossStats.Stage.DEAD && stage != BossStats.Stage.CRAWL)
        {
            currentHealth -= dmg;
            AudioSource audiSor = dataLogic.gameObject.AddComponent<AudioSource>();
            dataLogic.Play(dataLogic.hit, audiSor, dataLogic.volumFx);
            if (hit == false) hit = true;
        }
        else if (stage == BossStats.Stage.CRAWL)
        {
            stage = BossStats.Stage.DEAD;
        }
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
