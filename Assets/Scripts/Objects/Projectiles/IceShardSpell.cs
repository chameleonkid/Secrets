using UnityEngine;
using System.Collections;

public class IceShardSpell : PlayerProjectile
{
    public float slowTime;
    public Animator anim;

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
            OnHitReceiver(other.transform);
        }
    }

    private IEnumerator SlowEnemyForSeconds(EnemyLog enemy)
    {
        enemy.moveSpeed /= 2;
        enemy.GetComponent<ChangeColor>()?.ChangeSpriteColor(Color.blue, slowTime);
        yield return new WaitForSeconds(slowTime);
        enemy.moveSpeed *= 2;
    }
}
