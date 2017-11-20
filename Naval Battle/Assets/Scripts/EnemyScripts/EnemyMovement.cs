using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField]
	private Transform Target;
	[SerializeField]
	private float Speed = 0.75f;
	[SerializeField]
	private float TurningChaseSpeed = 1.0f;
	[SerializeField]
	private float TurningAimSpeed = 0.25f;
	[SerializeField]
	private float MaxSpeed = 3.5f;
	[SerializeField]
	private float DistanceToKeep = 20.0f;
	[SerializeField]
	private float SlowDownSpeed = 0.5f;

	private Rigidbody _rigidbody;
	private Transform _transform;
	private EnemyShoot _enemyShoot;
	private float _distanceToTarget;
	private float _timer = 0.0f;
	private float _xSpeed = 0.0f;
	private float _zSpeed = 0.0f;
	private bool _shootingActive;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
		_transform = transform;
		_enemyShoot = GetComponent<EnemyShoot>();
	}

	private void FixedUpdate() {
		if (Target == null) {
			return;
		}

		_distanceToTarget = Vector3.Distance(transform.position, Target.position);
		if (_distanceToTarget > DistanceToKeep) {
			_enemyShoot.StopShooting();
			_shootingActive = false;
			_timer = 0.0f;

			// instead of _transform.LookAt(Target); to get smooth rotation
			_transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(Target.position - _transform.position), TurningChaseSpeed * Time.deltaTime);

			_rigidbody.AddForce(transform.forward * Speed);
			Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);
			localVelocity.z = Mathf.Clamp(localVelocity.z, 0.0f, MaxSpeed);
			localVelocity.x = 0.0f;
			_xSpeed = localVelocity.x;
			_zSpeed = localVelocity.z;
			_rigidbody.velocity = _transform.TransformDirection(localVelocity);
		}
		else {

			Quaternion TargetRotation = Quaternion.LookRotation(Target.position - _transform.position);
			Vector3 RotatorTemp = TargetRotation.eulerAngles;
			RotatorTemp.y += 90.0f;
			TargetRotation = Quaternion.Euler(RotatorTemp);

			_transform.rotation = Quaternion.Lerp(_transform.rotation, TargetRotation, TurningAimSpeed * Time.deltaTime);

			Vector3 localVelocity = _transform.InverseTransformDirection(_rigidbody.velocity);

			//_timer = Mathf.Clamp01(_timer);   works without it, this line interrupts lerp below if slow down speed is < 1
			localVelocity.z = Mathf.Lerp(_zSpeed, 0.0f, _timer * SlowDownSpeed);
			localVelocity.x = Mathf.Lerp(_xSpeed, 0.0f, _timer * SlowDownSpeed);
			_rigidbody.velocity = _transform.TransformDirection(localVelocity);

			_timer += Time.deltaTime;
			if (_timer * TurningAimSpeed > 1 && !_shootingActive) {
				_shootingActive = true;
				_enemyShoot.StartShooting();
			}
		}
	}

	public void SetTarget(Transform newTarget) {
		Target = newTarget;
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, DistanceToKeep);
	}

}
