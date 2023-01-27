using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
		if (other.TryGetComponent<CharacterController>(out CharacterController player)){
			player.Die();
		}
	}
}
