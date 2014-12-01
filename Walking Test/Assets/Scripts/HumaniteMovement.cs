﻿using UnityEngine;
using System.Collections;

public class HumaniteMovement : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public bool activated;
	public float torque;
	private ObjectProperties properties;
	private float timeSinceMove; // not actually time; decreases from 1 to 0 logarithmically

	// Use this for initialization
	void Start () {
		properties = GetComponent(typeof(ObjectProperties)) as ObjectProperties;
		timeSinceMove = 1;
	}

	void Move () {
		Vector3 upDirection = properties.gravity;
		var moveVertical = Input.GetAxis("Horizontal");
		var moveHorizontal = Input.GetAxis("Vertical");
		//Vector3 movement = new Vector3(moveHorizontal, 0, 0);

		if (activated) {
			if (moveHorizontal != 0) {
				//rigidbody.drag = 0;
				//rigidbody.angularDrag = 0;
				timeSinceMove = 0.99999f;
			}
			rigidbody.velocity += rigidbody.transform.forward * speed * moveHorizontal;
			//transform.Rotate(Vector3.up * rotateSpeed * moveVertical * Time.smoothDeltaTime, Space.Self);
			transform.up = new Vector3(-upDirection.x, -upDirection.y / 2 + moveVertical, -upDirection.z);
			Debug.Log(transform.up + " " + -upDirection);
		}
	}

	void TorsoRotate () {
		//bummed from http://wiki.unity3d.com/index.php?title=TorqueLookRotation
		Vector3 upDirection = properties.gravity;
		float angleDiff = Vector3.Angle(-transform.up, upDirection);
		if (angleDiff > 0.01) {
			Vector3 cross = Vector3.Cross(-transform.up, upDirection);
			//Vector3 doubleCross = Vector3.Cross(-transform.up, -cross);
			Debug.Log(timeSinceMove);
			rigidbody.AddTorque(cross * Mathf.Sin(angleDiff * Mathf.Deg2Rad) * torque * timeSinceMove * Time.smoothDeltaTime);
		}
	}

	void TorsoRotateKinematic () {
		Vector3 upDirection = properties.gravity;
		//float angleDiff = Vector3.Angle(-transform.up, upDirection);
		Quaternion rotationDir = Quaternion.LookRotation(upDirection);
		//Vector3 rotationAngle = new Vector3(rotationDir.x, 0, rotationDir.z);
		//if (angleDiff > 0.1) {
			//transform.Rotate(Vector3.RotateTowards(-transform.up, upDirection, Mathf.Deg2Rad * angleDiff, 0));
			transform.up = new Vector3(-upDirection.x, -upDirection.y, transform.up.z);
		//transform.up = transform.up; //new Vector3(transform.up.x, transform.up.y, transform.up.z);
		//}
	}

	void Hover () {
		bool onGround = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 2);
		if (onGround) {
			rigidbody.AddForce(1.0001f * -properties.gravity);
		}
	}

	bool GroundCheck () {
		bool onGround = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.5f);
		return onGround;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeSinceMove *= 0.999f;
		//TorsoRotateKinematic();
		Move();
		//Hover();
	}
}