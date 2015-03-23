using UnityEngine;
using System.Collections;


/** This script is present in each planet. It first creates the collision meshes and then updates their enabled-ness depending on nearby rigidbodies.
 * 
 */
public class IcoCollision : MonoBehaviour {

	public List<GameObject> nearbyRigidbodies;

	// Use this for initialization
	void Start (GameObject planet) {
		
	}
	
	void checkCollision () {
	}
}
