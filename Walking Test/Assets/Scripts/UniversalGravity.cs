﻿ /*   
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
using System.Collections.Generic;

public class UniversalGravity : MonoBehaviour {

	public double G; // gravity coefficient
	public float R; // drag coefficient
	public float A; // angular drag coefficient
	public float updateInterval = 1;
	public float bodyDetectDistance = 4; // the distance, in fractions of the radius, at which a rigidbody is made known to a planet's instancer

	//private List<GameObject> rigidbodies = new List<GameObject>();
	private GameObject[] rigidbodies = new GameObject[0];
	private GameObject[] ASources = new GameObject[0];

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateRigidbodies", 0, updateInterval);
		InvokeRepeating("UpdateASources", 0, updateInterval);
		InvokeRepeating("SetMasses", 0, updateInterval);
	}

	// Give every celestial object a mass based on its density and size
	void SetMasses () {
		foreach (GameObject source in rigidbodies) {
			if (source.tag == "CelestialObject") {
				ObjectProperties objprop = source.GetComponent(typeof(ObjectProperties)) as ObjectProperties;
				double density = objprop.density;
				double mass = (3.1415926/6f) * density * source.GetComponent<Renderer>().bounds.size.x * source.GetComponent<Renderer>().bounds.size.z * source.GetComponent<Renderer>().bounds.size.y;
				source.GetComponent<Rigidbody>().mass = (float)mass;

				// give list of nearby rigidbodies to all planets
				Renderer renderer = source.GetComponent<Renderer>();
				List<GameObject> nearBodies = new List<GameObject>();
				foreach (GameObject body in rigidbodies) {
					if (Vector3.Distance (body.transform.position, source.transform.position) < renderer.bounds.extents.magnitude * bodyDetectDistance) {
						nearBodies.Add (body);
					}
				}
				IcoTriangleInstancer instancer = source.GetComponent<IcoTriangleInstancer>();
				if (instancer != null) {
					instancer.updateBodies(nearBodies.ToArray ());
				}
			}
		}
	}

	// Updates list of all artificial gravity sources
	void UpdateASources () {
		ASources = GameObject.FindGameObjectsWithTag("ArtificialGrav");
	}

	// Updates list of all rigidbodies
	void UpdateRigidbodies () {
		List<GameObject> tempRigidbodies = new List<GameObject>();
		GameObject[] allGameObjects = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject source in allGameObjects) {
			if  (source.GetComponent<Rigidbody>() != null && source.activeInHierarchy)
				tempRigidbodies.Add(source);
		}
		rigidbodies = tempRigidbodies.ToArray();
	}

	// Update is called once per frame
	void FixedUpdate () {
		foreach (GameObject body in rigidbodies) {
			float bodyMass = body.GetComponent<Rigidbody>().mass;
			ObjectProperties bodyProperties = body.GetComponent(typeof(ObjectProperties)) as ObjectProperties;
			// Torso rotation directions for humanites
			///Vector3 upDirection = Vector3.zero;
			///int numOfVectors = 0;
			// drag
			Vector3 totalDrag = Vector3.zero;
			// natural gravity attraction
			Vector3 totalGravity = Vector3.zero;
			foreach(GameObject source in rigidbodies) {
				if (source != body) {
					float sourceMass = source.GetComponent<Rigidbody>().mass;
					Vector3 direction = (source.transform.position - body.transform.position).normalized;
					double distance = (source.transform.position - body.transform.position).magnitude;
					Vector3 force = (float)(G * bodyMass * sourceMass / System.Math.Pow(distance, 2)) * direction * Time.smoothDeltaTime;
					///body.rigidbody.AddForce(force);
					totalGravity += force;

					Vector3 drag = (float)(sourceMass / System.Math.Pow(distance, 2)) * direction;
					totalDrag += drag;

					///numOfVectors += 1;
					///upDirection += force;
				}
			}
			// artificial gravity attraction
			foreach(GameObject source in ASources) {
				ObjectProperties sourceProperties = source.GetComponent(typeof(ObjectProperties)) as ObjectProperties;
				double cutoffDist = sourceProperties.cutoff;
				double distance = (source.transform.position - body.transform.position).magnitude;
				if (source != body && distance < cutoffDist) {
					Vector3 direction = (source.transform.position - body.transform.position).normalized;
					Vector3 force = (float)(G * bodyMass * sourceProperties.virtualMass) * direction * Time.smoothDeltaTime;
					///body.rigidbody.AddForce(force);
					totalGravity += force;

					Vector3 drag = (float)(sourceProperties.virtualMass) * direction;
					totalDrag += drag;

					///numOfVectors += 1;
					///upDirection += force;
				}
			}
			///upDirection = upDirection / numOfVectors;
			body.GetComponent<Rigidbody>().AddForce(totalGravity);
			bodyProperties.gravity = totalGravity;
			if (body.tag != "CelestialObject") {
				body.GetComponent<Rigidbody>().drag = R * totalDrag.magnitude;
				body.GetComponent<Rigidbody>().angularDrag = A * totalDrag.magnitude;
			}

			
		}
	}
}
