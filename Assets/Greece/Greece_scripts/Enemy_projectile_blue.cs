using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_projectile_blue : MonoBehaviour
{
    public float speed = 5;
    public GameObject explosionPrefab;
    public AudioClip cannonballSound; // Reference to the cannonball sound.

    private AudioSource audioSource; // Reference to the AudioSource component.

    private void Start()
    {
        // Get the AudioSource component from the explosionPrefab.
        audioSource = explosionPrefab.GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the cannonball sound.
            if (audioSource != null && cannonballSound != null)
            {
                audioSource.PlayOneShot(cannonballSound);
            }

            // Instantiate the explosion prefab.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destroy the player and the projectile.
            Destroy(other.gameObject); // Destroy player
            Destroy(gameObject); // Destroy the projectile

            // Load the "GameOver" scene.
            SceneManager.LoadScene("GameOver");
        }
    }
}
