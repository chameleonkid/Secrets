using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class BoundedNPC : Character
{
    public override float health { get => 1; set {} }   //! Temp

    private Vector3 directionVector;
    public float speed;
    public Collider2D bound;
    public float moveTime;
    private float moveTimeSeconds;
    public float waitTime;
    private float waitTimeSeconds;
    private bool isMoving;

    private Interactable interactable;

    protected override void Awake()
    {
        base.Awake();

        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        moveTimeSeconds = Random.Range(1, moveTime);
        waitTimeSeconds = Random.Range(1, waitTime);
        ChangeDirection();
    }

    private void Update()
    {
        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if (moveTimeSeconds <= 0)
            {
                moveTimeSeconds = moveTime;
                isMoving = false;
                ChangeDirection();
            }
            if (!interactable.playerInRange)
            {
                Move();
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                isMoving = true;
                waitTimeSeconds = waitTime;
            }
        }
    }

    private void ChangeDirection()
    {
        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                //Walk right
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        animator.SetBool("isMoving", true);
        SetAnimatorXY(directionVector);
    }

    private void Move()
    {
        Vector3 temp = transform.position + directionVector * speed * Time.deltaTime;

        if (bound.bounds.Contains((Vector2)temp))
        {
            animator.SetBool("isMoving", true);
            rigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) //IF NPC hits something else or loop < 100
    {
        Vector3 temp = directionVector;

        for (int i = 100; temp == directionVector && i > 0; i--)
        {
            ChangeDirection();
        }
    }
}
