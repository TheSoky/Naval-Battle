using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    private Transform Target;
    [SerializeField]
    private Transform ShootingSide;
    [SerializeField]
    private float Speed = 0.75f;
    [SerializeField]
    private float MaxSpeed = 3.5f;
    [SerializeField]
    private float DistanceToKeep = 20.0f;
    [SerializeField]
    private float SlowDownSpeed = 0.5f;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _distanceToTarget;
    private float _timer = 0.0f;
    private float _xSpeed = 0.0f;
    private float _zSpeed = 0.0f;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void FixedUpdate() {
        if (Target == null) {
            return;
        }

        _distanceToTarget = Vector3.Distance(transform.position, Target.position);
        if (_distanceToTarget > DistanceToKeep) {
            _timer = 0.0f;
            _transform.LookAt(Target);
            _rigidbody.AddForce(transform.forward * Speed);
            Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);
            localVelocity.z = Mathf.Clamp(localVelocity.z, 0.0f, MaxSpeed);
            _xSpeed = localVelocity.x;
            _zSpeed = localVelocity.z;
            _rigidbody.velocity = _transform.TransformDirection(localVelocity);
        }
        else {
            Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);
            //_timer = Mathf.Clamp01(_timer);   works without it, this line interrupts lerp below if slow down speed is < 1
            localVelocity.z = Mathf.Lerp(_zSpeed, 0.0f, _timer*SlowDownSpeed);
            localVelocity.x = Mathf.Lerp(_xSpeed, 0.0f, _timer*SlowDownSpeed);
            _rigidbody.velocity = _transform.TransformDirection(localVelocity);
            
            _timer += Time.deltaTime;
        }
    }

    public void SetTarget(Transform newTarget) {
        Target = newTarget;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_transform.position, DistanceToKeep);
    }

}
