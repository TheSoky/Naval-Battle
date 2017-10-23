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
        Rigidbody tRigidBody = other.GetComponent<Rigidbody>();

        if (tRigidBody != null) {
            _rigidBodies.Add(tRigidBody);
        }
    }

    private void FixedUpdate() {

        foreach (Rigidbody rigidBodyElement in _rigidBodies) {

            if (rigidBodyElement == null) {
                _rigidBodies.Remove(rigidBodyElement); // posprema rigidbody liste ako je objekt unisten bez da je collider izasao iz trigera
            }

            if (rigidBodyElement.gameObject.layer == _floatingLayer) {          //Nije moglo sa switch jer je gameObject.layer const a nase layermaske nisu
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
