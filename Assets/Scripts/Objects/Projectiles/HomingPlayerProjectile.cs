using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class HomingPlayerProjectile : MonoBehaviour
{
    private Transform target;
    private Projectile projectile;

    private void Awake() => projectile = GetComponent<Projectile>();

    private void Start() => target = GameObject.FindWithTag(projectile.targetTag).transform;

    private void FixedUpdate()
    {
        target = FindClosestEnemy();
        var rb = projectile.rigidbody;
        if (rb != null)
        {
            var newPosition = Vector2.MoveTowards(rb.position, target.position, projectile.projectileSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }

    private Transform FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach(Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if( distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        return closestEnemy.transform;
    }
}
