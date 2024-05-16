using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public enum State
    {
        None,
        Chasing,
        Attacking
    }

    State _currentState = State.None;

    public State CurrentState
    {
        get => _currentState;
        private set
        {
            if (_currentState != value)
            {
                _currentState = value;
                UpdateState();
            }
        }
    }
    private PlayerStatManager playerStatManager;
    private EnemyStatManager enemyStatManager;
    public PlayerLeveling playerLeveling;
    Animator _animator;
    Coroutine currentRoutine;

    private float moveSpeed;
    private float attackSpeed;
    private float maxHealth;
    private float damage;
    private float currentHealth;
    private bool isBleeding;
    private float bleedingTimer;

    private GameObject player;
    private NavMeshAgent agent;
    [SerializeField] float attackCloseEnoughDistance = 2f;
    [SerializeField] EnemyHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        enemyStatManager = FindObjectOfType<EnemyStatManager>();
        playerStatManager = FindObjectOfType<PlayerStatManager>();
        playerLeveling = FindObjectOfType<PlayerLeveling>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();

        if (enemyStatManager != null)
        {
            moveSpeed = enemyStatManager.initialMoveSpeed;
            attackSpeed = enemyStatManager.initialAttackSpeed;
            maxHealth = enemyStatManager.initialMaxHealth;
            damage = enemyStatManager.initialDamage;

            agent.speed = moveSpeed;
        }

        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        SetRunning(true);
        SetAttacking(false);

        CurrentState = State.Chasing;
    }

    void UpdateState()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);

        SetRunning(CurrentState == State.Chasing);
        SetAttacking(CurrentState == State.Attacking);

        switch (CurrentState)
        {
            case State.Chasing:
                currentRoutine = StartCoroutine(ChaseRoutine());
                break;
            case State.Attacking:
                currentRoutine = StartCoroutine(AttackRoutine());
                break;
        }

        agent.isStopped = CurrentState == State.Attacking;
    }

    void SetRunning(bool flag)
    {
        _animator.SetBool("IsRunning", flag);
    }

    void SetAttacking(bool flag)
    {
        _animator.SetBool("IsAttacking", flag);
    }

    IEnumerator ChaseRoutine()
    {
        while (CurrentState == State.Chasing)
        {
            Vector3 playerPos = player.transform.position;

            if (IsCloseEnough(playerPos, attackCloseEnoughDistance))
            {
                CurrentState = State.Attacking;
            }
            else
            {
                agent.SetDestination(playerPos);
            }


            yield return null;
        }
    }

    IEnumerator AttackRoutine()
    {
        agent.velocity = Vector3.zero;

        while (CurrentState == State.Attacking)
        {
            if (!IsCloseEnough(player.transform.position, attackCloseEnoughDistance))
            {
                CurrentState = State.Chasing;
            }
             else
            {
                float attackTime = 1f / attackSpeed;
                
                float animationSpeed = 1f / attackTime;
                _animator.SetFloat("AttackSpeedMultiplier", animationSpeed);

                float damageDelay = 0.5f * attackTime;
                yield return new WaitForSeconds(damageDelay);

                playerStatManager.TakeDamage(damage);
                yield return new WaitForSeconds(damageDelay);

                _animator.SetFloat("AttackSpeedMultiplier", 1f);

                yield return null;
            }
        }
    }

    bool IsCloseEnough(Vector3 destination, float closeEnoughDistance)
    {
        return Vector3.Distance(transform.position, destination) < closeEnoughDistance;
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
    public void StartBleeding(float damagePerSecond, float duration)
    {
        if (!isBleeding)
        {
            isBleeding = true;
            bleedingTimer = 0f;
            StartCoroutine(BleedCoroutine(damagePerSecond, duration));
        }
    }

    IEnumerator BleedCoroutine(float damagePerSecond, float duration)
    {
        while (bleedingTimer < duration)
        {
            yield return new WaitForSeconds(0.5f);

            TakeDamage(damagePerSecond);
            bleedingTimer += duration;
        }
        isBleeding = false;
    }

    void Die()
    {
        EnemySpawnManager spawnManager = FindObjectOfType<EnemySpawnManager>();
        if (spawnManager != null)
        {
            spawnManager.RemoveEnemy(gameObject);
        }
        Destroy(gameObject);
        playerLeveling.EnemyKilled();
    }
}