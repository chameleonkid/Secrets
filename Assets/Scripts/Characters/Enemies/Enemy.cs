using UnityEngine;
using System;
using System.Collections;
using Pathfinding;
using Schwer.States;

public class Enemy : Character, ICanKnockback
{
    [Header("Enemy Stats")]
  //  [SerializeField] private HealthbarBehaviour healthbar;
    [SerializeField] protected XPSystem levelSystem = default;
    [SerializeField] protected int enemyXp = default;
    [SerializeField] protected FloatValue maxHealth = default;
    [SerializeField] private float _health;
    [SerializeField] protected bool isWalking;
    [SerializeField] protected float walkingTimer = 3f;
    [SerializeField] protected HealthbarBehaviour healthbar;

    [Header("Avoiding Config")]
    [SerializeField] protected float directionChangeCooldown = 0.5f; // Time in seconds between direction changes
    [SerializeField] protected float lastDirectionChangeTime;
    [SerializeField] protected Vector3 currentAvoidDirection;

    public event Action OnEnemyTakeDamage;
    public event Action OnEnemyDied;
    public event Action OnMinionDied;
    [Header("BossFight-Values")]
    public bool isMinion = false;

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
                chaseRadius = originalChaseRadius * 5;
                OnEnemyTakeDamage?.Invoke();                                //Signal for when enemys take dmg (hopefully :) )
                //if a healthbar is attached - update it!
                if(healthbar)
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
    [SerializeField] protected Vector2 homePosition = default;
    [SerializeField] protected Vector3 roamingPosition = default;
    [SerializeField] protected float chaseRadius = default;
    [SerializeField] protected float attackRadius = default;
    private float originalChaseRadius = default;

    [Header("Death Effects")]
    [SerializeField] protected GameObject deathEffect = default;
    [SerializeField] private float deathEffectDelay = 1;

    [Header("Death Signal")]
    [SerializeField] protected Signals roomSignal = default;
    [SerializeField] protected LootTable thisLoot = default;

    [Header("Pathfinding")]
    [SerializeField] protected Transform target = default;
    public float nextWaypointDistance = 0.5f;
    [SerializeField] protected Path path;
    [SerializeField] protected int currentWaipoint = 0;
    [SerializeField] protected bool reachedEndOfPath;
    [SerializeField] protected Seeker seeker;

    [Header("SoundsOnDemand")]
    [SerializeField] protected bool leftChaseRadius = true;

    protected virtual void OnEnable()
    {
        health = maxHealth.value;
        transform.position = homePosition;
        chaseRadius = originalChaseRadius;
        currentStateEnum = StateEnum.idle;
    }

    protected override void Awake()
    {
        base.Awake();
        homePosition = transform.position;
        health = maxHealth.value;
        originalChaseRadius = chaseRadius;
        target = GameObject.FindWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        roamingPosition = GetRoamingPostion();
        healthbar = GetComponentInChildren<HealthbarBehaviour>();
        healthbar.gameObject.SetActive(false);
    }


    void UpdatePath()
    {
        if (seeker.IsDone() && IsInRange())
        {
            seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaipoint = 1;  //If this is 0 the strange stuttering starts...
        }
    }

