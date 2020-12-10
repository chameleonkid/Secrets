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
            var enemyColor = other.GetComponent<ChangeColor>();
            if (enemy != null)
            {
                anim.SetBool("isFlying", false);
                StartCoroutine(SlowEnemyForSeconds(enemy,enemyColor));
            }
            OnHitReceiver(other.transform);
        }
    }

    private IEnumerator SlowEnemyForSeconds(EnemyLog enemy, ChangeColor enemyColor)
    {

        enemy.moveSpeed /= 2;
        enemyColor.changeColorToBlue(slowTime);
        yield return new WaitForSeconds(slowTime);
        enemy.moveSpeed *= 2;
        
        
    }
}
