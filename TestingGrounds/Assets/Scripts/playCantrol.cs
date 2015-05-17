using UnityEngine;
using System.Collections;

public class playCantrol : MonoBehaviour {
	public float MoveSpeed = 10; 
	public float RotateSpeed = 40;
	public float JumpForce = 10;

	public Rigidbody rb;
	public float distToGround;

	bool jump;
		
	void Start() {
		rb = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider>().bounds.extents.y + 0.1f;
	}
		
	void Update () {
		//Lock the cursor
		if(Input.GetKey(KeyCode.Escape)){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		//Check to see if character is touching the ground
		bool grounded = Physics.Raycast(transform.position, -Vector3.up, distToGround);

		// Amount to Move
		float MoveForward = Input.GetAxis("Vertical")*MoveSpeed*Time.deltaTime;
		float MoveLeft = Input.GetAxis("Horizontal")*MoveSpeed*Time.deltaTime;
		float MoveRotate = Input.GetAxis ("Mouse X") * RotateSpeed * Time.deltaTime;

		// Move the player
		Vector3 MoveAmnt = new Vector3(MoveLeft, 0, MoveForward);
		transform.Translate(MoveAmnt);
		transform.Rotate(Vector3.up * MoveRotate);

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
