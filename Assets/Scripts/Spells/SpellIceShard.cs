using UnityEngine;
using System.Collections;

public class SpellIceShard : ProjectileTemp
{
    public float slowTime;
    public Animator anim;

    protected override float destroyTime => slowTime + 0.25f;

    protected override void Awake()
    {
        anim = transform.GetComponent<Animator>();
        anim.SetBool("isFlying", true);
        base.Awake();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            var enemy = other.GetComponent<EnemyLog>();
            if (enemy != null)
            {
                anim.SetBool("isFlying", false);
                StartCoroutine(SlowEnemyForSeconds(enemy));
            }
            DestroyProjectile(other.transform);
        }
    }

    private IEnumerator SlowEnemyForSeconds(EnemyLog enemy)
    {
        enemy.moveSpeed /= 2;
        yield return new WaitForSeconds(slowTime);
        enemy.moveSpeed *= 2;
    }
}
