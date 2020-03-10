using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathSequence : MonoBehaviour {

	public static bool gameOver = false;
	public float timeBeforeToggle = 1f;

	[Header("UI Elements")]
	public GameObject GameOverScreen;
	public TMPro.TextMeshProUGUI score_Text;
	public TMPro.TextMeshProUGUI highScore_Text;
	public TMPro.TextMeshProUGUI totalCorrectInput_Text;

	// Use this for initialization
	void Start() {
		GameOverScreen.SetActive(false);
	}

	private void Update() {
		if(gameOver) {
			if(Input.GetKey(KeyCode.Space)) {
				PlayAgain();
			}
		}
	}

	private void OnDestroy() {
		gameOver = false;
	}

	public IEnumerator ToggleGameOverScreen(int score, int highScore, int totalCorrectInput) {
		gameOver = true;

		yield return new WaitForSeconds(timeBeforeToggle);

		score_Text.text = "Score" + "\n" + score.ToString();
		highScore_Text.text = "High Score" + "\n" + highScore.ToString();
		totalCorrectInput_Text.text = "Total Correct Input" + "\n" + totalCorrectInput.ToString();

		GameOverScreen.SetActive(true);
		yield return null;
	}

	public void PlayAgain() {
		SceneManager.LoadScene("Game");
	}
}
