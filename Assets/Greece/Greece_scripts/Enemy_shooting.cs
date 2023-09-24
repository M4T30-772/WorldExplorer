using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shooting : MonoBehaviour
{
    public GameObject enemyProjectile;
    public float spawnTimer;
    public float spawnMax = 7;
    public float spawnMin = 2;
    public AudioClip cannonballSound; // Reference to the cannonball sound.

    private AudioSource audioSource; // Reference to the AudioSource component.

    void Start()
    {
        spawnTimer = Random.Range(spawnMin, spawnMax);

        // Get the AudioSource component from the enemyProjectile.
        audioSource = enemyProjectile.GetComponent<AudioSource>();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            // Play the cannonball sound.
            if (audioSource != null && cannonballSound != null)
            {
                audioSource.PlayOneShot(cannonballSound);
            }

            // Instantiate the enemyProjectile.
            Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            spawnTimer = Random.Range(spawnMin, spawnMax);
        }
    }
}
