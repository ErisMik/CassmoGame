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

public class SetOrbit : MonoBehaviour {

	public GameObject parent;
	public Vector3 direction; // careful with directions! mind placement!
	public double strength; // needed because G is different
	public Vector3 spin;

	// Use this for initialization
	IEnumerator Start () {
		GameObject uniGravSource = GameObject.FindGameObjectsWithTag("GlobalScripts")[0];
		UniversalGravity properties = uniGravSource.GetComponent(typeof(UniversalGravity)) as UniversalGravity;
		double G = properties.G;
		yield return new WaitForSeconds(0.01f);
		float distance = (parent.transform.position - transform.position).magnitude;
		Vector3 velocity = (float)System.Math.Sqrt((rigidbody.mass + parent.rigidbody.mass) * G * strength / distance) * direction.normalized;
		rigidbody.velocity = velocity;
		rigidbody.angularVelocity = spin;
	}

}