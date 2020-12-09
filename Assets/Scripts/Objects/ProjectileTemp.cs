using UnityEngine;

public abstract class ProjectileTemp : Projectile
{
    public bool isChild = false;
    
    protected abstract float destroyTime { get; }
    protected abstract Collider2D projectileCollider { get; }

    protected override void Update()
    {
        if (isChild == false && rigidbody.velocity == Vector2.zero)
        {
            DestroyProjectile(null);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            DestroyProjectile(other.transform);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (!isChild)
        {
            Debug.Log(this.name + " collided with: " + other.transform.name);
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void DestroyProjectile(Transform other)
    {
        rigidbody.velocity = Vector2.zero;
        projectileCollider.enabled = false;
        Destroy(this.gameObject, destroyTime);
        
        // Not sure what this code is for
        Destroy(rigidbody);
        transform.SetParent(other);
        isChild = true;
    }
}
