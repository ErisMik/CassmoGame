using UnityEngine;
using System.Collections;

public static class PlanetDecorator {

	public static void colour(GameObject planet) {
		Vector3 centre = planet.GetComponent<Renderer>().bounds.center;
		
		Mesh mesh = planet.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		float[] heightRange = new float[] {1/0f, -1/0f, 0}; // min, max, range
		//float heightDiff;

		// get min and max distance from centre
		foreach(Vector3 vert in vertices) {
			float centreDist = Vector3.Distance(centre, vert);
			if (centreDist < heightRange[0]) heightRange[0] = centreDist;
			if (centreDist > heightRange[1]) heightRange[1] = centreDist;
		}
		heightRange[2] = heightRange[1] - heightRange[0];

		//Debug.Log (heightRange [2]);

		Color[] colours = new Color[vertices.Length];
		for(int i = 0; i < vertices.Length; i++) {
			float height = Vector3.Distance(centre, vertices[i]);
			float normHeight = (height - heightRange[0])/heightRange[2];
			//colours[i] = Color.Lerp(new Color(0F, 0.3F, 0F, 0F), Color.white, normalizedHeight);

			//if(normHeight < 0.2) colours[i] = new Color(0.3F, 0.3F, 0.9F, 0F);
			if (normHeight < 0.9) colours[i] = new Color(0F, 0.5F, 0F, 0F);
			//else if (normHeight < 0.9) colours[i] = new Color(0.4F, 0.4F, 0.4F, 0F);
			else colours[i] = Color.white;
			//if(i%5 == 0) Debug.Log (normalizedHeight);
		}
		mesh.colors = colours;
	}
}