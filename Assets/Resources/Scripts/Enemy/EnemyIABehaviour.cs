using UnityEngine;
using System.Collections;

public class EnemyIABehaviour : MonoBehaviour {

	private EnemyMoveBehaviour moveEnemy;

	private EnemyAnimationBehaviour animationEnemy;

	private float currentTime;

	public float currentTimeIni;

	public int life;

	// ESTADOS DE LA IA
	public enum IAEnemyStates {UP, RIGHT, LEFT, DOWN}
	// VARIABLE QUE CONTROLA LOS ESTADOS
	public IAEnemyStates state;

	// Use this for initialization
	void Start () {
		life = 15;
		// BUSCO MI COMPONENTE MOVE EN MI MISMO GAMEOBJECT
		moveEnemy = transform.GetComponent<EnemyMoveBehaviour> ();

		// BUSCO MI COMPONENTE DE ANIMACION EN MI HIJO
		//animationEnemy = transform.FindChild ("Constructor").
			//GetComponent<EnemyAnimationBehaviour> ();

		// EL PRIMER ESTADO ES PATROL
		setUp ();
	}
	
	// Update is called once per frame
	void Update () {
	
		switch (state) {
		
		case IAEnemyStates.UP:
			upBehaviour();
			break;
		case IAEnemyStates.LEFT:
			leftBehaviour();
			break;
		case IAEnemyStates.RIGHT:
			rightBehaviour();
			break;
        case IAEnemyStates.DOWN:
            downBehaviour();
            break;
		
		}

	}

	// SET 
	public void setUp(){
		// la animacion es reposo
		//animationEnemy.setIdle ();
		// El enemigo se mueve hacia a arriba
		moveEnemy.setUp ();

		currentTime = currentTimeIni;

		state = IAEnemyStates.UP;
	}

	public void setLeft(){

		currentTime = currentTimeIni;

		// la animacion es andar
		//animationEnemy.setWalkLeft ();
		// El enemigo se mueve a la izquierda
		moveEnemy.setLeft ();
		
		currentTime = currentTimeIni;
		state = IAEnemyStates.LEFT;
	}

	public void setRight(){

		currentTime = currentTimeIni;

		// la animacion es andar
		//animationEnemy.setWalkRight ();
		// El enemigo se mueve a la derecha
		moveEnemy.setRight ();
		
		currentTime = currentTimeIni;
		state = IAEnemyStates.RIGHT;
	}

    public void setDown()
    {

        currentTime = currentTimeIni;

        // la animacion es andar
        //animationEnemy.setWalkRight ();
        // El enemigo se mueve para abajo
        moveEnemy.setDown();

        currentTime = currentTimeIni;
        state = IAEnemyStates.DOWN;
    }

	// BEHAVIOUR
	private void upBehaviour(){
		currentTime -= Time.deltaTime;

		if(currentTime<0){
			setRight();
		}
	}

	private void leftBehaviour(){
		currentTime -= Time.deltaTime;
		
		if(currentTime<0){
			setUp();
		}
	}

	private void rightBehaviour(){
		currentTime -= Time.deltaTime;
		
		if(currentTime<0){
			setDown ();
		}
	}
    private void downBehaviour()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            setLeft();
        }
    }

	/*public void addDamage(){
		life--;

		animationEnemy.addDamageVisual ();

		if (life < 0) {
		
			Destroy (this.gameObject);
		}
	}*/
}
