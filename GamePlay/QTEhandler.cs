using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QTEhandler : MonoBehaviour {

	public GameObject displayBox;
	public GameObject passBox;
	[Space]
	public int qteGen;
	public int waitForKey;
	public int correctKey;
	public int countDown;

	void Start() {
		displayBox.GetComponent<Text>().text = "";
		passBox.GetComponent<Text>().text = "";
	}

	void Update() {
		if(waitForKey == 0) {
			qteGen = Random.Range(1, 5);
			countDown = 1;
			if(qteGen == 1) {
				waitForKey = 1;
				displayBox.GetComponent<Text>().text = "[W]";
				Debug.Log("[W]");
			}
			if(qteGen == 2) {
				waitForKey = 1;
				displayBox.GetComponent<Text>().text = "[A]";
				Debug.Log("[A]");
			}
			if(qteGen == 3) {
				waitForKey = 1;
				displayBox.GetComponent<Text>().text = "[S]";
				Debug.Log("[S]");
			}
			if(qteGen == 4) {
				waitForKey = 1;
				displayBox.GetComponent<Text>().text = "[D]";
				Debug.Log("[D]");
			}
			StartCoroutine(CountingDown());
		}
		if(qteGen == 1) {
			if(Input.anyKeyDown) {
				if(Input.GetButtonDown("W_Key")) {
					correctKey = 1;
					StartCoroutine(KeyPressing());
				} else {
					correctKey = 2;
					StartCoroutine(KeyPressing());
				}
			}
		}
		if(qteGen == 2) {
			if(Input.anyKeyDown) {
				if(Input.GetButtonDown("A_Key")) {
					correctKey = 1;
					StartCoroutine(KeyPressing());
				} else {
					correctKey = 2;
					StartCoroutine(KeyPressing());
				}
			}
		}
		if(qteGen == 3) {
			if(Input.anyKeyDown) {
				if(Input.GetButtonDown("S_Key")) {
					correctKey = 1;
					StartCoroutine(KeyPressing());
				} else {
					correctKey = 2;
					StartCoroutine(KeyPressing());
				}
			}
		}
		if(qteGen == 4) {
			if(Input.anyKeyDown) {
				if(Input.GetButtonDown("D_Key")) {
					correctKey = 1;
					StartCoroutine(KeyPressing());
				} else {
					correctKey = 2;
					StartCoroutine(KeyPressing());
				}
			}
		}
	}

	IEnumerator KeyPressing() {
		qteGen = 5;
		if(correctKey == 1) {
			countDown = 2;
			passBox.GetComponent<Text>().color = Color.green;
			passBox.GetComponent<Text>().text = "Pass";
			Debug.Log("Pass");
			yield return new WaitForSeconds(1.5f);
			correctKey = 0;
			passBox.GetComponent<Text>().text = "";
			displayBox.GetComponent<Text>().text = "";
			yield return new WaitForSeconds(1.5f);
			waitForKey = 0;
			countDown = 1;
		}
		if(correctKey == 2) {
			countDown = 2;
			passBox.GetComponent<Text>().color = Color.red;
			passBox.GetComponent<Text>().text = "Fail";
			Debug.Log("Fail");
			yield return new WaitForSeconds(1.5f);
			correctKey = 0;
			passBox.GetComponent<Text>().text = "";
			displayBox.GetComponent<Text>().text = "";
			yield return new WaitForSeconds(1.5f);
			waitForKey = 0;
			countDown = 1;
		}
	}

	IEnumerator CountingDown() {
		yield return new WaitForSeconds(6.0f);
		if(countDown == 1) {
			qteGen = 5;
			countDown = 2;
			passBox.GetComponent<Text>().color = Color.red;
			passBox.GetComponent<Text>().text = "Fail";
			Debug.Log("Fail");
			yield return new WaitForSeconds(1.5f);
			correctKey = 0;
			passBox.GetComponent<Text>().text = "";
			displayBox.GetComponent<Text>().text = "";
			yield return new WaitForSeconds(1.5f);
			waitForKey = 0;
			countDown = 1;
		}
	}
}
