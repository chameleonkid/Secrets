using System.Collections;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : Character
{
    [Header("State Machine")]
    public EnemyState currentState = default;

    [Header("Enemy Stats")]
    [SerializeField] protected FloatValue maxHealth = default;
    public float health = default;
    [SerializeField] protected string enemyName = default;
    [SerializeField] protected int baseAttack = default;
    public float moveSpeed = default;
    [SerializeField] protected Vector2 homePosition = default;
    public float chaseRadius = default;
    [SerializeField] protected float attackRadius = default;
    public float originalChaseRadius = default;

    [Header("Death Effects")]
    [SerializeField] private GameObject deathEffect = default;
    [SerializeField] private float deathEffectDelay = 1;

    [Header("Death Signal")]
    [SerializeField] private Signals roomSignal = default;
    [SerializeField] private LootTable thisLoot = default;

    protected Transform target = default;

    protected virtual void OnEnable()
    {
        health = maxHealth.initialValue;
        transform.position = homePosition;
        currentState = EnemyState.idle;
    }

    protected override void Awake()
    {
        base.Awake();

        homePosition = transform.position;
        health = maxHealth.initialValue;
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
        if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            SetAnimatorXYSingleAxis(temp - transform.position);
            rigidbody.MovePosition(temp);
            currentState = EnemyState.walk;
            animator.SetBool("WakeUp", true);
        }
    }

    protected virtual void OutsideChaseRadiusUpdate() {}

    private void TakeDamage(float damage)
    {
        health -= damage;
        chaseRadius = originalChaseRadius * 10;
        if (health <= 0)
        {
            Debug.Log("0 Leben");
            DeathEffect();
            MakeLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false);
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

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            ItemPickUp current = thisLoot.LootPowerUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
