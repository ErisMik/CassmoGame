using UnityEngine;
using System.Collections;

public class TorsoStability : MonoBehaviour {

	private ObjectProperties properties;
	public float torque;
	public GameObject wheel;

	// Use this for initialization
	void Start () {
		properties = GetComponent(typeof(ObjectProperties)) as ObjectProperties;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 upDirection = properties.gravity;

		//bummed from http://wiki.unity3d.com/index.php?title=TorqueLookRotation
		Vector3 targetDelta = upDirection;
 
		//get the angle between transform.forward and target delta
		float angleDiff = Vector3.Angle(-transform.up, targetDelta);
 
		// get its cross product, which is the axis of rotation to
		// get from one vector to the other
		Vector3 cross = Vector3.Cross(-transform.up, targetDelta);
 
		// apply torque along that axis according to the magnitude of the angle.
		rigidbody.AddTorque(cross * angleDiff * torque);
	}
}