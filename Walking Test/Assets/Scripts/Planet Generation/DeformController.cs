using UnityEngine;
using System.Collections;

// use this to generate a planet around a game object that has a mesh renderer and does not have a meshfilter

public class DeformController : MonoBehaviour {

	public bool create;
	public int recursionLevel;
	public bool deform;
	public float roughness;
	public bool mountains;
	public int numMountains;
	public float mountainMaxHeight; // fraction of radius
	public float mountainSteepness; // cos of angle
	public bool colour;
	public bool collision;

	// Use this for initialization
	void Start () {


		if (create) {
			IcoGenerator.Create(gameObject, recursionLevel);
		}
		if (deform) {
			//DeformIco deform = GameObject.FindWithTag("Global").GetComponent<DeformIco>();
			DeformIco.deform(gameObject, roughness);
		}
		if (mountains) {
				DeformIco.mountains (gameObject, numMountains, mountainMaxHeight, mountainSteepness, recursionLevel*2);
		}
		if (colour) {
			PlanetDecorator.colour(gameObject);
		}
	
	}
}
