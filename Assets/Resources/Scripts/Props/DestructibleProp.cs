using UnityEngine;
using System.Collections;

public class DestructibleProp : MonoBehaviour {

    public enum PropType { WALL, WATERTAP, RUBISH}
	
	public GameObject piece1;
    public GameObject puntuationText;
    private GameObject pieceGo;
    private GameObject pText;
    private TextMesh punText;
    private AudioSource audiSor;
    private DataLogic dataLogic;
    private int puntuation;
    private PlayerStats playerStats;
    public PropType propType;

    void Start()
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
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
        switch (propType)
        {

            case PropType.WALL:
                dataLogic.Play(dataLogic.breakWall, audiSor, dataLogic.volumFx);
            break;
            case PropType.WATERTAP:
            dataLogic.Play(dataLogic.glass, audiSor, dataLogic.volumFx);
            break;
            case PropType.RUBISH:
            dataLogic.Play(dataLogic.fart, audiSor, dataLogic.volumFx);
            break;

        }

        puntuation = 15 * playerStats.multiply;
        playerStats.score += puntuation;
        
        punText.text = puntuation.ToString();
		Destroy (this.gameObject);
	}
}
