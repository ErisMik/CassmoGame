using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

	public GameObject centreObject;
	public float radius;
	public float deviation_fraction;
	public int density;

	// Use this for initialization
	void Start () {
		Vector3 centre = centreObject.transform.position;
		for (float lon = 0; lon < 2 * Mathf.PI; lon += (2 * Mathf.PI / density)) {
			Vector3 position = centre + new Vector3(radius, 0, 0);
			GameObject bob = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			bob.transform.position = position;
			bob.transform.RotateAround(centre, Vector3.up, lon);

		}
	}
	
	
}