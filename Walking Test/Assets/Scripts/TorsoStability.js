#pragma strict

var Ground:GameObject;
var Torque:float;

function Start () {

}

function FixedUpdate () {

		//bummed from http://wiki.unity3d.com/index.php?title=TorqueLookRotation
		var targetDelta:Vector3 = Ground.transform.position - transform.position;
 
		//get the angle between transform.forward and target delta
		var angleDiff:float = Vector3.Angle(-transform.up, targetDelta);
 
		// get its cross product, which is the axis of rotation to
		// get from one vector to the other
		var cross:Vector3 = Vector3.Cross(-transform.up, targetDelta);
 
		// apply torque along that axis according to the magnitude of the angle.
		rigidbody.AddTorque(cross * angleDiff * Torque);
}