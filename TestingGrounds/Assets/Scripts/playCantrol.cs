using UnityEngine;
using System.Collections;

public class playCantrol : MonoBehaviour {
	public float MoveSpeed = 10; 
	public float RotateSpeed = 40;
	public float JumpForce = 10;

	public Rigidbody rb;
	public float distToGround;

	public GameObject camera;

	bool jump;
	float MoveRotateX;
	float MoveRotateY;
		
	void Start() {
		rb = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider>().bounds.extents.y + 0.1f;
	}
		
	void Update () {
		//Lock the cursor
		if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		//Check to see if character is touching the ground
		bool grounded = Physics.Raycast(transform.position, -Vector3.up, distToGround);

		// Amount to Move
		float MoveForward = Input.GetAxis("Vertical")*MoveSpeed*Time.deltaTime;
		float MoveLeft = Input.GetAxis("Horizontal")*MoveSpeed*Time.deltaTime;
		MoveRotateX = Input.GetAxis ("Mouse X") * RotateSpeed * Time.deltaTime;
		MoveRotateY = Input.GetAxis ("Mouse Y") * RotateSpeed * Time.deltaTime;

		// Move the player
		Vector3 MoveAmnt = new Vector3(MoveLeft, 0, MoveForward);
		transform.Translate(MoveAmnt);

		//If mouse key held down, rotate
		if (Input.GetKey (KeyCode.Mouse1)) {
			transform.Rotate (Vector3.up * MoveRotateX);
		} else if (Input.GetKey (KeyCode.Mouse0)) {
			camera.transform.Rotate (Vector3.up * MoveRotateX, Space.Self);
			camera.transform.Rotate (Vector3.left * MoveRotateY, Space.Self);
		}

		//Check for jump
		if(Input.GetKeyDown(KeyCode.Space) && grounded){
			jump = true;
		}
	}

	void FixedUpdate (){
		//Jump the player
		if (jump) {
			rb.AddForce(new Vector3(0, 1, 0) * JumpForce);
			jump = false;
		}
	}
}
