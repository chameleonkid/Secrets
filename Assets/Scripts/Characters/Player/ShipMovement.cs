using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Animator myAnim;
    [SerializeField] private float _speed = default;
    [SerializeField] private float runSpeedModifier = 1.5f;
    [SerializeField] private BoxCollider2D shipCollider = default;
    [SerializeField] private BoxCollider2D shipTriggerCollider = default;

    private PlayerInput input;
    private PlayerInput uiInput;



     // Start is called before the first frame update
     void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.direction.x = Input.GetAxisRaw("Horizontal");
        input.direction.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (input.direction != Vector2.zero)
        {
            MoveShip();
            myAnim.SetFloat("MoveX", input.direction.x);
            myAnim.SetFloat("MoveY", input.direction.y);
        }
        else
        {
            myAnim.SetBool("isMoving", false);
        }


    }

    public void MoveShip()
    {
        myRigidbody.MovePosition(new Vector2(transform.position.x,transform.position.y) + input.direction.normalized * _speed * Time.deltaTime);
        myAnim.SetBool("isMoving", true);
    }
}
