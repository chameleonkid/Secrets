using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaypointNPC : Character
{
    public override float health { get => 1; set { } }   //! Temp

    [Header("Waypoint NPC")]
    public float speed;
    public GameObject[] waypoints;
    public float waitTime;
    private float waitTimeSeconds;
    private bool isMoving;
    [SerializeField] private int currentWaypointIndex = 0;

    private Interactable interactable;
    private Seeker seeker;
    private Path path;

    protected override void Awake()
    {
        base.Awake();

        interactable = GetComponent<Interactable>();
        seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        waitTimeSeconds = waitTime;
        ChangeDirection();
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();

            // Set isMoving parameter on animator only when NPC is moving
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                isMoving = true;
                waitTimeSeconds = waitTime;
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                
            }
        }
    }

    protected void ChangeDirection()
    {
        Vector3 directionVector = (waypoints[currentWaypointIndex].transform.position - transform.position).normalized;
        animator.SetBool("isMoving", true);
        SetAnimatorXY(directionVector);
    }

    private void Move()
    {
        // Get the start position
        Vector3 startPosition = transform.position;

        // Get the target position
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;

        // Start the pathfinding calculation
        seeker.StartPath(startPosition, targetPosition, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            path.Claim(this);
            path.vectorPath.Insert(0, transform.position);
        }
        else
        {
            Debug.LogError("Pathfinding error: " + p.errorLog);
        }
    }

    private void FixedUpdate()
    {
        if (path != null && path.vectorPath != null && path.vectorPath.Count > 1)
        {
            // Move the NPC towards the next waypoint along the path
            Vector3 directionVector = (path.vectorPath[1] - transform.position).normalized;
            Vector3 temp = transform.position + directionVector * speed * Time.deltaTime;
            rigidbody.MovePosition(temp);

            // Check if the NPC has reached the current waypoint
            if (Vector3.Distance(transform.position, path.vectorPath[1]) <= 0.15f)
            {
                path.vectorPath.RemoveAt(0);
              //  ChangeDirection();
            }
        }
        else
        {
            isMoving = false;
        }
    }
}