using UnityEngine;
using System.Collections;

public class ShootableProp : MonoBehaviour {
	
	public GameObject piece1;
    public GameObject puntuationText;
    private GameObject pieceGo;
    private GameObject pText;
    private TextMesh punText;
    public bool cocaine;
    private DataLogic dataLogic;
    private AudioSource audiSor;
    private AchievementManager achievementManager;
    private int puntuation;
    private PlayerStats playerStats;

    void Start()
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        achievementManager = GameObject.FindGameObjectWithTag("DataLogic").
            GetComponent<AchievementManager>();
        pieceGo = (GameObject)Instantiate(piece1, transform.position, transform.rotation);
        audiSor = pieceGo.AddComponent<AudioSource>();
        pText = (GameObject)Instantiate(puntuationText, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
        punText = pText.GetComponent<TextMesh>();
        pText.SetActive(false);
        pieceGo.SetActive(false);
    }
	public void GetDestroyed()
	{

        pText.SetActive(true);
        pieceGo.SetActive(true);
        if (cocaine != true)
        {
            dataLogic.Play(dataLogic.glass, audiSor, dataLogic.volumFx);
        }
        else dataLogic.Play(dataLogic.balloonPlop, audiSor, dataLogic.volumFx);

        achievementManager.AddProgressToAchievement("Rage Againts the Machine", 1.0f);
        puntuation = 10 * playerStats.multiply;
        playerStats.score += puntuation;
        
        punText.text = puntuation.ToString();
        this.gameObject.SetActive(false);
		//Destroy (this.gameObject);
	}
}
