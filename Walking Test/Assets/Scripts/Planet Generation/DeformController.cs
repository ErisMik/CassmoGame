using UnityEngine;
using System.Collections;
using System;

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
	public Material material;

	private GameObject[] trianglePlanes;

	// Use this for initialization
	void Start () {



		if (create) {
			IcoGenerator.Create(gameObject, recursionLevel);
		}
		if (deform) {
			//DeformIco deform = GameObject.FindWithTag("Global").GetComponent<DeformIco>();
			DeformIco.deform(gameObject, roughness);
		}
		//gameObject.transform.localScale *= (float)(1f/Math.Pow(recursionLevel, recursionLevel/5f));
		gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds ();
		if (mountains) {
			DeformIco.mountains (gameObject, numMountains, mountainMaxHeight, mountainSteepness, recursionLevel*2);
		}
		if (colour) {
			PlanetDecorator.colour(gameObject);
		}
		if (collision) {
			IcoCollision icoCollision = new IcoCollision (gameObject);
			trianglePlanes = icoCollision.createPlanes (material);
			IcoTriangleInstancer icoTriangleInstancer = gameObject.GetComponent<IcoTriangleInstancer>();
			icoTriangleInstancer.setProperties(trianglePlanes, 0F, 0F);
		}
		gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds ();
	}
}