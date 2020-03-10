using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
	public GameObject pauseMenu;
	public Button continueGameButton;
	public Button quitToMenuButton;
	public Button exitGameButton;

	void Start() {
		continueGameButton = continueGameButton.GetComponent<Button>();
		quitToMenuButton = quitToMenuButton.GetComponent<Button>();
		exitGameButton = exitGameButton.GetComponent<Button>();
		pauseMenu.SetActive(false);
	}

	public void QuitToMenuPress() {
		SceneManager.LoadScene(0);
	}

	public void ContinuePress() {
		pauseMenu.SetActive(false);
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void TogglePauseMenu() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
		}
	}

	void Update() {
		TogglePauseMenu();
		if(!pauseMenu.activeInHierarchy) { Time.timeScale = 1; } else { Time.timeScale = 0; }
	}
}

