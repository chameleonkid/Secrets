using UnityEngine;

public class PlayerHomingProjectile : Projectile
{
    protected Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("enemy").transform;
        rigidbody.velocity = Vector2.zero;  // Needed to stop the rigidbody working vs the transform change
    }

    protected override void Update()    //! Probably should use FixedUpdate and affect rigidbody instead.
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, projectileSpeed * Time.deltaTime);

        LifetimeCountdown();
    }
}
