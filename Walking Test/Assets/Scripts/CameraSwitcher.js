#pragma strict

var BillyWheel:GameObject;
var BillyHead:GameObject;
var Ship:GameObject;
var Camera:GameObject;
var shipActivated:boolean;
var BillyWheelScript:SpiderWheel;

function Start () {
	shipActivated = false;
}

function Update () {
	if (Input.GetKeyDown("space")) {
		shipActivated = !shipActivated;

		var ShipScript = Ship.GetComponent(ShipMovement);

		if (!shipActivated) {
			Camera.transform.parent = BillyHead.transform;
			Camera.transform.localPosition = Vector3(0, 5, -15);
			var cubeLook:Vector3 = BillyHead.transform.TransformDirection(Vector3.forward);
			Camera.transform.localRotation = Quaternion.Euler(cubeLook.x + 15, cubeLook.y, cubeLook.z);
			Camera.GetComponent(MouseOrbit).target = BillyHead.transform;
			ShipScript.activated = false;
			BillyWheelScript.activated = true;
		}
		if (shipActivated) {
			Camera.transform.parent = Ship.transform;
			Camera.transform.localPosition = Vector3(-10, 5, 0);
			Camera.transform.localRotation = Quaternion.Euler(15, 90, 0);
			Camera.GetComponent(MouseOrbit).target = Ship.transform;
			ShipScript.activated = true;
			BillyWheelScript.activated = false;
		}
	}

}