using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gruzMother : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private float idleMoveSpeed;
    [SerializeField] private Vector2 idleMoveDirection;

    [Header("AttackUpNDown")]
    [SerializeField] private float attackMoveSpeed;
    [SerializeField] private Vector2 attackMoveDirection;

    [Header("AttackUpNDown")]
    [SerializeField] private float attackPlayerSpeed;
    [SerializeField] private Transform player;

    [Header("Other")]
    [SerializeField] private Transform groundCheckUp;
    [SerializeField] private Transform groundCheckDown;
    [SerializeField] private Transform groundCheckWall;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask wallLayer;        // ProjectileCollision
    [SerializeField] private bool isTouchingUp;
    [SerializeField] private bool isTouchingDown;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private Rigidbody2D enemyRB;
    [SerializeField] private bool goingUp = true;
    [SerializeField] private bool goingLeft = true;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, wallLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, wallLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, wallLayer);
       // IdleState();
       // AttackState();
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            AttackPlayer();
            anim.Play("Attack 2");

        }
        FlipTowardPlayer();

    }

    void IdleState()
    {
        if(isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if(isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        enemyRB.velocity = idleMoveSpeed * idleMoveDirection;

        if(isTouchingWall)
        {
            if(goingLeft)
            {
                FlipDirection();
            }    
            else if(!goingLeft)
            {
                FlipDirection();
            }
        }
        
    }

    void AttackPlayer()
    {

        // take player position
        playerPosition = player.position - transform.position;
        // normalize position
        playerPosition.Normalize();
        // Attack that position
        enemyRB.velocity = playerPosition * attackPlayerSpeed;

    }


    void AttackState()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        enemyRB.velocity = attackMoveSpeed * attackMoveDirection;

        if (isTouchingWall)
        {
            if (goingLeft)
            {
                FlipDirection();
            }
            else if (!goingLeft)
            {
                FlipDirection();
            }
        }

    }


    void FlipTowardPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;

        if(playerDirection > 0 && goingLeft)
        {
            FlipDirection();
        }
        else if (playerDirection < 0 && !goingLeft)
        {
            FlipDirection();
        }
    }


    void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }

    void FlipDirection()
    {
        goingLeft = !goingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);        // Maybe replace this
        // Set Animation to the right side
        // if goingLeft animator.play(WalkLeft)
        // if !goingLeft animator.play(WalkRight)
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }
}
