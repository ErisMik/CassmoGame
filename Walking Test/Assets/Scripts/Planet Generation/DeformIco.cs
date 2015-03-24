using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

static class DeformIco {

	public static void deform(GameObject sphere, float dist_limit_fraction) {
		Transform transform = sphere.GetComponent<Transform>();
		Vector3[] tempVerts = sphere.GetComponent<MeshFilter>().mesh.vertices;
		//Random rnd = new Random();
		Vector3 centre = sphere.GetComponent<Renderer>().bounds.center;
		float radius = sphere.GetComponent<Renderer>().bounds.extents.magnitude;
		float minDist = radius - radius * dist_limit_fraction;
		float maxDist = radius + radius * dist_limit_fraction;
		for(int i = 0; i < tempVerts.Length; i++) {
			float newDist = UnityEngine.Random.Range(minDist, maxDist);
			float distFromCentre = Vector3.Distance(tempVerts[i], centre);
			float distDiff = distFromCentre - newDist;
			tempVerts[i] = Vector3.MoveTowards(tempVerts[i], centre, distDiff);
		}
		sphere.GetComponent<MeshFilter>().mesh.vertices = tempVerts;
		transform.localScale = new Vector3 (1, 1, 1);
	}

	/** returns index position of the verts in the verts list
	 DEPRECATED */
	static List<int> getNearVerts(List<Vector3> verts, int vert) {
        Debug.Log("getting near verts, vert = " + vert);
		List<int> nearVerts = new List<int>();
        int sentinel = 5;
		while (nearVerts.Count < 6 && sentinel > 0) {
            sentinel--;
            Debug.Log("entering while loop, sentinel = " + sentinel);
            int nearestVert = 0;
			for(int i = 0; i < verts.Count; i++) {
                Debug.Log("while for loop, i = "+i);
				if (Vector3.Distance(verts[i], verts[vert]) < Vector3.Distance(verts[nearestVert], verts[vert])) {
                    if (verts[i] != verts[vert] && !nearVerts.Contains(i))
                        nearestVert = i;
				}
			}
            nearVerts.Add(nearestVert);
		}
		return nearVerts;
	}

	/** Does not perform the smoothing, only returns a smoothed list of vertices
	 DEPRECATED */
	static List<Vector3> smoothElevation(float factor, int iteration, List<Vector3> verts, int vert, Vector3 centre) {
        Debug.Log("smoothing elevation, iteration = "+iteration);
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

	static List<Vector3> smoothElevationFast(float factor, int iteration, List<Vector3> verts, int peak, Vector3 centre) {
		List<List<int>> sortedVerts = getNearVertsFast (verts, peak, iteration); // remember that these contain indexes of the verts list
		for (int ringNum = 0; ringNum < sortedVerts.Count; ringNum++) {
			for (int vertNum = 0; vertNum < sortedVerts[ringNum].Count; vertNum++) {
				float height = Vector3.Distance (verts[sortedVerts [ringNum] [vertNum]], centre);
				float peakHeight = Vector3.Distance (verts [peak], centre);
				float dHeight = (peakHeight - height) * (float)Math.Pow (factor, ringNum);
				verts [sortedVerts [ringNum][vertNum]] = Vector3.MoveTowards (verts [sortedVerts [ringNum][vertNum]], centre, -dHeight);
			}
		}
		return verts;
	}

	/** Returns a list containing lists. Each list is a "ring" of vertices around the peak, with 0 being the 6 vertices closest to the peak, 1 being the 12 vertices further away, etc
	 * @verts list of vertices
	 * @peak peak position within the list
	 * @numRings the number of rings around the peak to return
	 */
	static List<List<int>> getNearVertsFast(List<Vector3> verts, int peak, int numRings = 3) {

		List<List<int>> vertRings = new List<List<int>>();
		List<List<float>> vertsWithDist = new List<List<float>> ();
		for (int i = 0; i < verts.Count; i++) {
			float distance = Vector3.Distance (verts [peak], verts [i]);
			vertsWithDist.Add (new List<float>() {i, distance});
		}
		vertsWithDist = vertsWithDist.OrderBy(l => l[1]).ToList();
		vertRings.Add (new List<int>(1){(int)vertsWithDist[0][0]});
		int recentVert = 0;
		for (int ringNum = 1; ringNum < numRings; ringNum++) {
			vertRings.Add (new List<int>());
			for (int vertNum = recentVert + 1; vertNum < recentVert + 1 + 6*ringNum; vertNum++) {
				vertRings[ringNum].Add ((int)vertsWithDist[vertNum][0]);
			}
			recentVert = recentVert + 6*ringNum;
			//int max = previousMax + i;
			//List<int> ring = vertsWithDist.Skip(previousMax).Take (max).Select(x => (int)x).ToList ();
			//List<float> ringFloat = vertsWithDist[0].Skip(previousMax).Take (max).ToList ();
			//previousMax += max;
			//vertRings[i] = ring;
			//vertRings.Add (ring);
		}
		return vertRings;
	}

	/** For debugging */
	static void colourVerts(List<List<int>> vertRings, GameObject planet) {
		Mesh mesh = planet.GetComponent<MeshFilter> ().mesh;
		Color[] colours = new Color[mesh.vertices.Length];
		for (int ring = 0; ring < vertRings.Count; ring++) {
			Color ringColour = new Color(UnityEngine.Random.Range(0.0F,1.0F),UnityEngine.Random.Range(0.0F,1.0F),UnityEngine.Random.Range(0.0F,1.0F));
			foreach (int vert in vertRings[ring]) {
				//colours[6 * ring*(ring+1)/2 + 1] = // triangular numbers find the nth vertex in the jagged array, allowing us to iterate;
				colours [vert] = ringColour;
			}
		}
		mesh.colors = colours;
	}



	/** Instead of randomly moving vertices, this tries to makes hills and valleys 
	 @planet the planet object
	 @num number of mountains to create
	 @maxHeight max mountain height as fraction of planet radius
	 @steepness a float 0-1, lower is steeper, 0.5 is 45 degrees; cos of the angle
	 @searchDistance distance to search for vertices
	 @return mountain peaks
	 */
	public static void mountains(GameObject planet, int num, float maxHeight, float steepness, int numSmoothIterations) {
		Transform transform = planet.GetComponent<Transform>();
		Mesh mesh = planet.GetComponent<MeshFilter> ().mesh;
		Vector3[] verts = planet.GetComponent<MeshFilter> ().mesh.vertices;
		List<Vector3> tempVerts = planet.GetComponent<MeshFilter> ().mesh.vertices.ToList();
		Vector3 centre = planet.GetComponent<Renderer> ().bounds.center;
		float radius = planet.GetComponent<Renderer> ().bounds.extents.magnitude;

		for (int i = 0; i < num; i++) {
				int vertNum = UnityEngine.Random.Range (0, verts.Length);
				float height = UnityEngine.Random.Range (0, maxHeight * radius);
				tempVerts[vertNum] = Vector3.MoveTowards(tempVerts[vertNum], centre, -height);
				tempVerts = smoothElevationFast(steepness, numSmoothIterations, tempVerts, vertNum, centre);
		}
		mesh.vertices = tempVerts.ToArray ();
		//transform.localScale = new Vector3 (1, 1, 1);
		//mesh.RecalculateBounds ();
	}


}























