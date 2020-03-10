using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour {
	public GameObject creditsMenu;

	void Start() {
		Time.timeScale = 1;
		creditsMenu.SetActive(false);
	}

	public void ToggleCreditsMenu() {
		creditsMenu.SetActive(!creditsMenu.activeInHierarchy);
	}

	public void StartGamePress() {
		SceneManager.LoadScene(1);
	}

	public void ExitGamePress() {
		Application.Quit();
#if UNITY_EDITOR
		Debug.Log("The game would have closed.");
#endif
	}
}
