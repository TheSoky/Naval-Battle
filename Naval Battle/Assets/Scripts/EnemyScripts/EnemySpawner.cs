using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField]
	private List<EnemyMovement> EnemiesToSpawn = new List<EnemyMovement>();
	[SerializeField]
	private Transform EnemiesParent;
	[SerializeField]
	private float SpawnInterval = 15.0f;
	[SerializeField]
	private float MinSpawnDistance = 100.0f;
	[SerializeField]
	private float MaxSpawnDistance = 200.0f;
	[SerializeField]
	private float SpawnHeight = -0.85f;

	private bool _isActive = true;
	private Transform _player;

	public void SetSpawnerActive(bool setter) {
		_isActive = setter;
	}

	private void Start() {
		_player = GameObject.FindWithTag("Player").transform;
		StartCoroutine(Spawner());
	}

	private void SpawnEnemy() {
		Vector3 spawnPosition = Vector3.zero;
		spawnPosition.x = Random.Range(MinSpawnDistance, MaxSpawnDistance);
		if(Random.value > 0.5f) {
			spawnPosition.x *= -1;
		}
		spawnPosition.y = SpawnHeight;
		spawnPosition.z = Random.Range(MinSpawnDistance, MaxSpawnDistance);
		if (Random.value > 0.5f) {
			spawnPosition.z *= -1;
		}

		GameObject enemyClone = Instantiate(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Count)].gameObject, spawnPosition, Quaternion.identity);
		enemyClone.transform.SetParent(EnemiesParent);

		enemyClone.GetComponent<EnemyMovement>().SetTarget(_player);
	}

	private IEnumerator Spawner() {
		while (_isActive) {
			yield return new WaitForSeconds(SpawnInterval);
			SpawnEnemy();
		}
	}

}
