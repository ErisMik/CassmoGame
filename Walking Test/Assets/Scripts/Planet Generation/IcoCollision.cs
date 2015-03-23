using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/** This script is present in each planet. It first creates the collision meshes and then updates their enabled-ness depending on nearby rigidbodies.
 * 
 */
public class IcoCollision {
	
	public List<GameObject> nearbyRigidbodies;
	public GameObject planet;

	public IcoCollision (GameObject planet_) {
		planet = planet_;
	}

	// Use this for initialization
	public void createPlanes (Material material) {
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
			triangle.transform.localScale = scale*1.001F;
		}


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
	
	void checkCollision () {
	}
}