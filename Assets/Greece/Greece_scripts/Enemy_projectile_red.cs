using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_projectile_red : MonoBehaviour
{
    public float speed = 10;
    public GameObject explosionPrefab;
    public AudioClip cannonballSound; // Reference to the cannonball sound.

    private bool hasPlayedSound = false; // Flag to track if the sound has been played.

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    if (!hasPlayedSound && cannonballSound != null)
            {
                AudioSource.PlayClipAtPoint(cannonballSound, transform.position);
            }
        if (other.CompareTag("Player"))
        {

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
