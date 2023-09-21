using UnityEngine;
using System;

public class EnemyBoss : Character
{
    [Header("Enemy Stats")]
    [SerializeField] protected XPSystem levelSystem = default;
    [SerializeField] protected int enemyXp = default;
    [SerializeField] protected FloatValue maxHealth = default;
    [SerializeField] private float _health;
    [SerializeField] private GameObject openEntrance;
    [SerializeField] protected HealthbarBehaviour healthbar;
    public event Action OnEnemyTakeDamage;
    public event Action OnBossDied;
    [Header("BossFight-Values")]
    public bool isMinion = false;

    [Header("Boss defeated Music")]
    [SerializeField] private AudioClip[] bossDefeatedMusic;

    public override float health {
        get => _health;
        set {
            if (value > maxHealth.value)
            {
                value = maxHealth.value;
                healthbar.UpdateHealthBar(value, maxHealth.value);
            }
            else if (value < 0)
            {
                value = 0;
            }

            if (value < _health)
            {
                OnEnemyTakeDamage?.Invoke();                                //Signal for when enemys take dmg (hopefully :) )                                                                            // Need to prevent the enemy from moving and set idle/moving Anim
                if (healthbar)
                {
                    healthbar.gameObject.SetActive(true);
                    healthbar.UpdateHealthBar(value, maxHealth.value);
                }
            }

            _health = value;

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    [Header("Enemy Attributes")]
    [SerializeField] protected string enemyName = default;      // Unused, is it necessary?
    public float moveSpeed = default;                           // Should make protected

    [Header("Death Effects")]
    [SerializeField] protected GameObject deathEffect = default;
    [SerializeField] private float deathEffectDelay = 1;

    [Header("Death Signal")]
    [SerializeField] protected Signals roomSignal = default;
    [SerializeField] protected LootTable thisLoot = default;

    [Header("Saving")]
    [SerializeField] private bool isDefeated;
    [SerializeField] private BoolValue storeDefeated;
    [SerializeField] private GameObject bossGameObject;

    protected override void Awake()
    {
        base.Awake();
        health = maxHealth.value;
        isDefeated = storeDefeated.RuntimeValue;
        if (isDefeated)
        {
            bossGameObject.SetActive(false);
        }
        healthbar = GetComponentInChildren<HealthbarBehaviour>();
        healthbar.gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        health = maxHealth.value;
        currentStateEnum = StateEnum.idle;
    }

    protected virtual void Die()
    {
        DeathEffect();
        thisLoot?.GenerateLoot(transform.position);
        levelSystem.AddExperience(enemyXp);
        storeDefeated.RuntimeValue = true;
        if (deathSounds[0])
        {
            SoundManager.RequestSound(deathSounds.GetRandomElement());
        }
        if (deathSounds[0])
        {
            MusicManager.RequestMusic(bossDefeatedMusic);
        }

        if (openEntrance)
        {
            openEntrance.SetActive(false);
        }

        if (roomSignal != null)
        {
            roomSignal.Raise();
        }

        this.gameObject.SetActive(false);
        OnBossDied?.Invoke();
    }

    protected void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public float GetPercentHealth() => (health * 100) / maxHealth.value;

    public void KillEnemy() => health = 0;

    public void GetHealed(float healAmount)
    {
        if (this.health < this.maxHealth.value)
        {
            if (this.health + healAmount > this.maxHealth.value)
            {
                this.health = this.maxHealth.value;
            }
            this.health += healAmount;
            DamagePopUpManager.RequestDamagePopUp(healAmount, this.transform);
        }
    }

    public float GetMaxHealth()
    {
        return this.maxHealth.value;
    }
}
