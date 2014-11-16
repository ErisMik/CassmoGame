using UnityEngine;
using System.Collections;

public class HumaniteMovement : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public bool activated;
	public float torque;
	private ObjectProperties properties;

	// Use this for initialization
	void Start () {
		properties = GetComponent(typeof(ObjectProperties)) as ObjectProperties;
	}

	void Move () {
		var moveVertical = Input.GetAxis("Horizontal");
		var moveHorizontal = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0, 0);

		if (activated) {
			rigidbody.velocity += rigidbody.transform.forward * speed * moveHorizontal;
			transform.Rotate(Vector3.up * rotateSpeed * moveVertical * Time.smoothDeltaTime, Space.Self);
		}
	}

	void TorsoRotate () {
		//bummed from http://wiki.unity3d.com/index.php?title=TorqueLookRotation
		Vector3 upDirection = properties.gravity;
		float angleDiff = Vector3.Angle(-transform.up, upDirection);
		if (angleDiff > 0) {
			Vector3 cross = Vector3.Cross(-transform.up, upDirection);
			rigidbody.AddTorque(cross * angleDiff * torque);
		}
	}

	void Hover () {
		bool onGround = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 2);
		if (onGround) {
			rigidbody.AddForce(1.0001f * -properties.gravity);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move();
		TorsoRotate();
		Hover();
	}
}