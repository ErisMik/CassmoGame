using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/** This script is present in each planet. It first creates the collision meshes.
 * 
 */
public class IcoCollision {

	public GameObject planet;

	public IcoCollision (GameObject planet_) {
		planet = planet_;
	}

	// Use this for initialization
	public GameObject[] createPlanes (Material material) {
		Transform planetTransform = planet.GetComponent<Transform>();
		Vector3 scale = planetTransform.localScale;
		List<Triangle> triangles = new List<Triangle>();
		Mesh planetMesh = planet.GetComponent<MeshFilter>().mesh;
		for (int i = 0; i < planetMesh.triangles.Length; i += 3)
		{
			Vector3 p1 = planetMesh.vertices[planetMesh.triangles[i + 0]];
			Vector3 p2 = planetMesh.vertices[planetMesh.triangles[i + 1]];
			Vector3 p3 = planetMesh.vertices[planetMesh.triangles[i + 2]];
			triangles.Add (new Triangle(p1, p2, p3));
		}
		GameObject[] triangleObjects = new GameObject[triangles.Count];
		for (int i = 0; i < triangles.Count; i++) {
			Mesh mesh = new Mesh ();
			mesh.name = "Triangle " + i;
			mesh.vertices = new Vector3[3] {triangles [i].a, triangles [i].b, triangles [i].c};
			mesh.triangles = new int[3] {0, 1, 2};
			mesh.uv = new Vector2[0];
			mesh.RecalculateNormals ();
			GameObject triangle = new GameObject ("Triangle " + i);
			MeshFilter meshFilter = triangle.AddComponent<MeshFilter>();
			MeshCollider meshCollider = triangle.AddComponent<MeshCollider>();
			MeshRenderer meshRenderer = triangle.AddComponent<MeshRenderer>();
			meshFilter.mesh = mesh;
			meshCollider.convex = true;
			meshRenderer.material = material;
			//triangle.transform.localScale = scale*1.001F;
			triangle.transform.parent = planet.transform;
			//planet.transform.lossyScale *= 0.99f;
			triangleObjects[i] = triangle;
			fixCentre(triangle);
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

	void fixCentre (GameObject triangle) {
				triangle.transform.localScale *= 1.01f;
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