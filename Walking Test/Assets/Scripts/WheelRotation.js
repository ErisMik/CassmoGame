#pragma strict

public var speed:int;
public var rotationSpeed:float;
public var moveDampening:float; // should be negative
public var spinDampening:float; // should be negative
public var activated:boolean;

function Start () {

}

function SlowMove () {
	var avel = rigidbody.angularVelocity;
	rigidbody.AddTorque(Vector3(avel.x*moveDampening* Time.deltaTime, 0.0, avel.z*moveDampening* Time.deltaTime));
}

function SlowRotate () {
	var avel = rigidbody.angularVelocity;
	rigidbody.AddTorque(Vector3(0.0, avel.y*spinDampening* Time.deltaTime, 0.0));
}

function FixedUpdate () {
	var moveVertical = Input.GetAxis("Horizontal");
	var moveHorizontal = Input.GetAxis("Vertical");

	var movement = Vector3(moveHorizontal, 0.0, 0.0);

    if (activated) {
    	rigidbody.AddRelativeTorque(movement * speed * Time.deltaTime);
    	rigidbody.AddTorque(Vector3(0.0, moveVertical * rotationSpeed, 0.0));
    }

    if (moveVertical == 0) SlowRotate();
    if (moveHorizontal == 0) SlowMove();
}