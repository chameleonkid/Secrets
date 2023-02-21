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
        var rb = projectile.rigidbody;

        time += Time.deltaTime;
        float x = rb.velocity.x;
        float y = Mathf.Sin(time * frequency + phase) * amplitude;
        Vector2 position = rb.position + new Vector2(x, y) * Time.deltaTime;
        rb.MovePosition(position);
    }





}