#pragma strict
/* This script implements variable gravity direction. Billy's Wheel has a Constant Force which either points towards
towards the floor of the ship he is on. If no contact with an antigrav floor is detected for 10 frames, he is accelerated
downwards.
*/

private var recentCollision:int = 10;
private var Floor:GameObject;
var ground:GameObject;
var forceAmount:int; //positive

function OnCollisionStay (collision:Collision){
	if (collision.collider.tag == "AntiGravFloor") {
		Floor = collision.gameObject;
		recentCollision = 10;
	};
}

function Start () {
	Floor = ground;
}

function Update () {
	if (recentCollision > 0) {
		recentCollision -= 1;
	}
	else if (recentCollision <= 0) {
		Floor = ground;
	};

	var xDiff = (Floor.transform.position.x - transform.position.x);
	var zDiff = (Floor.transform.position.z - transform.position.z);
	var Force = (Floor.transform.position - transform.position).normalized * forceAmount * Time.smoothDeltaTime;
	rigidbody.AddForce(Force);
	//Debug.Log(Force);

}