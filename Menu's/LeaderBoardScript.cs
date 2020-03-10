using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaderBoardScript : MonoBehaviour {

	DBController dBController;

	public TMPro.TMP_InputField nameField;

	private void Start() {
		dBController = FindObjectOfType<DBController>();
	}

	public void LoadScene(int sceneIndex) {
		SceneManager.LoadScene(sceneIndex);
	}

	public void PostScoreToDataBase() {
		StartCoroutine(dBController._PostAnswers(PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetInt("HighScore")));
	}

	public void StoreName() {
		PlayerPrefs.SetString("PlayerName", nameField.text);
	}

}
