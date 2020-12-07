using System.Collections;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;
    public float chaseRadius;
    public float attackRadius;
    public float originalChaseRadius;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

    [Header("Death Signal")]
    public Signals roomSignal;
    public LootTable thisLoot;

    private void Awake()
    {
        homePosition = transform.position;
        health = maxHealth.initialValue;
        originalChaseRadius = chaseRadius;
    }

    private void OnEnable()
    {
        health = maxHealth.initialValue;
        transform.position = homePosition;
        currentState = EnemyState.idle;
    }

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
            PhysicalInventoryItem current = thisLoot.LootPowerUp();
            if(current != null)
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
