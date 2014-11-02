#pragma strict

private var legs = [];
var legsContainer:GameObject;
var raiseTorque:float;
var forwardTorque:float;
var backwardTorque:float;
private var count = 0;

function Start () {
	legs = legsContainer.FindGameObjectsWithTag("SpiderLeg");
}

function FixedUpdate () {
	/*if (count < 600) {
		var leg:GameObject = legs[count/100];
		leg.rigidbody.AddRelativeTorque(0, 0, raiseTorque * Time.deltaTime);
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

	var oddLegs = [legs[0] as GameObject, legs[2] as GameObject, legs[4] as GameObject];
	var evenLegs = [legs[1] as GameObject, legs[3] as GameObject, legs[5] as GameObject];
	for (var i = 0; i < 3; i++) {
		oddLegs[i].rigidbody.AddRelativeTorque(Vector3(roll * raiseTorque * Time.deltaTime, yaw * raiseTorque * Time.deltaTime, pitch * raiseTorque * Time.deltaTime));
		evenLegs[i].rigidbody.AddRelativeTorque(Vector3(croll * raiseTorque * Time.deltaTime, cyaw * raiseTorque * Time.deltaTime, cpitch * raiseTorque * Time.deltaTime));
	}

}