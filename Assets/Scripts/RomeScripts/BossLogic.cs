using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLogic : MonoBehaviour
{
    public float attackRange = 10f; // Domet napada bossa
    public float disappearDelay = 10f;

    public Slider healthSlider;
    private Animator animator;
    private BoxCollider boxCollider;

    private Transform player; // Referenca na igrača
    private bool isDead = false;
    private bool canAttack = true; // Provjera može li boss napasti

    public int currentHealth;

    private void Start()
    {
        currentHealth = 1000;
        healthSlider.maxValue = 1000;
        healthSlider.value = currentHealth;
        animator = GetComponent<Animator>(); // Assign the Animator component
        player = GameObject.FindGameObjectWithTag("Player").transform; // Pronalaženje igrača po tagu "Player"
    }

    private void Update()
    {
        if (isDead)
        {
            // If the boss is dead, prevent any further actions
            return;
        }

        // Face the player
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Ignore vertical distance
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 100f);
        }


        // Provjera je li igrač unutar dometa napada
        if (Vector3.Distance(transform.position, player.position) <= attackRange && canAttack != false && !isDead)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Simulacija napada na igrača
        animator.SetBool("Attack", true);
        float animationLength = 1.5f;
        Invoke("ApplyPlayerDamage", animationLength);
        // Postavljanje cooldowna nakon napada
        canAttack = false;
        Invoke("ResetAttackAnim", animationLength);
        Invoke("PermToAttackAgain", 3f);
    }

    private void ApplyPlayerDamage()
    {
    ControlPlayer player = FindObjectOfType<ControlPlayer>();
    if (player != null)
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float maxDistance = 3f;

        if (distanceToPlayer < maxDistance)
        {
            Vector3 kickDirection = -transform.forward;

            Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                float kickForce = 10f;
                playerRigidbody.AddForce(-kickDirection * kickForce, ForceMode.Impulse);
            }

            player.TakeDamage(15);
        }
    }
}

    private void ResetAttackAnim()
    {
        // Resetiranje mogućnosti napada nakon cooldowna
        animator.SetBool("Attack", false);
        
    }

    private void PermToAttackAgain()
    {
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        healthSlider.value = currentHealth;


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        animator.SetBool("Die", true);

        Destroy(healthSlider.gameObject);

        Destroy(boxCollider);

        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);

        Destroy(gameObject);
        ControlPlayer.killCount = ControlPlayer.killCount+1;
    }
}