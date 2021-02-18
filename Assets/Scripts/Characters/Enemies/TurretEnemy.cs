using System.Collections;
using UnityEngine;

public class TurretEnemy : SimpleEnemy
{
    [Header("AbilityValues")]
    public GameObject projectile;
    public bool canAttack = false;
    public float fireDelay;
    [SerializeField] private float fireDelaySeconds;

    protected virtual void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canAttack = true;
            fireDelaySeconds = fireDelay;
        }
    }

    protected override void InsideChaseRadiusUpdate()
    {
        if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
        {
            if (canAttack)
            {
                currentState = State.attack;
                FireProjectile();
            }
            currentState = State.walk;
            animator.SetBool("isMoving", true);
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentState = State.idle;
        animator.SetBool("isMoving", false);
    }

    protected virtual void FireProjectile()
    {
        StartCoroutine(FireCo());
        canAttack = false;
        currentState = State.walk;
    }

    protected virtual IEnumerator FireCo()
    {
        var originalMovespeed = this.moveSpeed;
        animator.Play("Attacking");
        SoundManager.RequestSound(attackSounds.GetRandomElement());
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;

        var difference = target.transform.position - transform.position;
        Projectile.Instantiate(projectile, transform.position, difference, Quaternion.identity, "Player");
    }

    public void HalfCooldown()
    {
        fireDelay = fireDelay / 2;
    }
}
