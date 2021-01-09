﻿using System.Collections;
using UnityEngine;

public class TurretEnemy : SimpleEnemy
{
    [Header("AbilityValues")]
    public GameObject projectile;
    public bool canFire = false;
    public float fireDelay;
    public float fireDelayReduce = 1;
    [SerializeField] private float fireDelaySeconds;


    protected virtual void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay / fireDelayReduce;
        }
    }

    protected override void InsideChaseRadiusUpdate()
    {
        if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
        {
            if (canFire)
            {
                currentState = State.attack;
                FireProjectile();
            }
            currentState = State.idle;
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentState = State.idle;
    }

    protected virtual void FireProjectile()
    {
        StartCoroutine(FireCo());
        canFire = false;
        currentState = State.walk;
    }

    protected virtual IEnumerator FireCo()
    {
        var originalMovespeed = this.moveSpeed;
        animator.Play("Attacking");
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;
        var difference = target.transform.position - transform.position;
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        var projSpeed = proj.GetComponent<Projectile>().projectileSpeed;
        proj.GetComponent<Projectile>().rigidbody.velocity = difference.normalized * projSpeed;
    }
}
