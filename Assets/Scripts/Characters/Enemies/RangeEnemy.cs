using UnityEngine;
using System.Collections;

public class RangeEnemy : TurretEnemy
{
    [SerializeField] private float shootingRange = 3f;
    [SerializeField] private float escapeRange = 1f;
    [SerializeField] private float facingUpdateCooldown = 0.5f; // Time in seconds before the enemy can update facing direction again during avoidance

    private bool isAvoidingPlayer;
    private float lastFacingUpdateTime; // Tracks the last time the enemy updated its facing direction

    protected override void FixedUpdate()
    {
        if (currentState is Schwer.States.Knockback)
        {
            currentState.FixedUpdate();
            return;
        }

        rigidbody.velocity = Vector2.zero;

        float distance = Vector3.Distance(target.position, transform.position);

        // Prioritize actions based on distance to the player
        if (distance <= escapeRange)
        {
            isAvoidingPlayer = true;
            AvoidPlayer();
        }
        else if (distance <= shootingRange)
        {
            isAvoidingPlayer = false;
            HandleAttacking();
        }
        else if (distance <= chaseRadius)
        {
            isAvoidingPlayer = false;
            HandleChasing();
        }
        else
        {
            isAvoidingPlayer = false;
            RandomMovement();
        }
    }

    private void HandleChasing()
    {
        if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk)
        {
            if (path == null) return;

            if (currentWaipoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }

            animator.SetBool("isMoving", true);

            Vector3 targetPosition = Vector3.MoveTowards(rigidbody.position, path.vectorPath[currentWaipoint], moveSpeed * Time.deltaTime);
            rigidbody.MovePosition(targetPosition);

            // Use SetAnimatorXYSingleAxis to set the correct facing direction based on movement
            SetAnimatorXYSingleAxis(targetPosition - transform.position);

            float waypointDistance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaipoint]);

            if (waypointDistance < nextWaypointDistance)
            {
                currentWaipoint++;
            }
            else
            {
                reachedEndOfPath = false;
            }
        }
    }

    private void HandleAttacking()
    {
        currentStateEnum = StateEnum.idle;
        animator.SetBool("isMoving", false);

        // Set facing direction towards the player when attacking
        SetAnimatorXYSingleAxis(target.position - transform.position);

        if (canAttack)
        {
            currentStateEnum = StateEnum.attack;
            FireProjectile();
        }
    }

    protected override void FireProjectile()
    {
        Vector2 directionToPlayer = (target.position - transform.position).normalized; // Calculate the direction to the player
        SetAnimatorXYSingleAxis(directionToPlayer);
        StartCoroutine(FireCo(directionToPlayer)); // Pass the direction to the coroutine
        canAttack = false;
        currentStateEnum = StateEnum.walk;
    }

    protected virtual IEnumerator FireCo(Vector2 direction)
    {
        var originalMovespeed = this.moveSpeed;
        animator.SetBool("Attacking", true);
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f); // This is the time the enemy stands still for casting
        this.moveSpeed = originalMovespeed;

        if (timeBetweenProjectiles == 0)
        {
            SoundManager.RequestSound(attackSounds.GetRandomElement());
        }

        var offsets = RadialLayout.GetOffsets(amountOfProjectiles, MathfEx.Vector2ToAngle(GetAnimatorXY()), spreadAngle);
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            var position = new Vector2(transform.position.x, transform.position.y + projectileYOffset); // Set projectile higher since transform is at player's pivot point (feet).
            CreateProjectile(projectile, position + (offsets[i] * radius), direction); // Use the direction to player plus any offset

            if (timeBetweenProjectiles > 0)
            {
                yield return new WaitForSeconds(timeBetweenProjectiles); // Play sound every time if there are many spells slightly off
                SoundManager.RequestSound(attackSounds.GetRandomElement());
            }
        }
        animator.SetBool("Attacking", false);
    }

    protected override void AvoidPlayer()
    {
        // Calculate avoid direction
        Vector3 avoidDirection = (transform.position - target.position).normalized + GetRandomDirection() * 0.5f;
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, transform.position + avoidDirection, moveSpeed * Time.deltaTime);

        rigidbody.MovePosition(targetPosition);
        animator.SetBool("isMoving", true);

        // Only update facing direction if enough time has passed since the last update
        if (Time.time - lastFacingUpdateTime >= facingUpdateCooldown)
        {
            // Use SetAnimatorXYSingleAxis to set the correct facing direction based on avoidance movement
            SetAnimatorXYSingleAxis(avoidDirection);

            // Update the last facing update time
            lastFacingUpdateTime = Time.time;
        }
    }

    private void RandomMovement()
    {
        randomMovement(); // Using the inherited randomMovement method from the base class
        // Assuming randomMovement method internally handles calling SetAnimatorXYSingleAxis
    }
}