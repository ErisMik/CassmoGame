#pragma strict


/*
private var upDirection:Vector3;
private var properties:ObjectProperties;
var torque:float;
var wheel:GameObject;

function Start () {
}

function Update () {
	//properties = GetComponent(ObjectProperties);
	upDirection = GetComponent(ObjectProperties).gravity;

	//upDirection = wheel.GetComponent(Gravity).upDirection;
}

function FixedUpdate () {

		//upDirection = properties.gravity;

		//bummed from http://wiki.unity3d.com/index.php?title=TorqueLookRotation
		var targetDelta:Vector3 = upDirection;
 
		//get the angle between transform.forward and target delta
		var angleDiff:float = Vector3.Angle(-transform.up, targetDelta);
 
		// get its cross product, which is the axis of rotation to
		// get from one vector to the other
		var cross:Vector3 = Vector3.Cross(-transform.up, targetDelta);
 
		// apply torque along that axis according to the magnitude of the angle.
		rigidbody.AddTorque(cross * angleDiff * torque);
}

*/