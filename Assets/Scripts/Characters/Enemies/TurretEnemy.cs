using System.Collections;
using UnityEngine;

public class TurretEnemy : SimpleEnemy
{
    [Header("AbilityValues")]
    public GameObject projectile;
    public bool canAttack = false;
    public float fireDelay;
    [SerializeField] protected int amountOfProjectiles = 1;
    [SerializeField] protected int spreadAngle = 0;
    [SerializeField] protected float radius = 0;
    [SerializeField] protected float timeBetweenProjectiles = 1f;
    [SerializeField] protected float fireDelaySeconds;
    [SerializeField] protected float projectileDestructionTimer=1f;
    [SerializeField] protected float projectileYOffset = 0.25f;

    protected virtual void Update()
    {
        if (currentState is Schwer.States.Knockback) return;

        if(cantMove == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canAttack = true;
                fireDelaySeconds = fireDelay;
            }
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
        animator.SetBool("Attacking", true);
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This is the time the enemy stands still for casting
        this.moveSpeed = originalMovespeed;

        if( timeBetweenProjectiles == 0)                    // Play Sound only once if there are many spells at the same time
        {
            SoundManager.RequestSound(attackSounds.GetRandomElement());
        }
        var offsets = RadialLayout.GetOffsets(amountOfProjectiles, MathfEx.Vector2ToAngle(GetAnimatorXY()), spreadAngle);
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            // Should probably not hard-code the offset.
            var position = new Vector2(transform.position.x, transform.position.y + projectileYOffset);      // Set projectile higher since transform is at player's pivot point (feet).
            CreateProjectile(projectile, position + (offsets[i] * radius), offsets[i].normalized);
            
            if (timeBetweenProjectiles > 0)
            {
                yield return new WaitForSeconds(timeBetweenProjectiles); // Play sound every time if there are many spells slightly off
                SoundManager.RequestSound(attackSounds.GetRandomElement());
            }

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
        fireDelay = fireDelay / 3;
    }

    public void setFireDelaySeconds(float timeTillAttack)
    {
        fireDelaySeconds = timeTillAttack;
    }

    private Projectile CreateProjectile(GameObject prefab, Vector2 position, Vector2 direction)
    {
        var projectile = Projectile.Instantiate(prefab, position, direction, Projectile.CalculateRotation(direction), "Player");
        return projectile;
    }

}
