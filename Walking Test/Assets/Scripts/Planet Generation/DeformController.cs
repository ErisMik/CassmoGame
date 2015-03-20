using UnityEngine;
using System.Collections;

// use this to generate a planet around a game object that has a mesh renderer and does not have a meshfilter

public class DeformController : MonoBehaviour {

	public bool create;
	public int recursionLevel;
	public bool deform;
	public float roughness;

	// Use this for initialization
	void Start () {

		if (create) {
			IcoGenerator.Create(gameObject, recursionLevel);
		}
		if(deform) {
			//DeformIco deform = GameObject.FindWithTag("Global").GetComponent<DeformIco>();
			DeformIco.deform(gameObject, roughness);
		}
	
	}
}
