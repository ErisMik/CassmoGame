#pragma strict

private var legs = [];
var legsContainer:GameObject;
var raiseTorque:float;
var forwardTorque:float;
var backwardTorque:float;
private var count = 0;

function Start () {
	legs = legsContainer.FindGameObjectsWithTag("SpiderLeg");
	Debug.Log(legs.length);
}

function FixedUpdate () {
	if (count < 6) {
		var leg:GameObject = legs[count];
		leg.rigidbody.AddRelativeTorque(0, 0, raiseTorque * Time.deltaTime);
		leg.rigidbody.AddRelativeTorque(forwardTorque * Time.deltaTime, 0, 0);
		count += 1;
	}
	if (count == 6) {
		for (var i = 0; i < legs.length; i++) {
			var leg_a:GameObject = legs[i];
			leg_a.rigidbody.AddRelativeTorque(-backwardTorque * Time.deltaTime, 0, 0);
		}
		count = 0;
	}
	Debug.Log(count);
}