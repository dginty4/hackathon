using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float fixedXPosition = 0f; // Fixed horizontal position for the camera

    void Update()
    {
        if (player != null)
        {
            // Update the camera's position to follow the player vertically
            transform.position = new Vector3(fixedXPosition, player.position.y, transform.position.z);
        }
    }
}
