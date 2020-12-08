using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D arrowcollider;
    public bool isChild = false;

    void Update()
    {
        if (isChild == false && myRigidbody.velocity == Vector2.zero)
        {
            destroyArrow(null);
        }
    }

    public void Setup(Vector2 direction, Vector3 rotation)
    {
        myRigidbody.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.Euler(rotation);
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
        Destroy(this.gameObject, 1f); // Destroytime in float hinzufügen
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
            Debug.Log("Collided with: " + other.transform.name);
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
