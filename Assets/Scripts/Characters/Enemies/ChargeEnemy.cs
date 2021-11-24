using System.Collections;
using UnityEngine;

public class ChargeEnemy : Enemy
{

    [SerializeField] private bool hasPlayerPosition = false;
    [SerializeField] private Vector2 chargePosition;
    [SerializeField] private float chargeSpeed = 10;
    protected override void FixedUpdate()
    {
        //! Temporary!
        if (currentState is Schwer.States.Knockback)
        {
            currentState.FixedUpdate();
            return;
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
        Debug.Log("ChargeCo has started");
        currentStateEnum = StateEnum.attack;
        yield return new WaitForSeconds(1f);
        rigidbody.velocity = chargePosition * chargeSpeed;
        Debug.Log("I charged!");
        yield return new WaitForSeconds(5f);
        hasPlayerPosition = false;
        currentStateEnum = StateEnum.walk;
        Debug.Log("I waited 5 seconds and charge again!");

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

        if (hasPlayerPosition)
        {
            StartCoroutine(ChargeCo());

        }
    }


}
