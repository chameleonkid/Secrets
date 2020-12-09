using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed; // TurretEnemy supplies their own speed, need to modify
    [SerializeField] protected float lifetime;
    protected float lifetimeCountdown;

    public new Rigidbody2D rigidbody { get; protected set; }

    public virtual void SetVelocity(Vector2 direction) => rigidbody.velocity = direction * moveSpeed;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lifetimeCountdown = lifetime;
    }

    protected virtual void Update()
    {
        LifetimeCountdown();
    }

    protected void LifetimeCountdown()
    {
        lifetimeCountdown -= Time.deltaTime;
        if (lifetimeCountdown <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile is collided with " + other.gameObject.name);
        Destroy(this.gameObject);
    }

    public static Quaternion CalculateRotation(Vector2 direction)
    {
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * Vector3.forward;
        return Quaternion.Euler(rotation);
    }
}
