using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float chaseRadius;
    public float nextWaypointDistance = 0.5f;
    Animator animator;

    Path path;
    int currentWaipoint = 0;
    bool reachedEndOfPath;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && IsInRange())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaipoint = 0;
        }
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }




    public void MoveToPlayer()
    {
        if(IsInRange())
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
            Vector3 temp = Vector3.MoveTowards(rb.position, path.vectorPath[currentWaipoint], moveSpeed * Time.deltaTime);
            rb.MovePosition(temp);
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
            SetAnimatorXYSingleAxis(temp - transform.position);

            if (distance < nextWaypointDistance)
            {
                currentWaipoint++;
            }
            else
            {
                reachedEndOfPath = false;
            }
        }
        else
        {
            seeker.StartPath(rb.position, rb.position);
            animator.SetBool("isMoving", false);
        }

    }

    protected bool IsInRange()
    {
        if(Vector2.Distance(transform.position, target.transform.position) <= chaseRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected void SetAnimatorXYSingleAxis(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetAnimatorXY(direction * Vector2.right);
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            SetAnimatorXY(direction * Vector2.up);
        }
    }

    protected void SetAnimatorXY(Vector2 direction)
    {
        direction.Normalize();
        if (direction != Vector2.zero)
        {
            // Need to round since animator expects integers
            direction.x = Mathf.Round(direction.x);
            direction.y = Mathf.Round(direction.y);

            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        }
    }


}
