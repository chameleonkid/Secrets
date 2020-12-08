using UnityEngine;

public class SpellIceShard : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D spellCollider;
    public bool isChild = false;

    void Update()
    {
        if (isChild == false && myRigidbody.velocity == Vector2.zero)
        {
            destroySpellIceshard(null);
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
            destroySpellIceshard(other.transform);
         /*   if(other.GetComponent<EnemyLog> )   Hey Schwer, could u check this? I Think the intention clear :) Slow enemys on hit
            {
                other.GetComponent<EnemyLog>.speed = other.GetComponent<EnemyLog>.speed / 2;
            }
        */
        }
    }

    public void destroySpellIceshard(Transform other)
    {
        Destroy(this.gameObject, 1f); // Destroytime in float hinzufügen
        myRigidbody.velocity = Vector2.zero;
        spellCollider.enabled = false;
        Destroy(myRigidbody);
        transform.SetParent(other);
        isChild = true;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!isChild)
        {
            Debug.Log("Iceshard collided with: " + other.transform.name);
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
