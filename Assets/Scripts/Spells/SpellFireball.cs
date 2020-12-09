using UnityEngine;
using System.Collections;


public class SpellFireball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D spellCollider;
    public bool isChild = false;
    public float dotTime;
    public float tickCount;
    public float dotDamage;

    void Update()
    {
        if (isChild == false && myRigidbody.velocity == Vector2.zero)
        {
            destroySpellFireball(null);
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
            var enemy = other.GetComponent<EnemyLog>();
            if (enemy != null)
            {
                StartCoroutine(DamageOverTime(enemy));
            }
            destroySpellFireball(other.transform);
        }
    }

    public void destroySpellFireball(Transform other)
    {
        Destroy(this.gameObject, dotTime * tickCount + 0.25f); // Destroytime in float hinzufügen
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
            Debug.Log("Fireball collided with: " + other.transform.name);
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator DamageOverTime(EnemyLog enemy)
    {
        for(int i = 0; i <= tickCount; i++)
        {
        yield return new WaitForSeconds(dotTime);
        enemy.health -= dotDamage;
        }
    }

}
