using UnityEngine;
using Pathfinding;

public class DiagonalMoveEnemy : Enemy // or Character, depending on your base class
{
    [SerializeField] private Vector3 homePoint;
    [SerializeField] private Vector2 roamAreaSize = new Vector2(10f, 10f); // Specify your roaming area size here


    private int currentWaypoint = 0;
    private bool isMovingDiagonal = false;
    private Vector2 diagonalDirection;

    // Rest of your class...

    protected override void Awake()
    {
        base.Awake();

        homePoint = this.transform.position;
        animator.SetBool("isMoving", false);
        SetRandomDiagonalDirection();
    }

    protected override void InsideChaseRadiusUpdate()
    {
        if (!isMovingDiagonal || reachedEndOfPath)
        {
            SetRandomDiagonalDirection();
        }

        MoveUsingAStarPath();
    }

    private void SetRandomDiagonalDirection()
    {
        float xDir = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDir = Random.Range(0, 2) == 0 ? -1 : 1;

        diagonalDirection = new Vector2(xDir, yDir).normalized;
        isMovingDiagonal = true;
    }

    private void MoveUsingAStarPath()
    {
        if (seeker.IsDone() && !reachedEndOfPath)
        {
            // Calculate the target position based on the diagonal direction
            Vector3 targetPosition = (Vector2)transform.position + diagonalDirection;

            // Start a new A* path calculation from the current position to the target position
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }

        if (!reachedEndOfPath)
        {
            // Move along the calculated path
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        Vector3 moveDirection = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 moveDelta = moveDirection * moveSpeed * speedModifier * Time.deltaTime;
        transform.position += moveDelta;

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.1f)
        {
            currentWaypoint++;
        }
    }

    private void OnPathComplete(Path p)
    {
        if (p is ABPath abPath && !abPath.error)
        {
            path = abPath;
            currentWaypoint = 0;
            reachedEndOfPath = false;
        }
    }
}