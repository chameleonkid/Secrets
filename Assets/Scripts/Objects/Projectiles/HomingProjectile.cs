using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    protected Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rigidbody.velocity = Vector2.zero;  // Needed to stop the rigidbody working vs the transform change
    }

    // Update is called once per frame
    protected override void Update()
    {
       
         this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, projectileSpeed * Time.deltaTime);
   
    // Suggested way to move the rigidBody looks strange....
    /*
    Vector3 direction = (target.transform.position - transform.position).normalized;
    rigidbody.MovePosition(transform.position + direction * 5 * Time.deltaTime);
    */
        LifetimeCountdown();
    }
}
