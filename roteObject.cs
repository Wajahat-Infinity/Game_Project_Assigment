using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roteObject : MonoBehaviour
{


    public float rotationSpeed = 200f; // Adjust the speed as needed

    void Update()
    {
        // Rotate the obstacle around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

