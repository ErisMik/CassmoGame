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
public var rotationSpeed:float;
public var moveDampening:float; // should be negative
public var spinDampening:float; // should be negative
public var activated:boolean;

function Start () {

}

function SlowMove () {
	var avel = GetComponent.<Rigidbody>().angularVelocity;
	GetComponent.<Rigidbody>().AddTorque(Vector3(avel.x*moveDampening* Time.deltaTime, 0.0, avel.z*moveDampening* Time.deltaTime));
}

function SlowRotate () {
	var avel = GetComponent.<Rigidbody>().angularVelocity;
	GetComponent.<Rigidbody>().AddTorque(Vector3(0.0, avel.y*spinDampening* Time.deltaTime, 0.0));
}

function FixedUpdate () {
	var moveVertical = Input.GetAxis("Horizontal");
	var moveHorizontal = Input.GetAxis("Vertical");

	var movement = Vector3(moveHorizontal, 0.0, 0.0);

    if (activated) {
    	GetComponent.<Rigidbody>().AddRelativeTorque(movement * speed * Time.deltaTime);
    	GetComponent.<Rigidbody>().AddTorque(Vector3(0.0, moveVertical * rotationSpeed, 0.0));
    }

    if (moveVertical == 0) SlowRotate();
    if (moveHorizontal == 0) SlowMove();
}