using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyMovements : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent agent;
    private Animator animator;
    private bool playerInRange;
    private bool isAttacking;
    private float attackCooldown = 3f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerInRange = false;
        isAttacking = false;
    }

    private void Update()
{
    ControlPlayer player = FindObjectOfType<ControlPlayer>();
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        float detectionRange = 5f;
        float attackRange = 2f;

        Vector3 playerDirection = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, playerDirection);

        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            if (!isAttacking)
            {
                // Check if the player is behind the enemy
                if (angleToPlayer > 90)
                {
                    // Rotate the enemy towards the player
                    transform.rotation = Quaternion.LookRotation(playerDirection);
                    // Remove the y-rotation to make sure the enemy doesn't tilt up or down
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                }

                agent.SetDestination(playerTransform.position);
                agent.isStopped = false;
                animator.SetBool("Walk", true);
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("Walk", false);
            }

            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                animator.SetTrigger("Attack");
                isAttacking = true;

                StartCoroutine(ResetAttackCooldown());
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
        }
    }
    else
    {
        agent.isStopped = true;
        animator.SetBool("Walk", false);
    }
}

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}