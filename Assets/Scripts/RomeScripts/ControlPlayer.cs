using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlPlayer : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking;
    private bool isTakingDamage;
    public bool isDead;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    public static int health;
    public bool isShieldActive = false;
    private Coroutine shieldCoroutine;

    public Canvas healthCanvas;
    public Slider healthSlider;

    public float attackRadius = 0.1f;
    private float lastAttackTime;

    public static int killCount;
    public int romePlayerScore = 0;

    private void Start()
    {
        health = 100;
        animator = GetComponent<Animator>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
        killCount = 0;
    }

    private void Update()
    {
        healthSlider.value = health;
        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            Attack();
        }
        else
        {
            EndAttack();
        }
        if (Input.GetKeyDown(KeyCode.B) && !isAttacking)
        {
            StartShield();
        }

        if(PauseMenu.currentSceneName == "RomeLevel1") { if(killCount == 3) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactRim 1"); killCount = 0; PlayerPrefs.SetInt("romePlayerScore", 10); } }
        if(PauseMenu.currentSceneName == "RomeLevel2") { if(killCount == 5) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactRim 2"); killCount = 0; PlayerPrefs.SetInt("romePlayerScore", 20); } }
        if(PauseMenu.currentSceneName == "RomeLevel3") { if(killCount == 3) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactRim 3"); killCount = 0; PlayerPrefs.SetInt("romePlayerScore", 30); } }
        if(PauseMenu.currentSceneName == "RomeLevel4") { if(killCount == 3) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactRim 4"); killCount = 0; PlayerPrefs.SetInt("romePlayerScore", 40); } }
        if(PauseMenu.currentSceneName == "RomeLevel5") { if(killCount == 9) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactRim 5"); killCount = 0; PlayerPrefs.SetInt("romePlayerScore", 50); } }
    }

    private void Attack()
    {
        if (isAttacking == false && Time.time - lastAttackTime >= 0.5f)
        {
            animator.SetBool("Attack", true);
            isAttacking = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Vector3 directionToEnemy = collider.transform.position - transform.position;
                    directionToEnemy.y = 0f;
                    directionToEnemy.Normalize();

                    Vector3 playerForward = transform.forward;

                    float dotProduct = Vector3.Dot(directionToEnemy, playerForward);

                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                    if (dotProduct > 0.5f && distanceToEnemy + 0.1 < attackRadius)
                    {
                        EnemyLogic enemyLogic = collider.GetComponent<EnemyLogic>();
                        BossLogic bossLogic = collider.GetComponent<BossLogic>();
                        if (enemyLogic != null)
                        {
                            enemyLogic.TakeDamage(10);
                        }
                        if (bossLogic != null)
                        {
                            bossLogic.TakeDamage(10);
                        }
                    }
                }
            }

            lastAttackTime = Time.time;
        }
    }

    private void EndAttack()
    {
        animator.SetBool("Attack", false);
        Invoke("EndIsAttacking", 3f);
    }

    private void EndIsAttacking()
    {
        isAttacking = false;
        Invoke("EndIsAttacking", 3f);
    }

    public void TakeDamage(int damage)
    {
        if (!isTakingDamage && isShieldActive == false)
        {
            health -= damage;
            healthSlider.value = health;
            
            if (health <= 0)
            {
                healthSlider.value = 0;
                Die();
                return;
            }

            healthSlider.value = health;

            animator.SetBool("GetDamage", true);
            isTakingDamage = true;

            Invoke("EndTakingDamage", 0.5f);
        }
    }

    private void EndTakingDamage()
    {
        animator.SetBool("GetDamage", false);
        isTakingDamage = false;
    }

    private void Die()
    {
        if (!isDead)
        {
            animator.SetBool("Dead", true);
            isDead = true;
            // Postavi Y poziciju bossa na -0.96
            Vector3 newPosition = transform.position;
            newPosition.y = -0.96f;
            transform.position = newPosition;
            SceneManager.LoadScene("GameOver");
        }
    }

    private void StartShield()
    {
        //animator.SetBool("Shield", true);
        //isShieldActive = true;
        //shieldCoroutine = StartCoroutine(EndShieldAfterDelay(0.5f));
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= 0.6f)
        {
            animator.SetBool("Shield", true);
            isShieldActive = true;
            shieldCoroutine = StartCoroutine(EndShieldAfterDelay(0.5f));
        }
        else
        {
            Debug.Log("Shield failed to activate.");
        }
    }

    private void EndShield()
    {
        animator.SetBool("Shield", false);
        isShieldActive = false;

        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }
    }

    private IEnumerator EndShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndShield();
    }
}