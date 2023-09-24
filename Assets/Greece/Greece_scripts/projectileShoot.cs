using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnOffset = 1.0f; // Adjust this to control the offset distance.
    public float shootCooldown = 2.0f; // Time in seconds between shots.

    private float timeSinceLastShot = 0.0f;

    void Update()
    {
        // Update the time since the last shot.
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timeSinceLastShot >= shootCooldown)
        {
            ShootProjectile();
            timeSinceLastShot = 0.0f; // Reset the timer after shooting.
        }
    }

void ShootProjectile()
{
    if (projectilePrefab != null)
    {
        // Calculate the spawn position in front of the object.
        Vector3 spawnPosition = transform.position + transform.forward * spawnOffset;

        // Instantiate the projectile at the adjusted spawn position.
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Get the AudioSource component from the projectilePrefab and play the sound.
        AudioSource projectileAudio = projectile.GetComponent<AudioSource>();
        if (projectileAudio != null)
        {
            projectileAudio.Play();
        }
    }
}
}