    protected bool IsInRange()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= chaseRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<PlayerMovement>())
        {
            randomMovement();
        }

    }

    protected virtual void FixedUpdate()
    {

            //! Temporary!
            if (currentState is Schwer.States.Knockback)
            {
                currentState.FixedUpdate();
                return;
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
            }

        if (cantMove.RuntimeValue == false && isFrozen == false)
        {

            var percentHealh = maxHealth.value / 100f;
            var distance = Vector3.Distance(target.position, transform.position);
            if (distance <= chaseRadius && distance > attackRadius && this.health > (percentHealh * 10))
            {
                InsideChaseRadiusUpdate();
            }
            else if (distance <= chaseRadius && distance > attackRadius && this.health <= (percentHealh * 10))
            {
                Flee();
            }
            else if (distance > chaseRadius)
            {
                OutsideChaseRadiusUpdate();
            }
        }

    }

    protected virtual void InsideChaseRadiusUpdate()
    {
        if (leftChaseRadius)
        {
            SoundManager.RequestSound(inRangeSounds.GetRandomElement());
        }
        leftChaseRadius = false;
        if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaipoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            animator.SetBool("isMoving", true);
            Vector3 temp = Vector3.MoveTowards(rigidbody.position, path.vectorPath[currentWaipoint], moveSpeed * speedModifier * Time.deltaTime);
            rigidbody.MovePosition(temp);
            float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaipoint]);
            SetAnimatorXYSingleAxis(temp - transform.position);
            if (distance < nextWaypointDistance)
            {
                currentWaipoint++;
            }
            else
            {
                reachedEndOfPath = false;
            }
        }
    }

    protected virtual void OutsideChaseRadiusUpdate()
    {
        randomMovement();
        leftChaseRadius = true;
    }

    protected virtual void CheckForMinion()
    {
        if (isMinion)
        {
            // Debug.Log("Minion was killed");
            OnMinionDied?.Invoke();
        }
        else
        {
            // Debug.Log("Normal Enemy was killed");
            OnEnemyDied?.Invoke();
        }
    }

    protected virtual void Die()
    {
        DeathEffect();
        if(deathSounds.Length >= 0)
        {
            SoundManager.RequestSound(deathSounds.GetRandomElement());
        }
    
        thisLoot?.GenerateLoot(transform.position);
        levelSystem.AddExperience(enemyXp);

        if (roomSignal != null)
        {
            roomSignal.Raise();
        }

        this.gameObject.SetActive(false);
        CheckForMinion();
    }

    protected virtual void DeathEffect()
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
        vec3Home += (GetRandomDirection() * UnityEngine.Random.Range(3f, 3f));
        while (Physics2D.OverlapPoint(vec3Home))
        {
            vec3Home += (GetRandomDirection() * UnityEngine.Random.Range(3f, 3f));
           // Debug.Log("" + this + " Roamingpoint was in an collider");
        }
        return vec3Home;
    }

    protected void randomMovement()
    {
        if (this.transform.position != roamingPosition)
        {
            if (walkingTimer > 0)
            {
                walkingTimer -= Time.deltaTime;
                Vector3 temp = Vector3.MoveTowards(transform.position, roamingPosition, moveSpeed * speedModifier * Time.deltaTime);
                SetAnimatorXYSingleAxis(temp - transform.position);
                rigidbody.MovePosition(temp);
                currentStateEnum = StateEnum.walk;
                animator.SetBool("isMoving", true);
            }
            else
            {
                StartCoroutine(randomWaiting());
                walkingTimer = 3f;
            }
        }
        else
        {
            if (isWalking == false)
            {
                StartCoroutine(randomWaiting());
            }
        }
    }

    protected virtual IEnumerator randomWaiting()
    {
        isWalking = true;
        currentStateEnum = StateEnum.idle;
        animator.SetBool("isMoving", false);
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 4f));
        roamingPosition = GetRoamingPostion();
        isWalking = false;
    }

    protected void Flee()
    {
        //   var randomFleeDirection = transform.position += GetRandomDirection();
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, -1f * moveSpeed * speedModifier * Time.deltaTime);
        SetAnimatorXYSingleAxis(temp - transform.position);
        rigidbody.MovePosition(temp);
        animator.SetBool("isMoving", true);
    }

    protected virtual void AvoidPlayer()
    {
        if (Time.time > lastDirectionChangeTime + directionChangeCooldown)
        {
            // Update the avoid direction after the cooldown
            currentAvoidDirection = (transform.position - target.position).normalized + GetRandomDirection() * 0.5f;
            lastDirectionChangeTime = Time.time;
        }

        // Move the enemy in the current avoid direction
        Vector3 temp = Vector3.MoveTowards(transform.position, transform.position + currentAvoidDirection, moveSpeed * Time.deltaTime);
        SetAnimatorXYSingleAxis(temp - transform.position);
        rigidbody.MovePosition(temp);
        animator.SetBool("isMoving", true);
    }

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


    public void SetFreeze(float freezeDuration)
    {
        if(isFreezeable)
        {
            StartCoroutine(FreezeCo(freezeDuration));
        }
    }

    protected virtual IEnumerator FreezeCo(float freezeDuration)
    {
        isFrozen = true;
        animator.enabled = false;
        yield return new WaitForSeconds(freezeDuration);
        animator.enabled = true;
        isFrozen = false;
    }


}
