using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyLog
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void CheckDistance() //override from EnemyLog 
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("Moving", true);
            }

        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (Vector3.Distance(target.position, transform.position) >= chaseRadius && Vector3.Distance(target.position, transform.position) >= attackRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("Moving", false);
        }

    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("Attacking", true);
        yield return null;
        anim.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.5f); //Attack CD
        currentState = EnemyState.walk;
        // anim.SetBool("Attacking", false);
    }

}
