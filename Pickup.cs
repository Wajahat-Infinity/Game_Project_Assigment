using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Duration for which the speed boost lasts
    public float boostDuration = 5f;
    // Speed multiplier to apply when picking up
    public float speedMultiplier = 2f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the pickup
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement script to boost speed
            PlayerCollisionScript playerMovement = other.GetComponent<PlayerCollisionScript>();
            if (playerMovement != null)
            {
                playerMovement.BoostSpeed(speedMultiplier, boostDuration);
            }

            // Destroy the pickup object
            Destroy(gameObject);
        }
    }
}