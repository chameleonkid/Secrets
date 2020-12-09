using UnityEngine;
using System.Collections;

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


 
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!isChild)
        {
            Debug.Log("Iceshard collided with: " + other.transform.name);
            myRigidbody.velocity = Vector2.zero;
        }
    }

    // Destroy initial IceShard
    public void destroySpellIceshard(Transform other)
    {
        Destroy(this.gameObject, 0.5f); // Destroytime in float hinzufügen
        myRigidbody.velocity = Vector2.zero;
        spellCollider.enabled = false;
        Destroy(myRigidbody);      
        transform.SetParent(other);
        isChild = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            var enemy = other.GetComponent<EnemyLog>();
            if (enemy != null)
            {
                StartCoroutine(SlowEnemyForSeconds(enemy, other.transform));
            }
        
    }


    private IEnumerator SlowEnemyForSeconds(EnemyLog enemy, Transform other)
    {
        enemy.moveSpeed /= 2;
        yield return new WaitForSeconds(1.5f);
        enemy.moveSpeed *= 2;
        destroySpellIceshard(other.transform);
    }


}





