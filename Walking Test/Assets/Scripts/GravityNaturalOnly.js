#pragma strict
/* This script implements variable gravity direction. Billy's Wheel has a Constant Force which either points towards
towards the floor of the ship he is on. If no contact with an antigrav floor is detected for 10 frames, he is accelerated
downwards.
Currently, planet is default.

This is for moons and other celestial objects that want to be affected by gravity. The main
change is that their mass is calculated, not set manually.
*/

/*

var forceAmount:float; //positive, ideally 1, used for ArtGrav calcs
var mass:float; // this will be set by the script, don't modify
var gravityConstant:float; // recommended 1e-09?
var upDirection:Vector3; // set by script
private var naturalGravitySources = new Array([]);
private var artiGravitySources = new Array([]);
private var naturalDetectionCutoff:int = 500; // Constant for equation to check if natural gravity source is close enough to exert force
private var artificialDetectionCutoff:int = 50; // Constant for equation to check if natural gravity source is close enough to exert force

function GetGravitySources () {
	var tempNaturalGravSources = new Array([]);
	var taggedGSources = GameObject.FindGameObjectsWithTag("GravitySource");
	for (var i = 0; i < taggedGSources.length; i++) {
		var object:GameObject = taggedGSources[i];
		if ((object.transform.position - transform.position).magnitude < naturalDetectionCutoff * object.renderer.bounds.size.x 
			&& object.transform != transform) {
			var mass:float = (3.1415/6) * object.renderer.bounds.size.x * object.renderer.bounds.size.x * object.renderer.bounds.size.z;
			var NPropertyArray:Array = new Array(object, mass);
			tempNaturalGravSources.push(NPropertyArray);
		}
	}
	naturalGravitySources = tempNaturalGravSources;

	var tempArtiGravSources = new Array([]);
	var taggedASources = GameObject.FindGameObjectsWithTag("ArtificialGrav");
	for (var j = 0; j < taggedASources.length; j++) {
		var source:GameObject = taggedASources[j];
		if ((source.transform.position - transform.position).sqrMagnitude < artificialDetectionCutoff * source.renderer.bounds.size.x
			&& source.transform != transform) {
			var cutoff:float = source.GetComponent(GravProperties).cutoff;
			var strength:float = source.GetComponent(GravProperties).strength;
			var GPropertyArray:Array = new Array(source, strength, cutoff);
			tempArtiGravSources.push(GPropertyArray);
		}
	}
	artiGravitySources = tempArtiGravSources;
}

function Start () {
	InvokeRepeating("GetGravitySources", 0, 3.0); // runs every 3 seconds
	mass = (3.1415/6) * rigidbody.mass * renderer.bounds.size.x * renderer.bounds.size.x * renderer.bounds.size.z;
	rigidbody.mass = mass;
}

function FixedUpdate () {

	// for calculating
	var tempUpDirection = Vector3.zero;
	var numOfVectors = 0;

	// Compile list of artificial gravity sources within range
	var activeArtiSources = new Array();
	for (var i = 0; i < artiGravitySources.length; i++) {
		var ASourceArray:Array = artiGravitySources[i];
		var ASource:GameObject = ASourceArray[0]; // hacky solutions to redefine the "objects" as the correct data type, as Unity forgets
		var ACutoff:float = ASourceArray[2];
		if ((ASource.transform.position - transform.position).magnitude < ACutoff) {
			activeArtiSources.push(ASourceArray);
		}
	}
	// If there are artificial gravity sources nearby, apply forces towards them
	if (activeArtiSources.length != 0) {
		for (var j = 0; j < activeArtiSources.length; j++) {
			var aASource:GameObject = ASourceArray[0]; // hacky solutions to redefine the "objects" as the correct data type, as Unity forgets
			var AStrength:float = ASourceArray[1];
			var AForce = (aASource.transform.position - transform.position).normalized * AStrength * forceAmount * Time.smoothDeltaTime;
			rigidbody.AddForce(AForce);

			tempUpDirection += AForce;
			numOfVectors += 1;
		}
	}
	// If no artificial gravity sources nearby, apply forces towards natural ones
	else {
		for (var k = 0; k < naturalGravitySources.length; k++) {
			var GSourceArray:Array = naturalGravitySources[k];
			var GSource:GameObject = GSourceArray[0]; // hacky solutions to redefine the "objects" as the correct data type, as Unity forgets
			var GSourceMass:float = GSourceArray[1];
			var direction = (GSource.transform.position - transform.position);
			var distance = direction.magnitude;
			var NForceAmount:float = gravityConstant * mass * GSourceMass / (distance * distance);
			var NForce:Vector3 = NForceAmount * direction;
			rigidbody.AddForce(NForce);

			tempUpDirection += NForce;
			numOfVectors += 1;
		}
	}
	upDirection = tempUpDirection / numOfVectors;

}

*/