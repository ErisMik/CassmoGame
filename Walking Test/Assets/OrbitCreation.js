#pragma strict

var initialForce:Vector3;

function Start () {
	rigidbody.AddForce(initialForce);
}