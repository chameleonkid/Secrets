using UnityEngine;

public class BoundedNPC : Interactable
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public Collider2D bound;
    public float moveTime;
    private float moveTimeSeconds;
    public float waitTime;
    private float waitTimeSeconds;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        moveTimeSeconds = Random.Range(1, moveTime);
        waitTimeSeconds = Random.Range(1, waitTime);
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
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
            if (!playerInRange)
            {
                Move();
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                isMoving = true;
                waitTimeSeconds = waitTime;
            }
        }
    }

    void ChangeDirection()
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
        anim.SetBool("isMoving", true);
        UpdateAnimation();
    }

    void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;

        if (bound.bounds.Contains((Vector2)temp))
        {
            anim.SetBool("isMoving", true);
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
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
