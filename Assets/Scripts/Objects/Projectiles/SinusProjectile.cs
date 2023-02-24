using UnityEngine;
public class SinusProjectile : Projectile
{
    private Projectile projectile;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float phase = 45f;
    private float time = 0f;


    protected virtual void Awake()
    {
        projectile = GetComponent<SinusProjectile>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        lifetimeCountdown = lifetime;
    }

    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;
        float x = rigidbody.velocity.x;
        float y = Mathf.Sin(time * frequency) * amplitude;

        if (Mathf.Abs(rigidbody.velocity.y) > Mathf.Abs(rigidbody.velocity.x))
        {
            x = rigidbody.velocity.y * Mathf.Sign(rigidbody.velocity.x);
        }

        Vector2 position = rigidbody.position + new Vector2(x, y) * Time.deltaTime;
        rigidbody.MovePosition(position);
    }


}
