using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D arrowcollider;
    public bool isChild = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (isChild == false && myRigidbody.velocity == Vector2.zero )
        {        
            destroyArrow(null);        
        }

    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("enemy"))
            {
            destroyArrow(other.transform);
            }

    }

    public void destroyArrow(Transform other)
    {
        Destroy(this.gameObject,1f); // Destroytime in float hinzufügen
        myRigidbody.velocity = Vector2.zero;
        arrowcollider.enabled = false;
        Destroy(myRigidbody);
        transform.SetParent(other);
        isChild = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!isChild)
        {
            Debug.Log("Collision");
            Debug.Log(other.transform);
            myRigidbody.velocity = Vector2.zero;
        }
    }

}
