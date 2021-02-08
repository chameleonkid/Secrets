using UnityEngine;

public class HomingProjectile : Projectile
{
    protected Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rigidbody.velocity = Vector2.zero;  // Needed to stop the rigidbody working vs the transform change
    }

    protected override void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, projectileSpeed * Time.deltaTime);

        LifetimeCountdown();
    }
}
