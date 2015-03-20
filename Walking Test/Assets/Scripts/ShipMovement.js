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

public var speed:int;
public var yawSpeed:float;
public var pitchSpeed:float;
public var rollSpeed:float;
public var moveDampening:float; // should be negative
public var spinDampening:float; // should be negative
public var activated:boolean;

function Start () {

}

function FixedUpdate () {
	var forward = Input.GetAxis("Accelerate");
	var yaw = Input.GetAxis("Yaw");
	var pitch = Input.GetAxis("Pitch");
	var roll = Input.GetAxis("Roll");

    if (activated) {
    	GetComponent.<Rigidbody>().AddRelativeForce(forward * speed * Time.deltaTime, 0.0, 0.0);
    	GetComponent.<Rigidbody>().AddRelativeTorque(Vector3(roll * rollSpeed * Time.deltaTime, yaw * yawSpeed * Time.deltaTime, pitch * pitchSpeed * Time.deltaTime));
    }

    if (yaw == 0 && pitch == 0 && roll == 0) GetComponent.<Rigidbody>().angularVelocity *= -spinDampening; //rigidbody.AddTorque(rigidbody.angularVelocity * spinDampening * Time.deltaTime);
    if (forward == 0) GetComponent.<Rigidbody>().velocity *= -moveDampening; //rigidbody.AddForce(rigidbody.velocity * moveDampening * Time.deltaTime);
}