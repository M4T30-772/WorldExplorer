using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_movement : MonoBehaviour
{
    public float moveSpeed = 4.0f; // Adjust the speed as needed
    private float timeSinceLastMove = 0.0f;
    private bool isMovingRight = true;

    // Update is called once per frame
    void Update()
    {
        // Increase the timeSinceLastMove by the time passed since the last frame
        timeSinceLastMove += Time.deltaTime;

        // Check if it's time to change direction (2 seconds)
        if (timeSinceLastMove >= 2.0f)
        {
            // Generate a random direction (left or right)
            int randomDirection = Random.Range(0, 2); // 0 or 1
            isMovingRight = (randomDirection == 0);

            // Reset the timer
            timeSinceLastMove = 0.0f;
        }

        // Calculate the movement direction
        Vector3 movementDirection = isMovingRight ? Vector3.right : Vector3.left;

        // Move the ship in the selected direction
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }
}
