using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : MonoBehaviour {

	[SerializeField]
	private Rigidbody Player;
	[SerializeField]
	private float RotationFactor = 5.0f;

	private RectTransform _sprite;

	private void Awake() {
		_sprite = GetComponent<RectTransform>();
	}

	private void Update() {
		Vector3 rotation = Player.angularVelocity;
		_sprite.rotation = Quaternion.Euler(0.0f, 0.0f, -rotation.y * RotationFactor);
	}


}
