using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotatorAroundTarget : MonoBehaviour {

	[SerializeField]
	private float Distance = 2.0f;
	[SerializeField]
	private float XSpeed = 20.0f;
	[SerializeField]
	private float YSpeed = 20.0f;
	[SerializeField]
	private float YMinLimit = -90.0f;
	[SerializeField]
	private float YMaxLimit = 90.0f;
	[SerializeField]
	private float DistanceMin = 10.0f;
	[SerializeField]
	private float DistanceMax = 10.0f;

	private float _rotationYAxis = 0.0f;
	private float _rotationXAxis = 0.0f;
	private float _velocityX = 0.0f;
	private float _velocityY = 0.0f;
	private Transform _target;

	void Start() {
		_target = GameObject.FindWithTag("Player").transform;
		transform.SetParent(_target);

		Vector3 angles = transform.eulerAngles;
		_rotationYAxis = angles.y;
		_rotationXAxis = angles.x;
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	void LateUpdate() {
		if (_target) {

			_velocityX = XSpeed * Input.GetAxis("Mouse X") * Distance;
			_velocityY = YSpeed * Input.GetAxis("Mouse Y");

			_rotationYAxis += _velocityX;
			_rotationXAxis -= _velocityY;
			_rotationXAxis = ClampAngle(_rotationXAxis, YMinLimit, YMaxLimit);
			//Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
			Quaternion toRotation = Quaternion.Euler(_rotationXAxis, _rotationYAxis, 0);
			Quaternion rotation = toRotation;

			Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * 5, DistanceMin, DistanceMax);
			RaycastHit hit;
			if (Physics.Linecast(_target.position, transform.position, out hit)) {
				Distance -= hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance);
			Vector3 position = rotation * negDistance + _target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	private static float ClampAngle(float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}

		return Mathf.Clamp(angle, min, max);
	}
}
