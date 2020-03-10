using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private QTEHandler_Test qte;
	[SerializeField] private RockSpawner rockSpawner;
	[Space]
	[SerializeField] private AudioClip flappingWings;
	[Space]
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private GameObject nextJumpGameobject;
	[SerializeField] private Vector3 distanceToPoint;
	[SerializeField] private Vector3 nextJumpPoint;

	public Animator animator;
	public int rockSpawnerIndex = 0;
	public bool movingToNextPoint = false;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start() {
		animator = this.GetComponent<Animator>();

		if(!rockSpawner) { rockSpawner = FindObjectOfType<RockSpawner>(); }
		if(!qte) { qte = FindObjectOfType<QTEHandler_Test>(); }
		rb = GetComponent<Rigidbody2D>();
		rb.simulated = false;

	}

	// Update is called once per frame
	void Update() {
		if(!DeathSequence.gameOver) {
			if(qte.correctInput && !movingToNextPoint) {
				GetNextJumpPos();
				movingToNextPoint = true;
				qte.correctInput = false;
				animator.SetBool("Jumping", true);
			}
			JumpToNextPos();
		}
	}

	public void GetNextJumpPos() {
		if(rockSpawner != null && rockSpawner.SpawnedRocks.Count != 0) {
			nextJumpGameobject = rockSpawner.SpawnedRocks[rockSpawnerIndex].transform.Find("CharacterJumpPoint").gameObject;
			nextJumpPoint = nextJumpGameobject.transform.position;
			StartCoroutine(rockSpawner.SpawnRocks(rockSpawner.SpawnInterval));

			rockSpawnerIndex++;
		}
	}

	public void JumpToNextPos() {
		if(nextJumpPoint != Vector3.zero) {
			if(movingToNextPoint) {
				// This works... But not well :/
				distanceToPoint = new Vector3(transform.position.x - nextJumpPoint.x, transform.position.y - nextJumpPoint.y, transform.position.z - nextJumpPoint.z);
				transform.position = Vector3.MoveTowards(this.transform.position, nextJumpPoint, moveSpeed * Time.deltaTime);
			}
			if(this.transform.position.y == nextJumpPoint.y && this.transform.position.x == nextJumpPoint.x) {
				animator.SetBool("Jumping", false);
				movingToNextPoint = false;
			}
		}
	}

	private void OnDrawGizmos() {
		if(nextJumpPoint != Vector3.zero) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine(this.transform.position, nextJumpPoint);
		}
	}

	public void PlayAudio() {
		this.GetComponent<AudioSource>().PlayOneShot(flappingWings);
	}
}
