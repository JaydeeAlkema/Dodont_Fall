using UnityEngine;

public class DestroyObjectOnHit : MonoBehaviour {
	[SerializeField] private string[] tagsToCheck;

	public void OnTriggerEnter2D(Collider2D other) {
		for(int i = 0; i < tagsToCheck.Length; i++) {
			if(other.CompareTag(tagsToCheck[i])) {
				Destroy(other.gameObject);
			}
		}
	}
}
