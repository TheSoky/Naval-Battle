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
    private float TurningSpeed = 1.0f;

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
        Vector3 turningMovement = new Vector3(_movementTurning * TurningSpeed, 0.0f, 0.0f);

        _rigidbody.AddForceAtPosition(turningMovement, TurningPoint.position, ForceMode.Force);
        _rigidbody.AddForce(0.0f, 0.0f, _movementForward * Speed, ForceMode.Force);

        Vector3 Velocity = _rigidbody.velocity;     //nemoguce direktno pristupiti _rigidbody.velocity.z da se clamp-a u limit
        Velocity.z = Mathf.Clamp(Velocity.z, MinSpeed, MaxSpeed);
        _rigidbody.velocity = Velocity;
    }

}
