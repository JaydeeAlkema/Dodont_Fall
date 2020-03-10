using UnityEngine;

public class CloudMovement : MonoBehaviour {

	[SerializeField] private float moveSpeed;
	[SerializeField] private Vector3 movementVector = Vector3.left;

	// Use this for initialization
	void Start() {
		RandomizeSpeed();
	}

	// Update is called once per frame
	void Update() {
		MoveCloud();
		if(this.transform.position.x < Camera.main.transform.position.x - 10) {
			Destroy(this.gameObject, 3f);
		}
	}

	private void RandomizeSpeed() {
		this.moveSpeed = Random.Range(moveSpeed / 2, moveSpeed);
	}

	private void MoveCloud() {
		this.transform.Translate(movementVector * moveSpeed * Time.deltaTime);
	}
}
