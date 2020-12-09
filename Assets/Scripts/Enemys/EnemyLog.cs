using UnityEngine;

public class EnemyLog : Enemy
{
    [SerializeField] protected Transform target;

    protected virtual void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        animator.SetBool("WakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance() //make sure to make it overrideable
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                SetAnimatorXYSingleAxis(temp - transform.position);
                rigidbody.MovePosition(temp);
                currentState = EnemyState.walk;
                animator.SetBool("WakeUp", true);
            }

        }
        if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("WakeUp", false);
            //   ChangeState(EnemyState.idle);
        }
    }
}
