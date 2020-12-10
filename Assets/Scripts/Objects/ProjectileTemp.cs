using UnityEngine;

public abstract class ProjectileTemp : Projectile
{
    protected abstract float destroyTime { get; }

    protected override void Update() {} // Need empty override to disable base projectile lifetime mechanic

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            DestroyProjectile(other.transform);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (rigidbody != null)
        {
            Debug.Log(this.name + " collided with: " + other.transform.name);
            rigidbody.velocity = Vector2.zero;
            DestroyProjectile(other.transform);
        }
    }

    public void DestroyProjectile(Transform other)
    {
        rigidbody.velocity = Vector2.zero;
        collider.enabled = false;
        Destroy(this.gameObject, destroyTime);

        AttachToReceiver(other);
    }

    protected void AttachToReceiver(Transform receiver)
    {
        if (receiver != null)
        {
            Destroy(rigidbody);
            transform.SetParent(receiver);
        }
    }
}
