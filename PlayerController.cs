


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCollisionScript : MonoBehaviour
//{
//    [SerializeField]
//    private float speed = 5f;  // Speed of the player, exposed to the Inspector
//    private float originalSpeed; // Store the original speed
//    private Color collisionColor = Color.red;  // Color to change to upon collision

//    // Dictionary to store original colors for each wall
//    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

//    private void Start()
//    {
//        // Save the original speed
//        originalSpeed = speed;
//    }

//    void Update()
//    {
//        // Get input from the horizontal (A/D, Left/Right arrows) and vertical (W/S, Up/Down arrows) axes
//        float moveHorizontal = Input.GetAxis("Horizontal");
//        float moveVertical = Input.GetAxis("Vertical");

//        // Create a movement vector
//        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

//        // Move the player
//        transform.Translate(movement * speed * Time.deltaTime);
//    }

//    public void BoostSpeed(float multiplier, float duration)
//    {
//        // Increase the player's speed
//        speed *= multiplier;
//        StartCoroutine(RevertSpeedAfterDelay(duration));
//    }

//    private IEnumerator RevertSpeedAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        speed = originalSpeed; // Revert to original speed
//    }

//    // Called when the player starts colliding with another object
//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Wall"))
//        {
//            // Increase score through ScoreManager when colliding with Wall or Ball
//            ScoreManager.Instance.AddScore(10);  // Add 10 points per collision

//            Renderer wallRenderer = collision.gameObject.GetComponent<Renderer>();

//            if (wallRenderer != null)
//            {
//                // Save the original color of the wall if not already saved
//                if (!originalColors.ContainsKey(wallRenderer))
//                {
//                    originalColors[wallRenderer] = wallRenderer.material.color;
//                }

//                // Change the wall's color to red upon collision
//                wallRenderer.material.color = collisionColor;
//            }
//        }

//        // Prevent score increase when touching the floor
//        if (collision.gameObject.CompareTag("Floor"))
//        {
//            return;
//        }
//    }

//    // Called every frame the player stays in contact with another object
//    void OnCollisionStay(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Wall"))
//        {
//            Renderer wallRenderer = collision.gameObject.GetComponent<Renderer>();

//            if (wallRenderer != null)
//            {
//                // Keep the wall red while the player is in contact with it
//                wallRenderer.material.color = collisionColor;
//            }
//        }
//    }

//    // Called when the player stops colliding with another object
//    void OnCollisionExit(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Wall"))
//        {
//            Renderer wallRenderer = collision.gameObject.GetComponent<Renderer>();

//            if (wallRenderer != null && originalColors.ContainsKey(wallRenderer))
//            {
//                // Revert the wall back to its original color after collision ends
//                wallRenderer.material.color = originalColors[wallRenderer];
//            }
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;  // Speed of the player, exposed to the Inspector
    private float originalSpeed; // Store the original speed
    private Color collisionColor = Color.red;  // Color to change to upon collision

    // Dictionary to store original colors for each wall
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    // Speed reduction variables
    [SerializeField]
    private float speedReduction = 2f; // Amount to reduce speed
    [SerializeField]
    private float speedReductionDuration = 3f; // Duration for which speed is reduced

    private void Start()
    {
        // Save the original speed
        originalSpeed = speed;
    }

    void Update()
    {
        // Get input from the horizontal (A/D, Left/Right arrows) and vertical (W/S, Up/Down arrows) axes
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime);
    }

    public void BoostSpeed(float multiplier, float duration)
    {
        // Increase the player's speed
        speed *= multiplier;
        StartCoroutine(RevertSpeedAfterDelay(duration));
    }

    private IEnumerator RevertSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = originalSpeed; // Revert to original speed
    }

    // Called when the player starts colliding with another object
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") ||
            collision.gameObject.CompareTag("Spinner") ||
            collision.gameObject.CompareTag("Dropper"))
        {
            // Increase score through ScoreManager when colliding with Wall or Ball
            ScoreManager.Instance.AddScore(10);  // Add 10 points per collision

            // Reduce the player's speed
            speed -= speedReduction;
            StartCoroutine(RevertSpeedAfterCollision());

            Renderer wallRenderer = collision.gameObject.GetComponent<Renderer>();

            if (wallRenderer != null)
            {
                // Save the original color of the wall if not already saved
                if (!originalColors.ContainsKey(wallRenderer))
                {
                    originalColors[wallRenderer] = wallRenderer.material.color;
                }

                // Change the wall's color to red upon collision
                wallRenderer.material.color = collisionColor;
            }
        }

        // Prevent score increase when touching the floor
        if (collision.gameObject.CompareTag("Floor"))
        {
            return;
        }
    }

    private IEnumerator RevertSpeedAfterCollision()
    {
        yield return new WaitForSeconds(speedReductionDuration);
        speed = originalSpeed; // Revert to original speed
    }

    // Called every frame the player stays in contact with another object
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Renderer wallRenderer = collision.gameObject.GetComponent<Renderer>();

            if (wallRenderer != null)
            {
                // Keep the wall red while the player is in contact
                wallRenderer.material.color = collisionColor;
            }
        }
    }

    // Called when the player stops colliding with another object
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Renderer wallRenderer = collision.gameObject.GetComponent<Renderer>();

            if (wallRenderer != null && originalColors.ContainsKey(wallRenderer))
            {
                // Revert the wall back to its original color after collision ends
                wallRenderer.material.color = originalColors[wallRenderer];
            }
        }
    }
}