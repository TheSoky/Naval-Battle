using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	[SerializeField]
	private float FloatFactor = 0.005f;
	[SerializeField]
	private float SinkingSpeed = 0.5f;

	private List<Rigidbody> _rigidBodies = new List<Rigidbody>();
	private int _floatingLayer = 0;
	private int _sinkingLayer = 1;

	private void Awake() {
		_floatingLayer = LayerMask.NameToLayer("FloatingObject");
		_sinkingLayer = LayerMask.NameToLayer("SinkingObject");
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Cannonball") {

			Rigidbody tRigidBody = other.GetComponent<Rigidbody>();

			if (tRigidBody != null) {
				_rigidBodies.Add(tRigidBody);
				tRigidBody.useGravity = false;
				tRigidBody.velocity = Vector3.zero;
			}
		}
	}

	private void FixedUpdate() {

		foreach (Rigidbody rigidBodyElement in _rigidBodies) {

			if (rigidBodyElement == null) {
				_rigidBodies.Remove(rigidBodyElement); // in case object was destroyed without its collider leaving this trigger
			}

			if (rigidBodyElement.gameObject.layer == _floatingLayer) {          //couldn't do with "switch" because gameObject.layer is const, layermask isn't
				rigidBodyElement.AddForce(Vector3.up * FloatFactor * Mathf.Cos(Time.timeSinceLevelLoad), ForceMode.Impulse);
			}
			else if (rigidBodyElement.gameObject.layer == _sinkingLayer) {
				rigidBodyElement.AddForce(Vector3.down * SinkingSpeed);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		Rigidbody tRigidBody = other.GetComponent<Rigidbody>();

		if (tRigidBody != null) {
			_rigidBodies.Remove(tRigidBody);
			Destroy(tRigidBody.gameObject);
		}
	}
}
