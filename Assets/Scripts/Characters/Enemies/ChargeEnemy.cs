using System.Collections;
using UnityEngine;

public class ChargeEnemy : Enemy
{

    [SerializeField] private bool hasPlayerPosition = false;
    [SerializeField] private bool isCharging = false;
    [SerializeField] private Vector3 chargePosition;
    [SerializeField] private float chargeSpeed = 10;
    protected override void FixedUpdate()
    {
        //! Temporary!
        if (currentState is Schwer.States.Knockback)
        {
            currentState.FixedUpdate();
            return;
        }

        if(isCharging)
        {

        }
        
        else
        {
            rigidbody.velocity = Vector2.zero;
        }

        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (leftChaseRadius)
            {
                SoundManager.RequestSound(inRangeSounds.GetRandomElement());
            }
            leftChaseRadius = false;
            if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
            {
                if (path == null)
                {
                    return;
                }
                if (currentWaipoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    return;
                }
                animator.SetBool("isMoving", true);
                Vector3 temp = Vector3.MoveTowards(rigidbody.position, path.vectorPath[currentWaipoint], moveSpeed * speedModifier * Time.deltaTime);
                rigidbody.MovePosition(temp);
                float wayPointdistance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaipoint]);
                SetAnimatorXYSingleAxis(temp - transform.position);

                if (wayPointdistance < nextWaypointDistance)
                {
                    currentWaipoint++;
                }
                else
                {
                    reachedEndOfPath = false;
                }
            }
        }
        else if (distance <= chaseRadius && distance <= attackRadius)
        {
            if ((currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk) && currentStateEnum != StateEnum.stagger)
            {
                AttackPlayer();
            }
        }
        else if (distance >= chaseRadius && distance >= attackRadius)
        {
            leftChaseRadius = true;
            randomMovement();
        }
    }
    
    public IEnumerator ChargeCo()
    {
        animator.SetBool("isMoving", false);
        isCharging = true;
        Debug.Log("ChargeCo has started");
        currentStateEnum = StateEnum.attack;
        animator.SetTrigger("PrepareCharge");
        yield return new WaitForSeconds(0.5f); //ChargePrepareTime
        animator.SetTrigger("isCharging");
        rigidbody.velocity = chargePosition * chargeSpeed;
        if(transform.position == chargePosition)
        {
            rigidbody.velocity = Vector2.zero; //If reached Chargedestination stop NOT WORKING!?
        }
        yield return new WaitForSeconds(0.25f); //Chargeduration
        rigidbody.velocity = Vector2.zero;
        animator.SetTrigger("isCoolingDown");
        Debug.Log("I charged!");
        yield return new WaitForSeconds(2f); // Stay at Chargedestination for a while and 
        animator.SetBool("isMoving", true);
        isCharging = false;
        hasPlayerPosition = false;
        currentStateEnum = StateEnum.idle;
        Debug.Log("I waited X seconds and charge again!");


    }
    

    public void AttackPlayer()
    {

        if (!hasPlayerPosition)
        {
            // take player position
            chargePosition = target.position - transform.position;
            // normalize position
            chargePosition.Normalize();
            // Attack that position
            hasPlayerPosition = true;
        }

        if (hasPlayerPosition )
        {
            StartCoroutine(ChargeCo());

        }
    }


}
