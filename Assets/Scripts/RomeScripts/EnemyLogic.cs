using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyLogic : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int enemyLevel = 1;

    public Animator animator;
    public Slider healthSlider;
    public float disappearDelay = 10f;

    private bool isDead = false;
    private BoxCollider boxCollider;

    public int damage = 0;
    public int damageMultiplier = 0;
    public int totalDamage = 0;
    private bool isAttacking;

    private NavMeshAgent agent;
    private EnemyMovements enemyMovements;

    private void Start()
    {
        damage = 2;
        damageMultiplier = 2 * enemyLevel;
        totalDamage = damage * damageMultiplier;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        agent = GetComponent<NavMeshAgent>();
        enemyMovements = GetComponent<EnemyMovements>();
    }

    private void Update()
    {
        if(!isAttacking)
        {
            GiveDamage(totalDamage);
        }
    }

    public void GiveDamage(int totalDamage)
    {
        ControlPlayer player = FindObjectOfType<ControlPlayer>();
        if (player != null && isDead == false && player.isDead == false)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0f;
            directionToPlayer.Normalize();
        
            Vector3 enemyForward = transform.forward;

            float dotProduct = Vector3.Dot(directionToPlayer, enemyForward);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            float maxDistance = 3f;
            if (dotProduct > 0.5f && distanceToPlayer + 0.5f < maxDistance)
            {
                animator.SetBool("Attack", true);
                isAttacking = true;
                
                float animationLength = 1f;
                Invoke("ApplyPlayerDamage", animationLength);
                Invoke("ResetGiveDamage", 2f);
            }
        }
    }

    private void ApplyPlayerDamage()
    {
        ControlPlayer player = FindObjectOfType<ControlPlayer>();
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            float maxDistance = 3f;
            if (distanceToPlayer + 0.9f < maxDistance)
            {
                player.TakeDamage(totalDamage);
            }
        }
    }

    private void ResetGiveDamage()
    {
        animator.SetBool("Attack", false);
        isAttacking = false;
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

        if (enemyMovements != null)
            enemyMovements.enabled = false;

        if (agent != null)
            agent.isStopped = true;

        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
        ControlPlayer.killCount = ControlPlayer.killCount+1;
    }
}