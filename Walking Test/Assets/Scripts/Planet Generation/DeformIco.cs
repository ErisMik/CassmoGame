using UnityEngine;
using System.Collections;

static class DeformIco {

	public static void deform(GameObject sphere, float dist_limit_fraction) {
		Vector3[] tempVerts = sphere.GetComponent<MeshFilter>().mesh.vertices;
		//Random rnd = new Random();
		Vector3 centre = sphere.GetComponent<Renderer>().bounds.center;
		float radius = sphere.GetComponent<Renderer>().bounds.extents.magnitude;
		float minDist = radius - radius * dist_limit_fraction;
		float maxDist = radius + radius * dist_limit_fraction;
		for(int i = 0; i < tempVerts.Length; i++) {
			float newDist = Random.Range(minDist, maxDist);
			float distFromCentre = Vector3.Distance(tempVerts[i], centre);
			float distDiff = distFromCentre - newDist;
			tempVerts[i] = Vector3.MoveTowards(tempVerts[i], centre, distDiff);
		}
		sphere.GetComponent<MeshFilter>().mesh.vertices = tempVerts;
	}
}
