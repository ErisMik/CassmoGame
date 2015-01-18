 /*   
 	This file is part of Cassmo

    Copyright (C) 2014-2015  Peter Fajner & Eric Mikulin

    Cassmo is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Cassmo is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#pragma strict

var BillyWheel:GameObject;
var BillyHead:GameObject;
var Ship:GameObject;
var Camera:GameObject;
var shipActivated:boolean;
var BillyWheelScript:WheelRotation;

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