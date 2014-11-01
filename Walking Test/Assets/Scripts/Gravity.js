#pragma strict
/* This script implements variable gravity direction. Billy's Wheel has a Constant Force which either points towards
towards the floor of the ship he is on. If no contact with an antigrav floor is detected for 10 frames, he is accelerated
downwards.
Currently, planet is default.
*/

private var recentCollision:int = 10;
var floor:GameObject; // current ground
var ground:GameObject; // planet
var forceAmount:int; //positive

function OnCollisionStay (collision:Collision){
	if (collision.collider.tag == "AntiGravFloor") {
		floor = collision.gameObject;
		recentCollision = 100;
	};
}

function Start () {
	floor = ground;
}

function Update () {
	if (recentCollision > 0) {
		recentCollision -= 1;
	}
	else if (recentCollision <= 0) {
		floor = ground;
	};

	var force = (floor.transform.position - transform.position).normalized * forceAmount * Time.smoothDeltaTime;
	rigidbody.AddForce(force);

}