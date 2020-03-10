using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {
	public Transform followTarget;
	public float smoothSpeed = 12.5f;
	public Vector3 offset;

	private void LateUpdate() {
		Vector3 disiredPosition = new Vector3(followTarget.position.x + offset.x, offset.y, followTarget.position.z + offset.z);
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, disiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}

}
