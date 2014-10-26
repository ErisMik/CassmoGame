#pragma strict

public var speed:int;
public var yawSpeed:float;
public var pitchSpeed:float;
public var rollSpeed:float;
public var moveDampening:float; // should be negative
public var spinDampening:float; // should be negative
public var activated:boolean;

function Start () {

}

function FixedUpdate () {
	var forward = Input.GetAxis("Accelerate");
	var yaw = Input.GetAxis("Yaw");
	var pitch = Input.GetAxis("Pitch");
	var roll = Input.GetAxis("Roll");

    if (activated) {
    	rigidbody.AddRelativeForce(forward * speed * Time.deltaTime, 0.0, 0.0);
    	rigidbody.AddRelativeTorque(Vector3(roll * rollSpeed * Time.deltaTime, yaw * yawSpeed * Time.deltaTime, pitch * pitchSpeed * Time.deltaTime));
    }

    if (yaw == 0 && pitch == 0 && roll == 0) rigidbody.angularVelocity *= -spinDampening; //rigidbody.AddTorque(rigidbody.angularVelocity * spinDampening * Time.deltaTime);
    if (forward == 0) rigidbody.velocity *= -moveDampening; //rigidbody.AddForce(rigidbody.velocity * moveDampening * Time.deltaTime);
}