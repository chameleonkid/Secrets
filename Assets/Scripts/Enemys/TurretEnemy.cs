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
        if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
        {
            if (canFire)
            {
                FireProjectile();
            }
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        animator.SetBool("WakeUp", false);
        // ChangeState(EnemyState.idle);
    }

    protected virtual void FireProjectile()
    {
        // Debug.Log("FIRE!!!!");
        var difference = target.transform.position - transform.position;
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<BaseProjectile>().Launch(difference.normalized * projectileSpeed);
        canFire = false;
        currentState = EnemyState.walk;
        animator.SetBool("WakeUp", true);
    }
}
