using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class HomingPlayerProjectile : MonoBehaviour
{
    private Transform target;
    private Projectile projectile;
    [SerializeField] private float initialForceMagnitude = 10;
    [SerializeField] private float homingDelay = 0.25f;
    [SerializeField] private float directionChangeSpeed = 2.0f; // Adjust as needed

    private Rigidbody2D rb; // Rigidbody2D for arrow

    private float homingTimer = 0.0f;
    private bool hasInitialForceApplied = false;
    private Vector2 currentDirection; // The current direction of the arrow

    private void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    private void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Initialize the current direction
        currentDirection = rb.velocity.normalized;
    }

    private void FixedUpdate()
    {
        // If the initial force has been applied and the homing delay timer is up, start homing
        if (hasInitialForceApplied && homingTimer >= homingDelay)
        {
            target = FindClosestEnemy();
            MoveTowardsTarget();
        }
        else
        {
            // Apply an initial force in the facing direction of the player
            if (!hasInitialForceApplied)
            {
                Vector2 facingDirection = transform.up;
                rb.AddForce(facingDirection.normalized * initialForceMagnitude, ForceMode2D.Impulse);
                hasInitialForceApplied = true;
            }

            // Update the homing delay timer
            homingTimer += Time.deltaTime;
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            // Gradually adjust the direction using Lerp
            Vector2 desiredDirection = ((Vector2)target.position - rb.position).normalized;
            currentDirection = Vector2.Lerp(currentDirection, desiredDirection, directionChangeSpeed * Time.deltaTime).normalized;

            // Update the velocity based on the new direction
            rb.velocity = currentDirection * projectile.projectileSpeed;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

            // Apply the rotation to the projectile
            rb.rotation = angle;
        }
    }

    private Transform FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        Debug.DrawLine(transform.position, closestEnemy.transform.position);
        return closestEnemy.transform;
    }
}