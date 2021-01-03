using UnityEngine;
using System;

public class Enemy : Character
{
    
    [Header("Enemy Stats")]
    [SerializeField] private XPSystem levelSystem = default;
    [SerializeField] private int enemyXp = default;
    [SerializeField] protected FloatValue maxHealth = default;
    [SerializeField] private float _health;
    public event Action OnEnemyTakeDamage;
    public event Action OnEnemyDied;
    public event Action OnMinionDied;
    public bool isMinion = false;

    public override float health {
        get => _health;
        set {
            if (value > maxHealth.value)
            {
                value = maxHealth.value;
            }
            else if (value < 0)
            {
                value = 0;
            }

            if (value < _health)
            {
                chaseRadius = originalChaseRadius * 10;
                OnEnemyTakeDamage?.Invoke();                                //Signal for when enemys take dmg (hopefully :) )
            }

            _health = value;

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    [SerializeField] protected string enemyName = default;      // Unused, is it necessary?
    public float moveSpeed = default;                           // Should make protected
    [SerializeField] protected Vector2 homePosition = default;
    [SerializeField] protected float chaseRadius = default;
    [SerializeField] protected float attackRadius = default;
    private float originalChaseRadius = default;

    [Header("Death Effects")]
    [SerializeField] private GameObject deathEffect = default;
    [SerializeField] private float deathEffectDelay = 1;

    [Header("Death Signal")]
    [SerializeField] private Signals roomSignal = default;
    [SerializeField] private LootTable thisLoot = default;

    protected Transform target = default;

    protected virtual void OnEnable()
    {
        health = maxHealth.value;
        transform.position = homePosition;
        chaseRadius = originalChaseRadius;
        currentState = State.idle;
        
    }

    protected override void Awake()
    {
        base.Awake();

        homePosition = transform.position;
        health = maxHealth.value;
        originalChaseRadius = chaseRadius;

        target = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void FixedUpdate()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            InsideChaseRadiusUpdate();
        }
        else if (distance > chaseRadius)
        {
            OutsideChaseRadiusUpdate();
        }
    }

    protected virtual void InsideChaseRadiusUpdate()
    {
        if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            SetAnimatorXYSingleAxis(temp - transform.position);
            rigidbody.MovePosition(temp);
            currentState = State.walk;
            animator.SetBool("isMoving", true);
        }
    }

    protected virtual void OutsideChaseRadiusUpdate() {}

    private void Die()
    {
        if (!isMinion)
        {
            Debug.Log("Normal Enemy was killed");
            DeathEffect();
            thisLoot?.GenerateLoot(transform.position);
            levelSystem.AddExperience(enemyXp);
            OnEnemyDied?.Invoke();

            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            {
                Debug.Log("Minion was killed");
                DeathEffect();
                thisLoot?.GenerateLoot(transform.position);
                levelSystem.AddExperience(enemyXp);
                OnMinionDied?.Invoke();

                if (roomSignal != null)
                {
                    roomSignal.Raise();
                }
                this.gameObject.SetActive(false);
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public float GetPercentHealth()
    {
        float enemyPercentHealth = (health * 100) / maxHealth.value;
        return enemyPercentHealth;
    }

    public void KillEnemy()
    {
        health = 0;
    }
}
