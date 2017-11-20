using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	[Header("Forward Movement")]
	[SerializeField]
	private float Speed = 1.0f;
	[SerializeField]
	private float MaxSpeed = 3.0f;

	[Header("Turning")]
	[SerializeField]
	private Transform TurningPoint;
	[SerializeField]
	private float TurningSpeed = 0.01f;
	[SerializeField]
	private float MaxTurningSpeed = 1.0f;
	//[SerializeField]
	//private float MaxTurningAngle = 35.0f;

	/*                                                      //Testing Variables
    [Header("TEST Read Only")]  
    public Vector3 CurrentMovement = Vector3.zero;
    public Vector3 CurrentAngularVelocity = Vector3.zero;
    public float MaxZSpeed = 0.0f;
    public float MaxXSpeed = 0.0f;
    public Vector3 LocalMovement = Vector3.zero; 
    */

	private float _movementForward;
	private float _movementTurning;
	private Rigidbody _rigidbody;
	private Transform _transform;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
		_transform = transform;
	}

	private void Update() {
		_movementForward = Input.GetAxisRaw("Vertical");
		_movementTurning = Input.GetAxisRaw("Horizontal");

		_rigidbody.AddForceAtPosition(_transform.right * TurningSpeed * _movementTurning, TurningPoint.position, ForceMode.Impulse);
		_rigidbody.AddForce(_transform.forward * Speed * _movementForward, ForceMode.Force);

		/*Vector3 velocity = _rigidbody.velocity;     //gives global velocity while local coordinates are needed
        velocity.z = Mathf.Clamp(velocity.z, -MaxSpeed, MaxSpeed);
        velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        _rigidbody.velocity = velocity;*/


		Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);
		localVelocity.z = Mathf.Clamp(localVelocity.z, 0.0f, MaxSpeed);
		localVelocity.y = 0;
		localVelocity.x = 0;
		_rigidbody.velocity = _transform.TransformDirection(localVelocity);

		/*
        Vector3 rotation = _transform.localRotation.eulerAngles;
        rotation.z = Mathf.Clamp(rotation.z, -MaxTurningAngle, MaxTurningAngle);
        rotation.x = 0.0f;
        _transform.localRotation = Quaternion.Euler(rotation);
        */

		float MovementZPercent = localVelocity.z / MaxSpeed;

		Vector3 angularVelocity = _rigidbody.angularVelocity;

		angularVelocity.y = Mathf.Clamp(angularVelocity.y, -MaxTurningSpeed * MovementZPercent, MaxTurningSpeed * MovementZPercent);
		angularVelocity.z = 0.0f;
		angularVelocity.x = 0.0f;
		_rigidbody.angularVelocity = angularVelocity;

		/*                                  //testing variable setters
        LocalMovement = localVelocity;
        CurrentAngularVelocity = _rigidbody.angularVelocity;
        CurrentMovement = _rigidbody.velocity;
        if(_rigidbody.velocity.z > MaxZSpeed) {
            MaxZSpeed = _rigidbody.velocity.z;
        }
        if (_rigidbody.velocity.x > MaxXSpeed) {
            MaxXSpeed = _rigidbody.velocity.x;
        }
        */
	}

}
