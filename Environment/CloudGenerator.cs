using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CloudGenerator : MonoBehaviour {

	[SerializeField] private bool shouldSpawn = true;
	[SerializeField] [Range(0, 10)] private float spawnInterval;
	[SerializeField] [Range(0, 5)] private float cloudHeightOffset;
	[Space]
	[SerializeField] private Transform smallCloudsOrigin;
	[SerializeField] private Transform medCloudsOrigin;
	[SerializeField] private Transform bigCloudsOrigin;
	[Space]
	[SerializeField] private Transform smallCloudParent;
	[SerializeField] private Transform medCloudParent;
	[SerializeField] private Transform bigCloudParent;
	[Space]
	[SerializeField] private ParallaxBackground parallaxSmall;
	[SerializeField] private ParallaxBackground parallaxMed;
	[SerializeField] private ParallaxBackground parallaxBig;
	[Space]
	[SerializeField] private List<GameObject> smallClouds = new List<GameObject>();
	[SerializeField] private List<GameObject> mediumClouds = new List<GameObject>();
	[SerializeField] private List<GameObject> bigClouds = new List<GameObject>();

	// Update is called once per frame
	void Update() {
		if(shouldSpawn) {
			shouldSpawn = false;
			StartCoroutine(GenerateClouds());
		}
	}

	private IEnumerator GenerateClouds() {
		CreateNewCloud(smallClouds.ToArray(), smallCloudsOrigin, smallCloudParent, parallaxSmall);
		CreateNewCloud(mediumClouds.ToArray(), medCloudsOrigin, medCloudParent, parallaxMed);
		CreateNewCloud(bigClouds.ToArray(), bigCloudsOrigin, bigCloudParent, parallaxBig);

		yield return new WaitForSeconds(spawnInterval);
		shouldSpawn = true;
	}

	private void CreateNewCloud(GameObject[] clouds, Transform spawnOrigin, Transform parent, ParallaxBackground parallaxBackground) {
		float heightOffset = Random.Range(0 - cloudHeightOffset, cloudHeightOffset);
		int index = Random.Range(0, clouds.Length);
		GameObject cloudToSpawn = Instantiate(clouds[index], desiredSpawnPosition(spawnOrigin, heightOffset), Quaternion.identity);
		cloudToSpawn.transform.parent = parent;
		parallaxBackground.backgroundLayers.Add(cloudToSpawn.transform);
	}

	public Vector3 desiredSpawnPosition(Transform transform, float offset) {
		Vector3 desiredPos = new Vector3(
			transform.position.x,
			transform.position.y + offset,
			transform.position.x);

		return desiredPos;
	}
}
