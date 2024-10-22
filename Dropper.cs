using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    // Time in seconds to wait before enabling gravity (editable in the Inspector)
    [SerializeField] private float timeToWait = 3f;

    // Cache reference to the Rigidbody component
    private Rigidbody rBody;

    // Track the elapsed time since the start
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the object
        rBody = GetComponent<Rigidbody>();

        // Disable gravity initially to keep the object suspended
        rBody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Track how much time has passed since the object was enabled
        elapsedTime += Time.deltaTime;

        // Check if the specified time has passed
        if (elapsedTime >= timeToWait)
        {
            // Enable gravity to make the object fall
            rBody.useGravity = true;

            // Optional: Once gravity is enabled, stop checking further
            // You can comment out the following line if you want the check to keep running
            this.enabled = false;
        }
    }
}
