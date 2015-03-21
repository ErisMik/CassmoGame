using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static class DeformIco {

	public static int numIterations = 3; // play with this

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

	/** returns index position of the verts in the verts list */
	static List<int> getNearVerts(List<Vector3> verts, int vert) {
		List<int> nearVerts = new List<int>();
		int searchDist = 1;
		while (nearVerts.Count < 6) {
			for(int i = 0; i < verts.Count; i++) {
				if (Vector3.Distance(verts[i], verts[vert]) < searchDist) {
					if (verts[i] != verts[vert] && !nearVerts.Contains(i))
						nearVerts.Add(i);
				}
			}
		}
		return nearVerts;
	}

	/** Does not perform the smoothing, only returns a smoothed list of vertices */
	static List<Vector3> smoothElevation(float factor, int iteration, List<Vector3> verts, int vert, Vector3 centre) {
		iteration--;
		if(iteration > 0) {
			List<int> nearVerts = getNearVerts(verts, vert);
			foreach(int i in nearVerts) {
				float height = Vector3.Distance (verts[i], centre);
				float otherHeight = Vector3.Distance (verts[vert], centre);
				float dHeight = (otherHeight - height) * factor;
				verts[i] = Vector3.MoveTowards(verts[i], centre, -dHeight);
				verts = smoothElevation(factor, iteration, verts, i, centre);
			}
		}
		return verts;
	}


	/** Instead of randomly moving vertices, this tries to makes hills and valleys 
	 @planet the planet object
	 @num number of mountains to create
	 @maxHeight max mountain height as fraction of planet radius
	 @steepness a float 0-1, lower is steeper, 0.5 is 45 degrees; cos of the angle
	 @searchDistance distance to search for vertices
	 */
	public static void mountains(GameObject planet, int num, float maxHeight, float steepness) {
			Mesh mesh = planet.GetComponent<MeshFilter> ().mesh;
			Vector3[] verts = planet.GetComponent<MeshFilter> ().mesh.vertices;
			List<Vector3> tempVerts = planet.GetComponent<MeshFilter> ().mesh.vertices.ToList();
			Vector3 centre = planet.GetComponent<Renderer> ().bounds.center;
			float radius = planet.GetComponent<Renderer> ().bounds.extents.magnitude;

			for (int i = 0; i < num; i++) {
					int vertNum = Random.Range (0, verts.Length);
					float height = Random.Range (0, maxHeight * radius);
					tempVerts[vertNum] = Vector3.MoveTowards(tempVerts[vertNum], centre, -height);
					tempVerts = smoothElevation(steepness, numIterations, tempVerts, vertNum, centre);
			}
			mesh.vertices = tempVerts.ToArray ();
	}


}























