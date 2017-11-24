using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private GameObject Player;
	[SerializeField]
	private Canvas PauseMenu;
	[SerializeField]
	private Canvas EndGameMenu;

	private void Awake() {
		Instantiate(Player, GameHandler.GH.GetPosition()/*new Vector3(0.0f,0.2f,0.0f)*/, GameHandler.GH.GetRotation());
		PauseMenu.enabled = false;
		EndGameMenu.enabled = false;
	}

	private void Update() {
		if (Input.GetButtonDown("Cancel")) {
			Time.timeScale = 0.0f;
			PauseMenu.enabled = true;
		}
	}

	public void GameLost() {
		EndGameMenu.enabled = true;
	}

	public void Resume() {
		Time.timeScale = 1.0f;
		PauseMenu.enabled = false;
	}

	public void ToMenu() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("MainMenu");
	}

	public void PlayAgain() {
		SceneManager.LoadScene("OpenSea");
	}

}
