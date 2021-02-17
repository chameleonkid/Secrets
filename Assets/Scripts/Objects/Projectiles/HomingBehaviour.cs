using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class HomingBehaviour : MonoBehaviour
{
    private Transform target;
    private Projectile projectile;

    private void Awake() => projectile = GetComponent<Projectile>();

    private void Start() => target = GameObject.FindWithTag(projectile.targetTag).transform;

    private void FixedUpdate()
    {
        var rb = projectile.rigidbody;
        if (rb != null) {
            var newPosition = Vector2.MoveTowards(rb.position, target.position, projectile.projectileSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }
}
