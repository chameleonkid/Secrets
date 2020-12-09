using UnityEngine;
using System.Collections;

public class SpellIceShard : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D spellCollider;
    public bool isChild = false;
    public float slowTime;
    public Animator anim;
    
  

    void Update()
    {
        if (isChild == false && myRigidbody.velocity == Vector2.zero)
        {
            destroySpellIceShard(null);
        }
    }

    public void Setup(Vector2 direction, Vector3 rotation)
    {
        anim = transform.GetComponent<Animator>();
        anim.SetBool("isFlying", true);
        myRigidbody.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("enemy"))
        {
            var enemy = other.GetComponent<EnemyLog>();
            if (enemy != null)
            {
                anim.SetBool("isFlying", false);
                StartCoroutine(SlowEnemyForSeconds(enemy));
            }
            destroySpellIceShard(other.transform);
        }

    }

    public void destroySpellIceShard(Transform other)
    {
        Destroy(this.gameObject, slowTime + 0.25f); // Destroytime in float hinzufügen
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
            Debug.Log("IceSharr collided with: " + other.transform.name);
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator SlowEnemyForSeconds(EnemyLog enemy)
    {
        enemy.moveSpeed /= 2;
        yield return new WaitForSeconds(slowTime);
        enemy.moveSpeed *= 2;
       
    }

}






