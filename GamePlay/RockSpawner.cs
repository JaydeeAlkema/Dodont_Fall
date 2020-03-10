using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {
	private bool isSpawningRock = false;

	[SerializeField] private GameObject rockSpawnOrigin; // Position at which the rocks will spawn (Place this as low on the ground as possible!)
	[Space]
	[SerializeField] private List<GameObject> rocksToSpawn = new List<GameObject>(); // List With different kind of rocks to spawn
	[SerializeField] private List<GameObject> spawnedRocks = new List<GameObject>(); // List With different kind of rocks that have been spawned
	[Space]
	[SerializeField] private float spawnInterval = 1.0f;  // Interval is in seconds
	[SerializeField] private float spawnOffset;
	[SerializeField] private int newRockIndex = 0;
	[SerializeField] private int oldRockIndex = 0;

	public float SpawnInterval {
		get { return spawnInterval; }
		set { spawnInterval = value; }
	}

	public List<GameObject> RocksToSpawn {
		get { return rocksToSpawn; }
		set { rocksToSpawn = value; }
	}

	public List<GameObject> SpawnedRocks {
		get { return spawnedRocks; }
		set { spawnedRocks = value; }
	}

	// Update is called once per frame
	void Update() {
		if(spawnedRocks.Count < 12) {
			if(!isSpawningRock) {
				isSpawningRock = true;
				StartCoroutine(SpawnRocks(SpawnInterval));
			}
		}
	}

	public IEnumerator SpawnRocks(float waitTime) {
		oldRockIndex = newRockIndex;
		newRockIndex = Random.Range(0, RocksToSpawn.Count);

		if(newRockIndex == oldRockIndex) {
			newRockIndex = Random.Range(0, RocksToSpawn.Count);
		}

		GameObject rockToSpawn = Instantiate(rocksToSpawn[newRockIndex], rockSpawnOrigin.transform.position, transform.rotation);
		spawnedRocks.Insert(spawnedRocks.Count, rockToSpawn);
		this.transform.position = new Vector3(this.transform.position.x + spawnOffset, this.transform.position.y, this.transform.position.z);

		if(spawnInterval >= 0.5f) { spawnInterval = spawnInterval * 0.99f; };
		isSpawningRock = false;

		yield return 0;
	}

	private void OnDrawGizmos() {
		// transparent red... niet transexueel...
		Color transRed = new Color();
		transRed = Color.red;
		transRed.a = 0.5f;

		Gizmos.color = transRed;
		Gizmos.DrawLine(this.transform.position, rockSpawnOrigin.transform.position);
	}
}
