using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Remy").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, LayerMask.GetMask("Player"));

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                walkPointSet = false;
                Invoke(nameof(SetPatrolPoint), Random.Range(1f, 3f));
            }
        }
    }

    private void SearchWalkPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * walkPointRange;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, walkPointRange, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    private void SetPatrolPoint()
    {
        if (!playerInSightRange)
        {
            SearchWalkPoint();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
