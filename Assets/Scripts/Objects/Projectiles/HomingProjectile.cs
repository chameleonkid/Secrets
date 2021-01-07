using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    protected Transform target;
    [SerializeField] protected float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
         rigidbody.velocity = Vector2.zero;  // Needed to stop the rigidbody working vs the transform change
         this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, projectileSpeed * Time.deltaTime); // 3 = Speed aktuell noch Hardcoded -> Projectile müssen Ihren Speed kennen...

  
      // Suggested way to move the rigidBody looks strange....
      /*
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rigidbody.MovePosition(transform.position + direction * 5 * Time.deltaTime);
        */

        LifetimeCountdown();
    }
}
