using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLog : Enemy
{
    public Rigidbody2D myRigidbody;
    public Transform target;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("WakeUp", true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance(); 
    }

    public virtual void CheckDistance() //make sure to make it overrideable
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius )
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger )
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
            
        }
        if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("WakeUp", false);
         //   ChangeState(EnemyState.idle);
        }
    }


   public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    public void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                //Debug.Log("WALKING RIGHT");
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
               // Debug.Log("WALKING Left");
            }

        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
                //Debug.Log("WALKING UP");
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
              //  Debug.Log("WALKING DOWN");
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
            {
            currentState = newState;
        }
    }
}
