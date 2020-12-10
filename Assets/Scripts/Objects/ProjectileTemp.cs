using UnityEngine;

public abstract class ProjectileTemp : Projectile
{
    protected abstract float destroyTime { get; }

    protected override void Update() {} // Need empty override to disable base projectile lifetime mechanic

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            OnHitReceiver(other.transform);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (rigidbody != null)
        {
            Debug.Log(this.name + " collided with: " + other.transform.name);
            rigidbody.velocity = Vector2.zero;
            OnHitReceiver(other.transform);
        }
    }
}
