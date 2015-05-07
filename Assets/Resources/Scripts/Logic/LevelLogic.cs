using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {
	
	// PRIVATE DATALOGIC CLASE DATALOGIC
	private DataLogic dataLogic;

	// Use this for initialization
	void Start () {
	
		// 1) BUSCAMOS EN EL ESCENARIO UN OBJETO CON TAG DATALOGIC
		// 2) COGEMOS SU COMPONENTE DATALOGIC Y SE LO ASIGNAMOS
		// A NUESTRA VARIABLE
		dataLogic = GameObject.FindGameObjectWithTag ("DataLogic").
			GetComponent<DataLogic> ();

		// ASIGNAMOS EL NIVEL ACTUAL EL INDICE DEL NIVEL EN EL QUE ESTAMOS
		dataLogic.setCurrentLevel (Application.loadedLevel);

		// ASIGNAMOS EL SIGUIENTE NIVEL
		// teniendo en cuenta:
		// Si es menor que los niveles totales (Application.levelCount) 
		// el indice +1 
		// Si es mayor que los niveles totales (Application.levelCount) 
		// sera el indice 1.
		if (Application.loadedLevel+1 < Application.levelCount)
			dataLogic.setNextLevel(Application.loadedLevel + 1);
		else 
			dataLogic.setNextLevel(2);
        // ASIGNAMOS EL NIVEL ANTERIOR
       
        if (Application.loadedLevel - 1 > 0)
            dataLogic.setBackLevel(Application.loadedLevel - 1);
        else
            dataLogic.setBackLevel(2);
	}
	
	// RELOAD DE CURRENT LEVEL
	public void loadCurrentLevel(){
		// CARGA EL NIVEL ACTUAL DE NUEVO
		Application.LoadLevel (dataLogic.getCurrentLevel ());
	}
	// CARGO EL SIGUIENTE NIVEL
	public void loadNextLevel(){
		// CARGA EL SIGUIENTE NIVEL
		Application.LoadLevel (dataLogic.getNextLevel ());
	}

    public void loadBackLevel()
    {
        // CARGA EL SIGUIENTE NIVEL
        Application.LoadLevel(dataLogic.getBackLevel());
    }

}
