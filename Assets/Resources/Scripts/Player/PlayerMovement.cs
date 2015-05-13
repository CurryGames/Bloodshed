using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private PlayerStats playerStats;
	public float speed;            // The speed that the player will move at.
    private float grenadesTime;
    private float timer;
    private bool mouse;
	float throwForce;
	private PlayerShooting playerShot;
    private LoadingScreen loadingScreen;
	Vector3 movement;                   // The vector to store the direction of the player's movement.
	//Animator anim;              
    private DataLogic dataLogic;
	public bool onCharge;
    // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	
	void Awake ()
	{
		throwForce = 15;
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");		
		// Set up references.
		//anim = GetComponent <Animator> ();
		playerStats = GetComponent<PlayerStats> ();
		speed = playerStats.speed;
		playerRigidbody = GetComponent <Rigidbody> ();
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreen>();
		playerShot = transform.GetComponent<PlayerShooting> ();
        dataLogic = GameObject.FindGameObjectWithTag("DataLogic").GetComponent<DataLogic>();
		onCharge = false;
	}

	void Update()
	{
		speed = playerStats.speed;

        grenadesTime += Time.deltaTime;

        timer += Time.deltaTime;

        if (Input.GetAxis("Joy X") != 0 || Input.GetAxis("Joy Y") != 0) mouse = false;
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)  mouse = true;

        if (!playerStats.brutalMode)
        {
            //if (Input.GetKeyDown("escape")) Application.Quit ();
            //if (Input.GetKey ("1")) playerShot.weapon = PlayerShooting.Weapon.MELEE;
            if (Input.GetKey(KeyCode.F1) && !loadingScreen.loadCurrentScreen) loadingScreen.loadCurrentScreen = true;
            if (Input.GetKey("1") && playerShot.weapon != PlayerShooting.Weapon.GUN)
            {
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                dataLogic.Play(dataLogic.gunClock, audiSor, dataLogic.volumFx);
                playerShot.weapon = PlayerShooting.Weapon.GUN;
            }
            if (Input.GetKey("2")) playerShot.weapon = PlayerShooting.Weapon.SHOTGUN;
            if (Input.GetKey("3") && dataLogic.riffleActive == true) playerShot.weapon = PlayerShooting.Weapon.RIFLE;
			if (Input.GetKey(KeyCode.Mouse1)) 
			{	
				throwForce += 0.18f;
				if (throwForce >= 30) throwForce = 30;
			}
			if ((Input.GetKeyUp(KeyCode.Mouse1)|| Input.GetAxis("FireJoy") > 0) && playerStats.currentGrenades > 0 && grenadesTime >= 2.0f)
			{
                playerStats.currentGrenades--;
				playerShot.ThrowGrenade(throwForce);
				throwForce = 15;
                grenadesTime = 0;
			}

            if (Input.GetAxis("Mouse ScrollWheel") > 0 || (Input.GetButton("RB"))&& timer > 1.0f) // forward
             {
                 if (playerShot.weapon == PlayerShooting.Weapon.GUN)
                 {
                     AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                     dataLogic.Play(dataLogic.gunClock, audiSor, dataLogic.volumFx);
                     playerShot.weapon = PlayerShooting.Weapon.SHOTGUN;
                 }
                 else if (playerShot.weapon == PlayerShooting.Weapon.SHOTGUN && dataLogic.riffleActive == true)
                 {
                     AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                     dataLogic.Play(dataLogic.gunClock, audiSor, dataLogic.volumFx);
                     playerShot.weapon = PlayerShooting.Weapon.RIFLE;
                 }

                 timer = 0;
             }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 || (Input.GetButton("LB")) && timer > 1.0f) // back
             {
                 if (playerShot.weapon == PlayerShooting.Weapon.SHOTGUN)
                 {
                     AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                     dataLogic.Play(dataLogic.gunClock, audiSor, dataLogic.volumFx);
                     playerShot.weapon = PlayerShooting.Weapon.GUN;
                 }
                 else if (playerShot.weapon == PlayerShooting.Weapon.RIFLE)
                 {
                     AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                     dataLogic.Play(dataLogic.gunClock, audiSor, dataLogic.volumFx);
                     playerShot.weapon = PlayerShooting.Weapon.SHOTGUN;
                 }

                 timer = 0;
             }
        }
    }

	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (!onCharge)
		{
			// Move the player around the scene.
			Move (h, v);
            if (!mouse)
            {
                RotateJoystick();
            }
            else
            {
                // Turn the player to face the mouse cursor.
                Turning();
            }
			
		}

		if (onCharge)
		{
			playerRigidbody.AddRelativeForce (Vector3.forward * 300);
		}

		// Animate the player.
        if (h != 0 || v != 0 || onCharge)
        {
            playerStats.setRun();
        }
        else playerStats.setIddle();
		//Animating (h, v);
	}
	
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}
	
	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;
			
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;
			
			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			
			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);;
		}
	}

    void RotateJoystick()
    {
        float horizontalSpeed = 1.0F;
        float verticalSpeed = 1.0F;
   
        float h = horizontalSpeed * Input.GetAxis("Joy X");
        float v = verticalSpeed * Input.GetAxis("Joy Y");

        if (h != 0 || v != 0)
        {
            Vector3 moveRotation = new Vector3(h, 0, v);

            Quaternion newRotation = Quaternion.LookRotation(moveRotation);

            playerRigidbody.MoveRotation(newRotation); ;
        }
    }
    
        

    
	
	/*void Animating (float h, float v)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = h != 0f || v != 0f;
		
		// Tell the animator whether or not the player is walking.
		anim.SetBool ("IsWalking", walking);
	}*/

}