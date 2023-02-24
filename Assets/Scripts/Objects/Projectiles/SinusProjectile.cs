using UnityEngine;

public class SinusProjectile : Projectile
{
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 1f;
    // [SerializeField] private float phase = 45f;  //! Unused

    private Vector2 axis;
    private float time = 0f;

    private void Start()
    {
        axis = rigidbody.velocity.normalized;
    }

    protected override void Update()
    {
        base.Update();

        //! Should this be in FixedUpdate?
        time += Time.deltaTime;
        if (rigidbody) rigidbody.velocity = GetProjectileVelocity(axis, projectileSpeed, time, frequency, amplitude);
    }

    // https://old.reddit.com/r/Unity2D/comments/7qdmyq/sine_wave_projectile_movement/dsvjgim/
    private Vector2 GetProjectileVelocity(Vector2 forward, float speed, float time, float frequency, float amplitude) {
        var up = new Vector2(-forward.y, forward.x);
        float velocity = Mathf.Cos(time * frequency) * amplitude * frequency;
        return up * velocity + forward * speed;
    }
}
