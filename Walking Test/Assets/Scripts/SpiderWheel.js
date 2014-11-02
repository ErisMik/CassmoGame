#pragma strict

private var wheels = [];
var wheelsContainer:GameObject;
var torque:float;
var torso:GameObject;
var jumpForce:float;
var activated:boolean;
private var count = 0;
private var leftWheels;
private var rightWheels;

function Start () {
	wheels = wheelsContainer.FindGameObjectsWithTag("SpiderWheel");
	leftWheels = [wheels[0] as GameObject, wheels[1] as GameObject, wheels[2] as GameObject];
	rightWheels = [wheels[3] as GameObject, wheels[4] as GameObject, wheels[5] as GameObject];
}

function FixedUpdate () {
	/*if (count < 600) {
		var leg:GameObject = legs[count/100];
		leg.rigidbody.AddRelativeTorque(0, 0, torque * Time.deltaTime);
		leg.rigidbody.AddRelativeTorque(forwardTorque * Time.deltaTime, 0, 0);
		count += 1;
	}
	if (count == 600) {
		for (var i = 0; i < legs.length; i++) {
			var leg_a:GameObject = legs[i];
			leg_a.rigidbody.AddRelativeTorque(-backwardTorque * Time.deltaTime, 0, 0);
		}
		count = 0;
	}
	Debug.Log(count);*/
	var forward = Input.GetAxis("Accelerate");
	var yaw = Input.GetAxis("Yaw");
	var pitch = Input.GetAxis("Pitch");
	var roll = Input.GetAxis("Roll");

	var cyaw = Input.GetAxis("CameraYaw");
	var cpitch = Input.GetAxis("CameraPitch");
	var croll = Input.GetAxis("CameraZoom");

	var jump = Input.GetAxis("Jump");

	/*var oddLegs = [legs[0] as GameObject, legs[2] as GameObject, legs[4] as GameObject];
	var evenLegs = [legs[1] as GameObject, legs[3] as GameObject, legs[5] as GameObject];
	for (var i = 0; i < 3; i++) {
		oddLegs[i].rigidbody.AddRelativeTorque(Vector3(roll * torque * Time.deltaTime, yaw * torque * Time.deltaTime, pitch * torque * Time.deltaTime));
		evenLegs[i].rigidbody.AddRelativeTorque(Vector3(croll * torque * Time.deltaTime, cyaw * torque * Time.deltaTime, cpitch * torque * Time.deltaTime));
	}*/
	if (activated) {
		for (var i = 0; i < 3; i++) {
			//var wheel = wheels[i] as GameObject;
			//wheel.rigidbody.AddRelativeTorque(Vector3(roll * torque * Time.deltaTime, yaw * torque * Time.deltaTime, pitch * torque * Time.deltaTime));

			((leftWheels as Array)[i] as GameObject).rigidbody.AddRelativeTorque(Vector3(-pitch * torque * Time.deltaTime, 0, 0));
			((rightWheels as Array)[i] as GameObject).rigidbody.AddRelativeTorque(Vector3(-pitch * torque * Time.deltaTime, 0, 0));
			((leftWheels as Array)[i] as GameObject).rigidbody.AddRelativeTorque(Vector3(-yaw * torque * Time.deltaTime, 0, 0));
			((rightWheels as Array)[i] as GameObject).rigidbody.AddRelativeTorque(Vector3(yaw * torque * Time.deltaTime, 0, 0));
			if (yaw == 0 && pitch == 0 && roll == 0) {
				((leftWheels as Array)[i] as GameObject).rigidbody.angularVelocity = Vector3.zero;
				((rightWheels as Array)[i] as GameObject).rigidbody.angularVelocity = Vector3.zero;
			}
		}

		// jump
		if (jump != 0 && Physics.Raycast(torso.transform.position, -torso.transform.up, 1)) {
			torso.rigidbody.AddRelativeForce(0, jumpForce * jump, 0);
		}
	}

}