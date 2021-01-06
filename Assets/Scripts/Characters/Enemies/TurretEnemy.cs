using System.Collections;
using UnityEngine;

public class TurretEnemy : EnemyLog
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public float projectileSpeed;

    protected virtual void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    protected override void InsideChaseRadiusUpdate()
    {
        if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
        {
            if (canFire)
            {
                currentState = State.attack;
                FireProjectile();
            }
            currentState = State.idle;
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentState = State.idle;
    }

    protected virtual void FireProjectile()
    {
        animator.Play("Attacking");
        var difference = target.transform.position - transform.position;
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().rigidbody.velocity = difference.normalized * projectileSpeed;
        canFire = false;
        currentState = State.walk;
    }

    private IEnumerator fireCo()
    {
        currentState = State.attack;
        yield return new WaitForSeconds(0.3f);
    }
}
