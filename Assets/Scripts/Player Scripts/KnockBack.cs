using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour
{
    public float thrust;    // Should probably rename to `force`
    public float knockTime;
    public float damage;
    public PlayerInventory playerInventory;
    [SerializeField] private DmgPopUpTextManager normalDmgPopup = default;
    [SerializeField] private DmgPopUpTextManager critDmgPopup = default;
    private Transform enemyTransform;

    public float dotTime = 0;
    public float dotTicks = 0;
    public float dotDamage = 0;

    // Knockback + dmg
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Breakable>().Smash();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            var hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                OnHit(hit, other);
            }
        }
    }

    private void OnHit(Rigidbody2D hit, Collider2D collider)
    {
        var knockback = hit.transform.position - transform.position;
        knockback = knockback.normalized * thrust;
        hit.AddForce(knockback, ForceMode2D.Impulse);

        if (hit.gameObject.CompareTag("enemy") && collider.isTrigger)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (this.gameObject.CompareTag("Player"))
            {
                PlayerHitEnemy(enemy, playerInventory.currentWeapon.damage);
            }
            else if (this.gameObject.CompareTag("arrow"))
            {
                PlayerHitEnemy(enemy, playerInventory.currentBow.damage);
            }
            else if (this.gameObject.CompareTag("spell"))
            {
                PlayerHitEnemy(enemy, playerInventory.currentSpellbook.SpellDamage);
                if (playerInventory.currentSpellbook.itemName == "Spellbook of Fire")
                {
                    dotTime = 1;
                    dotDamage = 1;
                    dotTicks = 3;

                    StartCoroutine(DamageOverTime(enemy));
                }
            }
        }
        
        //################################## Enemy is taking Damage ###############################################################
        if (collider.gameObject.CompareTag("enemy") && collider.isTrigger && this.gameObject.CompareTag("Player")) {}
        //######################################### ARROW ##################################################################
        if (collider.gameObject.CompareTag("enemy") && collider.isTrigger && this.gameObject.CompareTag("arrow")) {}
        //######################################### Spell ##################################################################
        if (collider.gameObject.CompareTag("enemy") && collider.isTrigger && this.gameObject.CompareTag("spell")) {}

        //################################## Player is taking Damage ###############################################################
        if (collider.gameObject.CompareTag("Player") && collider.isTrigger)
        {
            if (collider.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
            {
                hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                playerInventory.calcDefense();
                if (damage - playerInventory.totalDefense > 0)                                                          //if more Dmg than armorvalue was done
                {
                    collider.GetComponent<PlayerMovement>().Knock(knockTime, damage - playerInventory.totalDefense);
                    Debug.Log(damage - playerInventory.totalDefense + " taken with armor!");

                }
                else                                                                                                    //if more amor than dmg please dont heal me with negative-dmg :)
                {
                    collider.GetComponent<PlayerMovement>().Knock(knockTime, 0);
                    Debug.Log(damage - playerInventory.totalDefense + " not enaugh DMG to pierce the armor");
                }
            }
        }
    }

    private bool IsCriticalHit() => (playerInventory.totalCritChance > 0 && Random.Range(0, 99) <= playerInventory.totalCritChance);

    private void PlayerHitEnemy(Enemy enemy, float damage)
    {
        enemyTransform = enemy.transform;
        var isCritical = IsCriticalHit();
        if (isCritical)
        {
            damage *= 2;
            Debug.Log("CRITICAL STRIKE FOR " + damage);
        }
        else
        {
            Debug.Log("NORMAL STRIKE FOR " + damage);
        }
        enemy.health -= damage;
        DamagePopup(damage, isCritical);
        enemy.currentState = EnemyState.stagger;
        enemy.Knock(knockTime);
    }

    public void DamagePopup(float damage) => DamagePopup(damage, false);
    public void DamagePopup(float damage, bool isCritical)
    {
        if (normalDmgPopup == null) return;

        if (isCritical)
        {
            DmgPopUpTextManager tempNumber = Instantiate(critDmgPopup, transform.position, Quaternion.identity, enemyTransform);
            tempNumber.SetText(damage);
        }
        else
        {
            DmgPopUpTextManager tempNumber = Instantiate(normalDmgPopup, transform.position, Quaternion.identity, enemyTransform);
            tempNumber.SetText(damage);
        }
    }

    private IEnumerator DamageOverTime(Enemy enemy)
    {
        for (int i = 0; i <= dotTicks; i++)
        {
            if (enemy.health > 0)
            {
                yield return new WaitForSeconds(dotTime);
                enemy.health -= dotDamage;
                DamagePopup(dotDamage);
            }
            else
            {
                Debug.Log(enemy + "should be dead!");
            }
        }
    }
}
