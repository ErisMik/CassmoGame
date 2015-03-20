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


using UnityEngine;
using System.Collections;

public class TorsoStability : MonoBehaviour {

	private ObjectProperties properties;
	public float torque;
	public GameObject wheel;

	// Use this for initialization
	void Start () {
		properties = GetComponent(typeof(ObjectProperties)) as ObjectProperties;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 upDirection = properties.gravity;

		//bummed from http://wiki.unity3d.com/index.php?title=TorqueLookRotation
		Vector3 targetDelta = upDirection;
 
		//get the angle between transform.forward and target delta
		float angleDiff = Vector3.Angle(-transform.up, targetDelta);
 
		// get its cross product, which is the axis of rotation to
		// get from one vector to the other
		Vector3 cross = Vector3.Cross(-transform.up, targetDelta);
 
		// apply torque along that axis according to the magnitude of the angle.
		GetComponent<Rigidbody>().AddTorque(cross * angleDiff * torque);
	}
}