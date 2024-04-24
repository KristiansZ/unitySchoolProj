using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float initialMoveSpeed = 3f;
    public float initialAttackSpeed = 1f;
    public float maxHealth = 20f;

    private float moveSpeed;
    private float attackSpeed;
    public float currentHealth;

    private GameObject player;
    private NavMeshAgent agent;
    [SerializeField] EnemyHealthBar healthBar;
    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        moveSpeed = initialMoveSpeed;
        attackSpeed = initialAttackSpeed;

        // Start the coroutine to update the destination
        StartCoroutine(WalkTowardsPlayer());
    }

    IEnumerator WalkTowardsPlayer()
    {
        while (true)
        {
            Vector3 playerPos = player.transform.position;
            // Set the destination to the player's position
            if (player != null)
            {
                agent.SetDestination(playerPos);
            }

            // Wait for a short interval before updating again
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemySpawnManager spawnManager = FindObjectOfType<EnemySpawnManager>();
        if (spawnManager != null)
        {
            spawnManager.RemoveEnemy(gameObject);
        }
        Destroy(gameObject);
    }

    public void IncreaseStats(float maxHealthIncrease, float moveSpeedIncrease, float attackSpeedIncrease)
    {
        maxHealth += maxHealthIncrease;
        moveSpeed += moveSpeedIncrease;
        attackSpeed += attackSpeedIncrease;
    }
}