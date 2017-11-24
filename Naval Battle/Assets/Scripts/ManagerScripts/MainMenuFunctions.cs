using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour {

	//button functions

	public void LoadGame() {
		GameHandler.GH.LoadProgress();
		SceneManager.LoadScene(GameHandler.GH.GetLevel());
	}

	public void StartNewGame() {
		GameHandler.GH.SetDefaultStats();
		GameHandler.GH.SaveProgress();
		SceneManager.LoadScene(GameHandler.GH.GetLevel());
	}

	public void Options() {

	}

	public void Credits() {

	}

	public void ExitGame() {
		Application.Quit();
	}

}
