using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/** This script creates the surface triangles. It is passed by the DeformController.
 * 
 */
public class IcoCollision {

	public GameObject planet;
	public Boolean align; // whether to align the vertices of neighbouring triangles

	public IcoCollision (GameObject planet_, Boolean align) {
		planet = planet_;
		this.align = align;
	}


	public GameObject[] createPlanes (Material material) {
		Transform planetTransform = planet.GetComponent<Transform>();
		///Vector3 scale = planetTransform.localScale;
		List<Triangle> triangles = new List<Triangle>();
		Mesh planetMesh = planet.GetComponent<MeshFilter>().mesh;
		for (int i = 0; i < planetMesh.triangles.Length; i += 3)
		{
			Vector3 p1 = planetMesh.vertices[planetMesh.triangles[i + 0]];
			Vector3 p2 = planetMesh.vertices[planetMesh.triangles[i + 1]];
			Vector3 p3 = planetMesh.vertices[planetMesh.triangles[i + 2]];
			triangles.Add (new Triangle(p1, p2, p3));
		}

		// create each triangle object
		GameObject[] triangleObjects = new GameObject[triangles.Count];
		for (int i = 0; i < triangles.Count; i++) {
			Mesh mesh = new Mesh ();
			float extrudeAmount = 50f;
			// includes extruded vertices; currently extrudes towards centre of planet rather than normal to surface
			mesh.vertices = new Vector3[6] {triangles [i].a, triangles [i].b, triangles [i].c, 
				Vector3.MoveTowards(triangles [i].a, planet.transform.position, extrudeAmount), 
				Vector3.MoveTowards(triangles [i].b, planet.transform.position, extrudeAmount), 
				Vector3.MoveTowards(triangles [i].c, planet.transform.position, extrudeAmount)};
			mesh.triangles = new int[] {0, 1, 2,   5, 4, 3,   4, 1, 0,   0, 3, 4,   5, 2, 1,   1, 4, 5,   3, 0, 2,   2, 5, 3}; // top face, bottom face, six triangle side faces
			mesh.uv = new Vector2[0];
			mesh.RecalculateNormals ();
			GameObject triangle = new GameObject ("Triangle " + i);
			MeshFilter meshFilter = triangle.AddComponent<MeshFilter>();
			MeshRenderer meshRenderer = triangle.AddComponent<MeshRenderer>();
			meshFilter.mesh = mesh;
			meshRenderer.material = material;
			//triangle.transform.localScale = scale*1.001F;

			//planet.transform.lossyScale *= 0.99f;
			triangleObjects[i] = triangle;
			fixCentre(triangle);
			triangle.GetComponent<MeshFilter>().mesh.RecalculateBounds();

			// add collider
			MeshCollider meshCollider = triangle.AddComponent<MeshCollider>();
			//BoxCollider meshCollider = triangle.AddComponent<BoxCollider>();
			meshCollider.convex = true;

			// prevent clipping with planet; scale each triangle and move it out
			///triangle.transform.localScale *= 1.03f;
			///float moveOutAmount = -0.01f; // the amount to move the triangle away from the planet, as a fraction of the radius; should be negative
			///triangle.transform.position = Vector3.MoveTowards(triangle.transform.position, planet.GetComponent<Renderer>().bounds.center, moveOutAmount * planet.GetComponent<Renderer>().bounds.extents.magnitude);
		}

		// prevent clipping with planet by scaling down the planet
		float scaleAmount = 0.02f; // in fractions of the radius
		planetTransform.localScale -= new Vector3 (scaleAmount, scaleAmount, scaleAmount);

		// parent the triangles to the planet
		foreach (GameObject triangle in triangleObjects) {
			triangle.transform.parent = planet.transform;
		}

		return triangleObjects;

	}

	struct Triangle {
		public readonly Vector3 a;
		public readonly Vector3 b;
		public readonly Vector3 c;
		public Triangle(Vector3 A, Vector3 B, Vector3 C) {
			this.a = A;
			this.b = B;
			this.c = C;
		}
	}

	// moves the centre of the triangle to its actual centre
	void fixCentre (GameObject triangle) {
				Mesh mesh = triangle.GetComponent<MeshFilter>().mesh;
				Vector3[] verts = mesh.vertices;
				Vector3 middleAB = (verts [0] - verts [1]) * 0.5f + verts [1];
				Vector3 middle = (middleAB - verts [2]) * 0.5f + verts [2];
		for (int i = 0; i < verts.Length; i++) {
						verts[i] -= middle;
				}
				mesh.vertices = verts;
				triangle.transform.position += middle;
		}
}