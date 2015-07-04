using UnityEngine;
using System.Collections;
using System;

// use this to generate a planet around a game object that has a mesh renderer and does not have a meshfilter

public class DeformController : MonoBehaviour {

	public bool create;
	public int recursionLevel;
	public bool deform;
	public float roughness; // 0 to 1
	public bool mountains;
	public int numMountains;
	public float mountainMaxHeight; // fraction of radius
	public float mountainSteepness; // cos of angle; 0 to 1, 0 being maximum steepness, 1 being flat
	public bool colour;
	public bool collision; // whether to create the surface collision triangles
	public bool alignTriangles; // whether to align the vertices of neighbouring surface collision triangles
	public Material material; // material to give the surface collision triangles

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
			IcoCollision icoCollision = new IcoCollision (gameObject, alignTriangles);
			trianglePlanes = icoCollision.createPlanes (material);
			IcoTriangleInstancer icoTriangleInstancer = gameObject.GetComponent<IcoTriangleInstancer>();
			icoTriangleInstancer.setProperties(trianglePlanes, 0F, 0F);
		}
		gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds ();
	}
}