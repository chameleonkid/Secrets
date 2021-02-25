using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameKnight : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private float idleMoveSpeed;
    [SerializeField] private Vector2 idleMoveDirection;


    [Header("AttackUpNDown")]
    [SerializeField] private float attackMoveSpeed;
    [SerializeField] private Vector2 attackMoveDirection;

    [Header("AttackPlayer")]
    [SerializeField] private float attackPlayerSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private bool hasPlayerPosition;
    [Header("Target")]
    [SerializeField] private Vector2 playerPosition;


    [Header("Orienation")]
    [SerializeField] private Transform groundCheckUp;
    [SerializeField] private Transform groundCheckDown;
    [SerializeField] private Transform groundCheckWall;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask wallLayer;        // ProjectileCollision
    [SerializeField] private bool goingUp = true;
    [SerializeField] private bool isTouchingUp;
    [SerializeField] private bool isTouchingDown;
    [SerializeField] private bool goingLeft = true;
    [SerializeField] private bool isTouchingWall;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody2D enemyRB;

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D hurtBox;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject bossTrigger;
    [SerializeField] private bool fightHasStarted = false;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Sounds")]
    [SerializeField] private AudioClip fireKnightLaugh;
    [SerializeField] private AudioClip fireKnightCharge;
    [SerializeField] private AudioClip fireKnightWW;
    [SerializeField] private AudioClip fireKnightSlam;

    // Start is called before the first frame update
    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, wallLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, wallLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, wallLayer);
        // IdleState();
        // AttackState();
    }

    void RandomStatePicker()
    {
        int randomState = Random.Range(0, 3);
        if( randomState == 0)
        {
            anim.SetTrigger("AttackUpAndDown");
        }
        
        else if( randomState == 1)
        {
            anim.SetTrigger("AttackPlayer");
        }
        
        else if (randomState == 2)
        {
            //Stay in Idle
            return;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!fightHasStarted)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                StartFlameKnight();
                fightHasStarted = true;
            }
        }
    }

    public void StartFlameKnight()
    {
        if(fireKnightLaugh)
        {
            SoundManager.RequestSound(fireKnightLaugh);
        }
        anim.SetTrigger("StartFight");
        bossTrigger.SetActive(false);
        StartCoroutine(ActivateHurtBoxCo());
    }

    public void StopMoving()
    {
        enemyRB.velocity = Vector2.zero;
    }

   public void IdleState()
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

    public void AttackPlayer()
    {
        if(!hasPlayerPosition)
        {
            // take player position
            playerPosition = player.position - transform.position;
            // normalize position
            playerPosition.Normalize();
            // Attack that position
            hasPlayerPosition = true;
        }

        if(hasPlayerPosition)
        {
            enemyRB.velocity = playerPosition * attackPlayerSpeed;
        }
        if(isTouchingDown || isTouchingUp || isTouchingWall)
        {
            StopMoving();
            hasPlayerPosition = false;
            anim.SetTrigger("Slammed");
        }
    }


    public void AttackState()
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


    public void FlipTowardPlayer()
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


    public void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }

    public void FlipDirection()
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

    IEnumerator ActivateHurtBoxCo()
    {
        yield return new WaitForSeconds(3f);
        hurtBox.enabled = true;
        hitBox.SetActive(true);
    }
}
