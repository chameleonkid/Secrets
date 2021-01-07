using UnityEngine;
using System;
using System.Collections;

public class Enemy : Character
{
    [Header("Enemy Stats")]
    [SerializeField] private XPSystem levelSystem = default;
    [SerializeField] private int enemyXp = default;
    [SerializeField] protected FloatValue maxHealth = default;
    [SerializeField] private float _health;
    [SerializeField] private bool isWalking;
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
           //     animator.Play("Death");
                Die();
            }
        }
    }

    [SerializeField] protected string enemyName = default;      // Unused, is it necessary?
    public float moveSpeed = default;                           // Should make protected
    [SerializeField] protected Vector2 homePosition = default;
    [SerializeField] protected Vector3 roamingPosition = default;
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

        roamingPosition = GetRoamingPostion();

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

    protected virtual void OutsideChaseRadiusUpdate()
    {
        randomMovement();
    }

    private void Die()
    {
        if (isMinion)
        {
            Debug.Log("Minion was killed");
            OnMinionDied?.Invoke();
        }
        else
        {
            Debug.Log("Normal Enemy was killed");
            OnEnemyDied?.Invoke();  
        }

        DeathEffect();
        thisLoot?.GenerateLoot(transform.position);
        levelSystem.AddExperience(enemyXp);

        if (roomSignal != null)
        {
            roomSignal.Raise();
        }

        this.gameObject.SetActive(false);
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public float GetPercentHealth() => (health * 100) / maxHealth.value;

    public void KillEnemy() => health = 0;



    protected Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    protected Vector3 GetRoamingPostion()
    {
        Vector3 vec3Home;
        vec3Home = homePosition;
        return vec3Home + (GetRandomDirection() * UnityEngine.Random.Range(3f, 3f));
    }



    protected void randomMovement()
    {
       
        if (this.transform.position != roamingPosition)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, roamingPosition, moveSpeed * Time.deltaTime);
            SetAnimatorXYSingleAxis(temp - transform.position);
            rigidbody.MovePosition(temp);
            currentState = State.walk;
            animator.SetBool("isMoving", true);
        }
        else
        {
            if(isWalking == false)
            {
                StartCoroutine(randomWaiting());
            }

        }

    }

    protected virtual IEnumerator randomWaiting()
    {
        isWalking = true;
        currentState = State.idle;
        animator.SetBool("isMoving", false);
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 4f));
        roamingPosition = GetRoamingPostion();
        isWalking = false;
    }




}
