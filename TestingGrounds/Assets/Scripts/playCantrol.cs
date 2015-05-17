using UnityEngine;
using System.Collections;

public class playCantrol : MonoBehaviour {
	public float MoveSpeed = 10; 
	public float RotateSpeed = 40;
	public float JumpForce = 10;

	public Rigidbody rb;

	bool jump;
		
	void Start() {
		rb = GetComponent<Rigidbody>();
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

		// Amount to Move
		float MoveForward = Input.GetAxis("Vertical")*MoveSpeed*Time.deltaTime;
		float MoveLeft = Input.GetAxis("Horizontal")*MoveSpeed*Time.deltaTime;
		float MoveRotate = Input.GetAxis ("Mouse X") * RotateSpeed * Time.deltaTime;

		// Move the player
		Vector3 MoveAmnt = new Vector3(MoveLeft, 0, MoveForward);
		transform.Translate(MoveAmnt);
		transform.Rotate(Vector3.up * MoveRotate);

		//Check for jump
		if(Input.GetKeyDown(KeyCode.Space)){
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
