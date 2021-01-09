using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPumpkin : TurretEnemy
{
    [Header("AbilityValues Boss")]
    public GameObject projectileTwo;
    public bool canFireTwo = false;
    public float fireDelayTwo;
    public float fireDelayTwoReduce = 1;
    [SerializeField] private float fireDelaySecondsTwo;


    protected override void Update()
    {
        base.Update();

        fireDelaySecondsTwo -= Time.deltaTime;
        if (fireDelaySecondsTwo <= 0)
        {
            canFireTwo = true;
            fireDelaySecondsTwo = fireDelayTwo / fireDelayTwoReduce;
        }
    }

    protected override void InsideChaseRadiusUpdate()
    {
        base.InsideChaseRadiusUpdate();

        if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
        {
            if (canFireTwo)
            {
                currentState = State.attack;
                FireProjectileTwo();
            }
            currentState = State.idle;
        }

    }


    protected virtual void FireProjectileTwo()
    {
        StartCoroutine(FireTwoCo());
        canFireTwo = false;
        currentState = State.walk;
    }


    protected virtual IEnumerator FireTwoCo()
    {
        var originalMovespeed = this.moveSpeed;
        animator.Play("Attacking 2");
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;
        var proj = Instantiate(projectileTwo, target.transform.position, Quaternion.identity);

    }
}
