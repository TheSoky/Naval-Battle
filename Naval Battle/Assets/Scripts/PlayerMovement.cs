using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [Header("Forward Movement")]
    [SerializeField]
    private float Speed = 1.0f;
    [SerializeField]
    private float MinSpeed = 0.0f;
    [SerializeField]
    private float MaxSpeed = 3.0f;

    [Header("Turning")]
    [SerializeField]
    private Transform TurningPoint;
    [SerializeField]
    private float TurningSpeed = 0.01f;
    [SerializeField]
    private float MaxTurningSpeed = 1.0f;
    [SerializeField]
    private float MinTurningSpeed = 0.0f;

    [Header("TEST Read Only")]
    public Vector3 CurrentMovement = Vector3.zero;
    public Vector3 CurrentAngularVelocity = Vector3.zero;
    public float MaxZSpeed = 0.0f;
    public float MaxXSpeed = 0.0f;

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
        _movementForward = Mathf.Clamp(_movementForward, 0.0f, 1.1f);
        _movementTurning = Input.GetAxisRaw("Horizontal");
        Vector3 turningMovement = new Vector3(_movementTurning * TurningSpeed, 0.0f, 0.0f);        

        _rigidbody.AddForceAtPosition(_transform.right * TurningSpeed * _movementTurning, TurningPoint.position, ForceMode.Impulse);
        _rigidbody.AddRelativeForce(_transform.forward * Speed * _movementForward, ForceMode.Force);

        Vector3 velocity = _rigidbody.velocity;     //nemoguce direktno pristupiti _rigidbody.velocity.z da se clamp-a u limit
        velocity.z = Mathf.Clamp(velocity.z, -MaxSpeed, MaxSpeed);
        velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        _rigidbody.velocity = velocity;

        Vector3 angularVelocity = _rigidbody.angularVelocity;
        angularVelocity.y = Mathf.Clamp(angularVelocity.y, MinTurningSpeed, MaxTurningSpeed);
        angularVelocity.z = 0.0f;
        angularVelocity.x = 0.0f;
        _rigidbody.angularVelocity = angularVelocity;

        CurrentAngularVelocity = _rigidbody.angularVelocity;
        CurrentMovement = _rigidbody.velocity;
        if(_rigidbody.velocity.z > MaxZSpeed) {
            MaxZSpeed = _rigidbody.velocity.z;
        }
        if (_rigidbody.velocity.x > MaxXSpeed) {
            MaxXSpeed = _rigidbody.velocity.x;
        }
    }

}
