using System.Collections;
using UnityEngine;

public class TurretEnemy : SimpleEnemy
{
    [Header("AbilityValues")]
    public GameObject projectile;
    public bool canAttack = false;
    public float fireDelay;
    [SerializeField] protected int amountOfProjectiles = 1;
    [SerializeField] protected float timeBetweenProjectiles = 1;
    [SerializeField] protected float fireDelaySeconds;

    protected virtual void Update()
    {
        if (currentState is Schwer.States.Knockback) return;

        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canAttack = true;
            fireDelaySeconds = fireDelay;
        }
    }

    protected override void InsideChaseRadiusUpdate()
    {
        if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
        {
            if (canAttack)
            {
                currentStateEnum = StateEnum.attack;
                animator.SetTrigger("isAttacking");
                FireProjectile();
            }
            currentStateEnum = StateEnum.walk;
            
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentStateEnum = StateEnum.idle;
        //animator.SetBool("isMoving", false);
        animator.SetTrigger("isSleeping");
    }

    protected virtual void FireProjectile()
    {
        StartCoroutine(FireCo());
        canAttack = false;
        currentStateEnum = StateEnum.walk;
    }

    protected virtual IEnumerator FireCo()
    {
        var originalMovespeed = this.moveSpeed;
        //animator.Play("Attacking");
        //SoundManager.RequestSound(attackSounds.GetRandomElement());
        animator.SetBool("Attacking", true);
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;

        
        for(int i = 0; i <= amountOfProjectiles-1; i++)
        {
            var difference = target.transform.position - transform.position;
            Projectile.Instantiate(projectile, transform.position, difference, Quaternion.identity, "Player");
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
        animator.SetBool("Attacking", false);

    }

    public void setAmountOfPojectiles(int amount)
    {
        amountOfProjectiles = amount-1;
    }

    public void setTimeBetweenProjectiles(float timeBetween)
    {
        timeBetweenProjectiles = timeBetween;
    }

    public void HalfCooldown()
    {
        fireDelay = fireDelay / 2;
    }

    public void setFireDelaySeconds(float timeTillAttack)
    {
        fireDelaySeconds = timeTillAttack;
    }
}
