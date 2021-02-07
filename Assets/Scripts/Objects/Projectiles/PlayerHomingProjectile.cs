using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingProjectile : PlayerProjectile
{
    protected Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("enemy").transform;
        rigidbody.velocity = Vector2.zero;  // Needed to stop the rigidbody working vs the transform change
    }

    // Update is called once per frame
    protected override void Update()
    {

        this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, projectileSpeed * Time.deltaTime);

        LifetimeCountdown();
    }
}
