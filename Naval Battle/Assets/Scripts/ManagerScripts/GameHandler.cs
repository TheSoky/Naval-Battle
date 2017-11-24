using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameHandler : MonoBehaviour {

	[SerializeField]									//game setup
	private int MaxPlayerHealth = 100;
	[SerializeField]
	private Vector3 StartingSpawnLocation = Vector3.zero;
	[SerializeField]
	private Vector3 StartingSpawnRotation = Vector3.zero;
	[SerializeField]
	private int StartingGold = 50;


	private int _playerHealth;
	private Vector3 _playerPosition;
	private Quaternion _playerRotation;
	private string _lastPlayableScene;
	private int _playerGold;

	private float _positionX;							//for serialization of _playerPosition Transform
	private float _positionY;
	private float _positionZ;

	private float _rotationX;
	private float _rotationY;
	private float _rotationZ;

	private static GameHandler instance = null;			//singleton variables
	public static GameHandler GH {
		get {
			return instance;
		}
	}

	//health functions

	public int GetHealth() {
		return _playerHealth;
	}

	public void AddDamage(int amount) {
		_playerHealth -= amount;
	}

	public void RepairShip() {
		_playerHealth = MaxPlayerHealth;
	}

	//mouse status setter

	public void SetMouseVisible(bool isVisible) {
		Cursor.visible = isVisible;
	}

	//loaded files getterss

	public string GetLevel() {
		return _lastPlayableScene;
	}

	public Vector3 GetPosition() {
		return _playerPosition;
	}

	public Quaternion GetRotation() {
		return _playerRotation;
	}

	public void SetDefaultStats() {                 // setting player stats for a new game
		_playerHealth = MaxPlayerHealth;
		_playerPosition = StartingSpawnLocation;
		_playerRotation = Quaternion.Euler(StartingSpawnRotation);
		_lastPlayableScene = "OpenSea";
		_playerGold = StartingGold;
	}

	private void Awake() {
		if(instance != null && instance != this) {		//singleton check so only 1 can exists and it won't be overridden
			Destroy(this.gameObject);
			return;
		}
		else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);

		LoadProgress();

		Cursor.lockState = CursorLockMode.Confined;	

	}



	public void SaveProgress() {						//saving player data to a file
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat");

		TransformComponentsToFloats();

		PlayerStats data = new PlayerStats {
			PlayerHealth = _playerHealth,

			PositionX = _positionX,
			PositionY = _positionY,
			PositionZ = _positionZ,

			RotationX = _rotationX,
			RotationY = _rotationY,
			RotationZ = _rotationZ,

			LastPlayableScene = _lastPlayableScene,
			PlayerGold = _playerGold
		};

		bf.Serialize(file, data);
		file.Close();
	}

	public void LoadProgress() {
		if(File.Exists(Application.persistentDataPath + "/SaveGame.dat")) {				//loading data from save file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat",FileMode.Open);
			PlayerStats data = (PlayerStats)bf.Deserialize(file);
			file.Close();

			_playerHealth = data.PlayerHealth;          // setting player stats based on saved file

			_positionX = data.PositionX;
			_positionY = data.PositionY;
			_positionZ = data.PositionZ;

			_rotationX = data.RotationX;
			_rotationY = data.RotationY;
			_rotationZ = data.RotationZ;

			_lastPlayableScene = data.LastPlayableScene;
			_playerGold = data.PlayerGold;

			FloatsToTransformComponents();
		}
		else {
			SetDefaultStats();
			TransformComponentsToFloats();
		}
	}

	private void TransformComponentsToFloats() {
		_positionX = _playerPosition.x;
		_positionY = _playerPosition.y;
		_positionZ = _playerPosition.z;

		_rotationX = _playerRotation.eulerAngles.x;
		_rotationY = _playerRotation.eulerAngles.y;
		_rotationZ = _playerRotation.eulerAngles.z;
	}

	private void FloatsToTransformComponents() {
		Vector3 position = new Vector3(_positionX, _positionY, _positionZ);
		Quaternion rotation = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);

		_playerPosition = position;
		_playerRotation = rotation;
	}

}

[Serializable]
class PlayerStats {
	public int PlayerHealth;

	public float PositionX;                           //for serialization of _playerPosition Transform
	public float PositionY;
	public float PositionZ;

	public float RotationX;
	public float RotationY;
	public float RotationZ;

	public string LastPlayableScene;
	public int PlayerGold;
}