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
    private int currentWaypointIndex = 0;

    private List<Vector3> pathPoints = new List<Vector3>(); // Store the points along the path
    private int currentPathIndex = 0; // Keep track of the current point in the path

    private Interactable interactable;

    protected override void Awake()
    {
        base.Awake();

        interactable = GetComponent<Interactable>();
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
                ChangeDirection();
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
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;

        // Get the start and target nodes
        GraphNode startNode = AstarPath.active.GetNearest(transform.position).node;
        GraphNode targetNode = AstarPath.active.GetNearest(targetPosition).node;
        Vector3 startPoint = (Vector3)startNode.position;
        Vector3 targetPoint = (Vector3)targetNode.position;
        // Create a new ABPath and set its start and end points
        ABPath path = ABPath.Construct(startPoint, targetPoint, null);
        path.heuristic = Heuristic.Manhattan;

        // Start the pathfinding calculation
        AstarPath.StartPath(path);

        // Move the NPC towards the next waypoint along the path
        Vector3 directionVector = (targetPosition - transform.position).normalized;
        Vector3 temp = transform.position + directionVector * speed * Time.deltaTime;
        rigidbody.MovePosition(temp);

        // Check if the NPC has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) <= 0.05f)
        {
            isMoving = false;
        }
    }
}