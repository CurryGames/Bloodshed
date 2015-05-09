using UnityEngine;
using System.Collections;

public class DestructibleProp : MonoBehaviour {
	
	public GameObject piece1;
    public GameObject puntuationText;
    public bool rubish;
    private DataLogic dataLogic;
    private int puntuation;
    private PlayerStats playerStats;

    void Start()
    {
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

    }

	public void GetDestroyed()
	{
        GameObject piece1GO = (GameObject)Instantiate(piece1, transform.position, transform.rotation);
        AudioSource audiSor = piece1GO.AddComponent<AudioSource>();

        if (rubish != true)
        {
            dataLogic.Play(dataLogic.glass, audiSor, dataLogic.volumFx);
        }
        else dataLogic.Play(dataLogic.fart, audiSor, dataLogic.volumFx);

        puntuation = 15 * playerStats.multiply;
        playerStats.score += puntuation;
        GameObject pText = (GameObject)Instantiate(puntuationText, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
        TextMesh punText = pText.GetComponent<TextMesh>();
        punText.text = puntuation.ToString();
		Destroy (this.gameObject);
	}
}
