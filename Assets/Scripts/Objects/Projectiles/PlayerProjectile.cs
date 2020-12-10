using UnityEngine;

public class PlayerProjectile : Projectile
{
    [Tooltip("How long to delay calling `Destroy` after hitting a collider")]
    [SerializeField] protected float destroyDelay;

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

    protected void OnHitReceiver(Transform receiver)
    {
        rigidbody.velocity = Vector2.zero;
        collider.enabled = false;
        Destroy(this.gameObject, destroyDelay);

        AttachToReceiver(receiver);
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
