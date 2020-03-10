using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

	[SerializeField] private bool scrolling = false;
	[SerializeField] private bool parallaxing = false;
	[Space]
	[SerializeField] private float parallaxSpeed;
	[SerializeField] private float backgroundSize;
	[Space]
	[SerializeField] private Transform cameraTransform;
	[SerializeField] public List<Transform> backgroundLayers = new List<Transform>();
	[Space]
	[SerializeField] private float viewzone = 10f;
	[SerializeField] private float lastCameraX;
	[SerializeField] int leftIndex;
	[SerializeField] int rightIndex;

	private void Start() {
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;

		GetChildren();

		leftIndex = 0;
		rightIndex = backgroundLayers.Count - 1;
	}

	private void LateUpdate() {
		if(parallaxing) {
			float deltaX = cameraTransform.position.x - lastCameraX;
			transform.position += Vector3.right * (deltaX * parallaxSpeed);
		}

		lastCameraX = cameraTransform.position.x;

		if(scrolling) {
			if(cameraTransform.position.x > (backgroundLayers[rightIndex].transform.position.x - viewzone)) {
				ScrollRight();
			}
		}
	}

	private void ScrollRight() {
		backgroundLayers[leftIndex].position = new Vector3(
		backgroundLayers[rightIndex].position.x + backgroundSize * Vector3.right.x,
		backgroundLayers[rightIndex].position.y,
		backgroundLayers[leftIndex].position.z);
		rightIndex = leftIndex;
		leftIndex++;
		if(leftIndex == backgroundLayers.Count) { leftIndex = 0; }
	}

	public void GetChildren() {
		for(int i = 0; i < transform.childCount; i++) {
			backgroundLayers.Add(transform.GetChild(i));
		}
	}
}
