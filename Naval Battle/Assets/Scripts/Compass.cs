using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

    [SerializeField]
    private Transform Camera;

    private RectTransform _sprite;

    private void Awake() {
        _sprite = GetComponent<RectTransform>();
    }

    private void Update() {
        Vector3 rotationEuler = Camera.eulerAngles;
        _sprite.rotation = Quaternion.Euler(0.0f, 0.0f, rotationEuler.y);
    }

}
