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
            Debug.Log(other.name + " entered " + this.name + "'s trigger");
            OnHitCollider(other.transform);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(this.name + " collided with: " + other.transform.name);
        OnHitCollider(other.transform);
    }

    protected void OnHitCollider(Transform other)
    {
        collider.enabled = false;
        rigidbody.velocity = Vector2.zero;  // Is this line necessary if we are destroying the rigidbody?
        Destroy(rigidbody);
        transform.SetParent(other);
        Destroy(this.gameObject, destroyDelay);
    }
}
