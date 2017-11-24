using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {
	[SerializeField]
	private int Health = 20;

	private DamageManager _damageManager;
	private EnemyMovement _enemyMovement;
	private EnemyShoot _enemyShoot;

	private int _sinkingLayer = 1;

	private void Awake() {
		_damageManager = GetComponent<DamageManager>();
		_enemyMovement = GetComponent<EnemyMovement>();
		_enemyShoot = GetComponent<EnemyShoot>();

		_sinkingLayer = LayerMask.NameToLayer("SinkingObject");
	}

	public void AddDamage(int amount) {
		Health -= amount;
		if (Health <= 0) {
			_damageManager.enabled = false;
			_enemyMovement.enabled = false;
			_enemyShoot.enabled = false;
			gameObject.layer = _sinkingLayer;
		}
	}

}
