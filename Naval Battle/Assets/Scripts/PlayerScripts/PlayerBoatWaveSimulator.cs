using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatWaveSimulator : MonoBehaviour {

	[SerializeField]
	private float TiltFactor = 1.0f;
	[SerializeField]
	private float TiltDurationFactor = 0.5f;

	private Transform _transform;

	private void Awake() {
		_transform = transform;
	}

	private void Update() {
		_transform.Rotate(0.0f, 0.0f, Mathf.Cos(Time.timeSinceLevelLoad * TiltDurationFactor) * TiltFactor * Time.deltaTime);
	}

}
