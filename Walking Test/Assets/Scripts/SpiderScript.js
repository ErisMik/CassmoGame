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