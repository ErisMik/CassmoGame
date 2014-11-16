using UnityEngine;
using System.Collections;

public class SetOrbit : MonoBehaviour {

	public GameObject parent;
	public Vector3 direction; // careful with directions! mind placement!
	public double strength; // needed because G is different

	// Use this for initialization
	IEnumerator Start () {
		GameObject uniGravSource = GameObject.FindGameObjectsWithTag("GlobalScripts")[0];
		UniversalGravity properties = uniGravSource.GetComponent(typeof(UniversalGravity)) as UniversalGravity;
		double G = properties.G;
		yield return new WaitForSeconds(0.01f);
		float distance = (parent.transform.position - transform.position).magnitude;
		Vector3 velocity = (float)System.Math.Sqrt((rigidbody.mass + parent.rigidbody.mass) * G * strength / distance) * direction.normalized;
		rigidbody.velocity = velocity;
	}

}