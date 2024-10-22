using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Include the TextMeshPro namespace

public class Finisher : MonoBehaviour
{
    public TextMeshProUGUI finishMessageText;  // Reference to the UI text component to display the message

    private bool playerInFinishArea = false;  // Tracks if the player is in the finish area
    private int ballsInFinishArea = 0;        // Counter to track how many balls are in the finish area
    private const int requiredBalls = 2;      // Number of balls required to finish the game (2 balls)

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInFinishArea = true;  // Mark player as in the finish area
            Debug.Log("Player has reached the finish area!");

            // Check if both balls are in the finish area
            CheckForGameCompletion();
        }

        // Check if the object entering the trigger is one of the balls
        if (other.CompareTag("Ball"))
        {
            ballsInFinishArea++;  // Increment the counter for balls in the finish area
            Debug.Log("A ball has reached the finish area!");

            // Check if both balls and the player are in the finish area
            CheckForGameCompletion();
        }
    }

    // This method is called when a collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInFinishArea = false;  // Mark player as out of the finish area
            Debug.Log("Player has left the finish area!");
        }

        if (other.CompareTag("Ball"))
        {
            ballsInFinishArea--;  // Decrement the counter if a ball leaves the finish area
            Debug.Log("A ball has left the finish area!");
        }
    }

    // Check if both balls and the player are in the finish area
    private void CheckForGameCompletion()
    {
        if (playerInFinishArea && ballsInFinishArea == requiredBalls)
        {
            Debug.Log("Both balls and the player have reached the finish area!");

            // Display the finish message
            DisplayFinishMessage();
        }
    }

    private void DisplayFinishMessage()
    {
        // Update the finish message text
        if (finishMessageText != null)
        {
            finishMessageText.text = "You finished! balls & player are in the finish area.";
        }
    }
}
