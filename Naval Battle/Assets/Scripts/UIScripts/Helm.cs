using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : MonoBehaviour {

	[SerializeField]
	private float RotationFactor = 5.0f;

	private Rigidbody _player;
	private RectTransform _sprite;

	private void Awake() {
		_sprite = GetComponent<RectTransform>();
	}

	private void Start() {
		_player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
	}

	private void Update() {
		Vector3 rotation = _player.angularVelocity;
		_sprite.rotation = Quaternion.Euler(0.0f, 0.0f, -rotation.y * RotationFactor);
	}


}
