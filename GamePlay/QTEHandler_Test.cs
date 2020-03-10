using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class QTEHandler_Test : MonoBehaviour {
	[SerializeField] private RockSpawner rockSpawner;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private DeathSequence deathSequence;
	[Space]
	[SerializeField] private Image normalDisplay;
	[SerializeField] private Image addativeDisplay;
	[SerializeField] private Image SpecialDisplay;
	[SerializeField] private TMPro.TextMeshProUGUI passBox;
	[SerializeField] private TMPro.TextMeshProUGUI scoreText;
	[SerializeField] private Slider countdownSlider;
	[Space]
	[SerializeField] private Sprite button_W;
	[SerializeField] private Sprite button_A;
	[SerializeField] private Sprite button_S;
	[SerializeField] private Sprite button_D;
	[SerializeField] private Sprite button_additive;
	[SerializeField] private Sprite button_Shift;
	[Space]
	[SerializeField] private KeyCode[] acceptableKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
	[Space]
	[SerializeField] private int randIndex;
	[SerializeField] private int randIndexSpecial;
	[SerializeField] private float timeToGiveInput = 1f;
	[SerializeField] private float timeToGenerateKey = 0.1f;
	[Space]
	public bool canCountDown = false;
	public bool canGiveInput = false;
	public bool specialInputNeeded = false;
	public bool correctInput = false;
	public bool incorrectInput = false;

	public int score = 0;
	public int totalCorrectInput = 0;

	private float _timeToGiveInput;

	// Use this for initialization
	void Start() {
		if(!rockSpawner) { rockSpawner = FindObjectOfType<RockSpawner>(); }
		if(!playerController) { playerController = FindObjectOfType<PlayerController>(); }
		if(!deathSequence) { deathSequence = FindObjectOfType<DeathSequence>(); }

		passBox.text = "";
		_timeToGiveInput = timeToGiveInput;
		countdownSlider.maxValue = timeToGiveInput;
		StartCoroutine(GenerateRandomKey());
	}

	// Update is called once per frame
	void Update() {
		CountDownTillInput();

		countdownSlider.value = _timeToGiveInput;
		countdownSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, _timeToGiveInput);
	}

	public IEnumerator GenerateRandomKey() {
		normalDisplay.gameObject.SetActive(false);
		addativeDisplay.gameObject.SetActive(false);
		SpecialDisplay.gameObject.SetActive(false);

		yield return new WaitForSeconds(timeToGenerateKey);

		specialInputNeeded = false;
		correctInput = false;
		incorrectInput = false;

		if(rockSpawner.SpawnedRocks[playerController.rockSpawnerIndex].name.Contains("Platform_Large")) {
			specialInputNeeded = true;
		}

		randIndex = Random.Range(0, acceptableKeys.Length);

		DisplayNormalInputSprite();
		DisplaySpecialInputSprite();
		passBox.text = "";

		canGiveInput = true;
		canCountDown = true;
		yield return new WaitForEndOfFrame();
	}

	public void CountDownTillInput() {
		if(!DeathSequence.gameOver) {
			if(canCountDown) { _timeToGiveInput -= Time.deltaTime; }
			if(_timeToGiveInput > Mathf.Epsilon) {
				if(!Input.GetKey(KeyCode.Escape) && !Input.GetMouseButton(0) && Input.anyKeyDown && canGiveInput) {
					if(!specialInputNeeded) {
						CheckForCorrectNormalInput();
					} else {
						CheckForCorrectNormalAndSpecialInput();
					}
				}
			} else {
				SetText(passBox, "Failed!", Color.red);
				ToggleDeathSequence();
			}
		}
	}

	private void CheckForCorrectNormalInput() {
		if(!DeathSequence.gameOver) {
			if(Input.GetKeyDown(acceptableKeys[randIndex])) {
				SetText(passBox, "Correct!", Color.green);

				CalculateNewScore(10);

				totalCorrectInput++;
				correctInput = true;
				incorrectInput = false;
				RestartTimerAndGenerateNewKey();
			} else if(!Input.GetKeyDown(acceptableKeys[randIndex])) {
				SetText(passBox, "Incorrect!", Color.red);
				ToggleDeathSequence();
			}
		}
	}

	private void CheckForCorrectNormalAndSpecialInput() {
		if(!DeathSequence.gameOver) {
			if(Input.GetKeyDown(acceptableKeys[randIndex]) && Input.GetKey(KeyCode.LeftShift)) {
				SetText(passBox, "Correct!", Color.green);

				CalculateNewScore(20);

				totalCorrectInput++;
				correctInput = true;
				incorrectInput = false;
				RestartTimerAndGenerateNewKey();
			} else if(!Input.GetKeyDown(acceptableKeys[randIndex]) && !Input.GetKey(KeyCode.LeftShift)) {
				SetText(passBox, "Incorrect!", Color.red);
				ToggleDeathSequence();
			}
		}
	}

	private void RestartTimerAndGenerateNewKey() {
		_timeToGiveInput = timeToGiveInput;
		canGiveInput = false;
		canCountDown = false;
		StartCoroutine(GenerateRandomKey());
	}

	private void DisplayNormalInputSprite() {
		normalDisplay.gameObject.SetActive(true);
		if(randIndex == 0) {
			normalDisplay.sprite = button_W;
		} else if(randIndex == 1) {
			normalDisplay.sprite = button_A;
		} else if(randIndex == 2) {
			normalDisplay.sprite = button_S;
		} else if(randIndex == 3) {
			normalDisplay.sprite = button_D;
		}
	}

	private void DisplaySpecialInputSprite() {
		if(specialInputNeeded) {
			addativeDisplay.gameObject.SetActive(true);
			SpecialDisplay.gameObject.SetActive(true);
		} else {
			addativeDisplay.gameObject.SetActive(false);
			SpecialDisplay.gameObject.SetActive(false);
		}
	}

	public void SetText(TMPro.TextMeshProUGUI text, string message, Color color) {
		text.color = color;
		text.text = message;
	}

	public void ToggleDeathSequence() {
		StartCoroutine(deathSequence.ToggleGameOverScreen(score, PlayerPrefs.GetInt("HighScore"), totalCorrectInput));

		canCountDown = false;
		specialInputNeeded = false;
		correctInput = false;
		incorrectInput = true;

		normalDisplay.gameObject.SetActive(false);
		addativeDisplay.gameObject.SetActive(false);
		SpecialDisplay.gameObject.SetActive(false);

		Vector2 randomPlayerForce = new Vector2(Random.Range(1f, 2.5f), Random.Range(-2.5f, 2.5f) * Time.deltaTime);

		playerController.rb.simulated = true;
		playerController.animator.SetBool("Jumping", false);
		playerController.animator.SetBool("Jumping", true);
		playerController.rb.AddForce(randomPlayerForce, ForceMode2D.Impulse);

		GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");
		GameObject[] rocksInScene = GameObject.FindGameObjectsWithTag("Rock");

		for(int i = 0; i < groundObjects.Length; i++) {
			groundObjects[i].SetActive(false);
		}

		for(int i = 0; i < rocksInScene.Length; i++) {
			Vector2 randomForce = new Vector2(Random.Range(-3.5f, 3.5f), Random.Range(-3.5f, 3.5f) * Time.deltaTime);
			rocksInScene[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			rocksInScene[i].GetComponent<Rigidbody2D>().AddForce(randomForce, ForceMode2D.Impulse);
		}

	}

	private void CalculateNewScore(int scoreToAdd) {
		float multiplier = totalCorrectInput / 10f;
		float _score = (scoreToAdd * _timeToGiveInput) * multiplier;
		score += (int)_score;
		scoreText.text = "SCORE: " + score.ToString();

		if(score > PlayerPrefs.GetInt("HighScore")) {
			PlayerPrefs.SetInt("HighScore", score);
		}
	}

}
