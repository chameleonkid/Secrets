using UnityEngine;

public class PlayerProjectile : Projectile
{
    [Tooltip("How long to delay calling `Destroy` after hitting a collider.")]
    [SerializeField] protected float destroyDelay;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            OnHitCollider(other.transform);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) => OnHitCollider(other.transform);

    protected void OnHitCollider(Transform other)
    {
        Debug.Log(this.name + " hit " + other.name);
        collider.enabled = false;
        rigidbody.velocity = Vector2.zero;  // Is this line necessary if we are destroying the rigidbody?
        Destroy(rigidbody);
        transform.SetParent(other);

        lifetimeCountdown = destroyDelay;
    }
}
