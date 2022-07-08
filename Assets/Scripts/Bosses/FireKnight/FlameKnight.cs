using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameKnight : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private float idleMoveSpeed = default;
    [SerializeField] private Vector2 idleMoveDirection;


    [Header("AttackUpNDown")]
    [SerializeField] private float attackMoveSpeed = default;
    [SerializeField] private Vector2 attackMoveDirection;

    [Header("AttackPlayer")]
    [SerializeField] private float attackPlayerSpeed = default;
    [SerializeField] private Transform player = default;
    [SerializeField] private bool hasPlayerPosition;
    [Header("Target")]
    [SerializeField] private Vector2 playerPosition;


    [Header("Orienation")]
    [SerializeField] private Transform groundCheckUp = default;
    [SerializeField] private Transform groundCheckDown = default;
    [SerializeField] private Transform groundCheckWall = default;
    [SerializeField] private float groundCheckRadius = default;
    [SerializeField] private LayerMask wallLayer = default;        // ProjectileCollision
    [SerializeField] private bool goingUp = true;
    [SerializeField] private bool isTouchingUp;
    [SerializeField] private bool isTouchingDown;
    [SerializeField] private bool goingLeft = true;
    [SerializeField] private bool isTouchingWall;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody2D enemyRB;

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D hurtBox = default;
    [SerializeField] private GameObject hitBox = default;
    [SerializeField] private GameObject bossTrigger = default;
    [SerializeField] private bool fightHasStarted = false;
    [SerializeField] private GameObject closeEntrance;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Sounds")]
    [SerializeField] private AudioClip fireKnightLaugh = default;
    [SerializeField] private AudioClip fireKnightCharge = default;
    [SerializeField] private AudioClip fireKnightWW = default;
    [SerializeField] private AudioClip fireKnightSlam = default;


    [Header("Choose random things to spawn of this:")]
    [SerializeField] private GameObject[] thingToSpawn;
    [Header("How many Randoms should be spawned?:")]
    [SerializeField] private int spawnCounter;
    [Header("How much delay between spawns?:")]
    [SerializeField] private float spawnDelay;
    [Header("How long will the thing remain?:")]
    [SerializeField] private float destroyTime;


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
            SoundManager.RequestSound(fireKnightWW);
            anim.SetTrigger("AttackUpAndDown");
        }
        
        else if( randomState == 1)
        {
            SoundManager.RequestSound(fireKnightCharge);
            anim.SetTrigger("AttackPlayer");
        }
        
        else if (randomState == 2)
        {
            SoundManager.RequestSound(fireKnightLaugh);
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
        if(closeEntrance)
        {
            closeEntrance.SetActive(true);
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
        if(isTouchingDown || isTouchingWall)
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

    public void StartFlames()
    {
        StartCoroutine(StartFlamesCo());
    }

    private IEnumerator StartFlamesCo()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
        for (int i = 0; i < spawnCounter; i++)
        {
            Vector2 homePos = this.transform.position;
            var randomSpawn = Random.Range(0, thingToSpawn.Length);
            var spawn = Instantiate(thingToSpawn[randomSpawn], this.transform.position, Quaternion.identity);  //this.transform.position needs to vary slightly for iteration
            yield return new WaitForSeconds(spawnDelay);
            Destroy(spawn, destroyTime);
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
