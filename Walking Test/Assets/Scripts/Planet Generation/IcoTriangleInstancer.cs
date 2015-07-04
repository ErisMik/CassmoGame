using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Placed inside each planet. Updates enabled triangles based on nearby body positions supplied by global script. */
public class IcoTriangleInstancer : MonoBehaviour {
	
	private GameObject[] nearbyBodies;
	private GameObject[] planes;
	public float updateInterval = 1;
	public float enableDistance = 0.01F; // as fraction of radius

	public void setProperties (GameObject[] planes_, float updateInterval_, float enableDistance_) {
		if (planes_ != null) 
			planes = planes_;
		if (updateInterval_ != 0F)
			updateInterval = updateInterval_;
		if (enableDistance_ != 0F)
			enableDistance = enableDistance_;
	}

	public void updateBodies (GameObject[] bodies) {
		nearbyBodies = bodies;
	}

	// Use this for initialization
	void Start () {

		InvokeRepeating("updateTriangles", 0, 1);
	
	}
	
	// Update is called once per frame
	void updateTriangles () {
		if (nearbyBodies == null || planes == null) // remove the nearbybodies check; at the moment, if nothing is nearby, the whole planet will load
						return;
		foreach (GameObject plane in planes) {
				bool shouldBeEnabled = false;
				foreach (GameObject body in nearbyBodies) {
						if (body == gameObject) break;
						if (Vector3.Distance (body.transform.position, plane.transform.position) < gameObject.GetComponent<Renderer>().bounds.extents.magnitude) {
								shouldBeEnabled = true;
								break;
						}
				}
				if (shouldBeEnabled)
						plane.SetActive(true);
				else
						plane.SetActive(false);
		}

	}
}
