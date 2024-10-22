using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDestroyer : MonoBehaviour
{
    // The score threshold to destroy the player
    public int scoreThreshold = 100;

    // Reference to the score from another script (e.g., ScoreManager)
    private int currentScore;

    // Update is called once per frame
    void Update()
    {
        // Assuming you have a ScoreManager that keeps track of the player's score
        // Fetch the current score from the ScoreManager (replace 'ScoreManager' with your actual class name)
        currentScore = ScoreManager.Instance.GetScore();

        // Check if the score has reached or exceeded the threshold
        if (currentScore >= scoreThreshold)
        {
            // Destroy the player GameObject (the GameObject this script is attached to)
            Destroy(gameObject);

            // Optionally, destroy after a delay:
            // Destroy(gameObject, 2.0f);  // Destroys the player after 2 seconds
        }
    }
}
