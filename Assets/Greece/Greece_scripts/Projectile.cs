using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public GameObject explosionPrefab;
    private AudioSource soundManagerAudioSource; // Reference to the AudioSource on the SoundManager.

    private void Start()
    {
        // Find and store the AudioSource component on the SoundManager GameObject.
        GameObject soundManager = GameObject.Find("SoundManager"); // Replace with the actual name.
        if (soundManager != null)
        {
            soundManagerAudioSource = soundManager.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime * 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Instantiate the explosion prefab.
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);


            // Destroy the enemy ship and the projectile.
            Destroy(other.gameObject); // Destroy the enemy ship
            Destroy(gameObject); // Destroy the projectile
            PlayerController.kills = PlayerController.kills+1;
        }
        else if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
