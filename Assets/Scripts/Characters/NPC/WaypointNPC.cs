using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaypointNPC : Character
{
    public override float health { get => 1; set { } } // Temporary
    [Header("Waypoint NPC")]
    public float speed;
    public GameObject[] waypoints;
    public float waitTime;
    private float waitTimeSeconds;
    private int currentWaypointIndex = 0;

    private Interactable interactable;
    private Seeker seeker;
    private Path path;

    private bool isMoving = false;
    private bool isCalculatingPath = false;

    protected override void Awake()
    {
        base.Awake();

        interactable = GetComponent<Interactable>();
        seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        waitTimeSeconds = waitTime;
        CalculatePath(); // Calculate the initial path
    }

    private void Update()
    {
        // Only update isMoving here
        if (path != null && path.vectorPath != null && path.vectorPath.Count > 1)
        {
            isMoving = true; // Start moving if a valid path exists
        }
        else
        {
            isMoving = false;
            // Handle waitTime and waypoint switching here
            waitTimeSeconds -= Time.deltaTime;
            if (!isCalculatingPath && waitTimeSeconds <= 0)
            {
                waitTimeSeconds = waitTime;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                CalculatePath(); // Calculate a new path when it's time to move
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        if (path != null && path.vectorPath != null && path.vectorPath.Count > 1)
        {
            Vector3 directionVector = (path.vectorPath[1] - transform.position).normalized;
            Vector3 temp = transform.position + directionVector * speed * Time.fixedDeltaTime;
            rigidbody.MovePosition(temp);
            SetAnimatorXY(directionVector);

            if (Vector3.Distance(transform.position, path.vectorPath[1]) <= 0.15f)
            {
                path.vectorPath.RemoveAt(0);
            }
        }
    }

    private void CalculatePath()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;

        isCalculatingPath = true;
        seeker.StartPath(startPosition, targetPosition, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        isCalculatingPath = false;

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
}