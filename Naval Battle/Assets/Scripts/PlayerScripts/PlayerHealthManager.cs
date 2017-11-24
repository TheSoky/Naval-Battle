using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

	private EnemySpawner _enemySpawner;
	private PlayerMovement _playerMovement;
	private PlayerShoot _playerShoot;
	private int _health;
	private Transform _camera;
	private LevelManager _levelManager;

	private int _sinkingLayer = 1;
	//TODO dodaj canvas za defeat
	private void Awake() {
		_enemySpawner = GetComponent<EnemySpawner>();
		_playerMovement = GetComponent<PlayerMovement>();
		_playerShoot = GetComponent<PlayerShoot>();
		_health = GameHandler.GH.GetHealth();
		_camera = GameObject.FindWithTag("MainCamera").transform;

		_sinkingLayer = LayerMask.NameToLayer("SinkingObject");
		_levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
	}

	public void AddDamage(int amount) {
		GameHandler.GH.AddDamage(amount);
		_health = GameHandler.GH.GetHealth();
		if (_health <= 0) {
			_camera.SetParent(null);
			_enemySpawner.enabled = false;
			_playerMovement.enabled = false;
			_playerShoot.enabled = false;
			gameObject.layer = _sinkingLayer;
			_levelManager.GameLost();
		}
	}

}
