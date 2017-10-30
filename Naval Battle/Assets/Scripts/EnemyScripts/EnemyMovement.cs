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

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void FixedUpdate() {
        if (Target == null) {
            return;
        }

        float Timer = 0.0f;

        _distanceToTarget = Vector3.Distance(transform.position, Target.position);
        if (_distanceToTarget > DistanceToKeep) {
            Timer = 0.0f;
            _transform.LookAt(Target);
            _rigidbody.AddForce(transform.forward * Speed);
            Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);
            localVelocity.z = Mathf.Clamp(localVelocity.z, 0.0f, MaxSpeed);
            _rigidbody.velocity = _transform.TransformDirection(localVelocity);
        }
        else {
            Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);
            Timer = Mathf.Clamp01(Timer);
            localVelocity.z = Mathf.Lerp(MaxSpeed, 0.0f, Timer*SlowDownSpeed);
            //localVelocity.x = Mathf.Lerp(MaxSpeed, 0.0f, Timer*SlowDownSpeed);
            _rigidbody.velocity = _transform.TransformDirection(localVelocity);
            
            Timer += Time.deltaTime;
        }
    }

    public void SetTarget(Transform newTarget) {
        Target = newTarget;
    }

}
