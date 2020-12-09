using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;
    [Header("Lifetime Vars")]
    [SerializeField] protected float lifetime;
    protected float lifetimeCountdown;

    public new Rigidbody2D rigidbody { get; protected set; }

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

    public void Launch(Vector2 initialVel)
    {
        rigidbody.velocity = initialVel * moveSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile is collided with " + other.gameObject.name);
        Destroy(this.gameObject);
    }
}
