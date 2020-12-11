using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifetime;
    protected float lifetimeCountdown;

    public new Rigidbody2D rigidbody { get; protected set; }
    protected new Collider2D collider;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        
        lifetimeCountdown = lifetime;
    }

    protected virtual void Update() => LifetimeCountdown();

    protected void LifetimeCountdown()
    {
        lifetimeCountdown -= Time.deltaTime;
        if (lifetimeCountdown <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) => Destroy(this.gameObject);

    public static Quaternion CalculateRotation(Vector2 direction)
    {
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * Vector3.forward;
        return Quaternion.Euler(rotation);
    }
}
