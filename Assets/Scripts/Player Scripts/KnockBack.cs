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
            if (this.gameObject.CompareTag("Player"))
            {
                PlayerHitEnemy(hit.GetComponent<Enemy>());
            }
        }
        
        //################################## Enemy is taking Damage ###############################################################
        if (collider.gameObject.CompareTag("enemy") && collider.isTrigger && this.gameObject.CompareTag("Player")) {}
        //######################################### ARROW ##################################################################
        if (collider.gameObject.CompareTag("enemy") && collider.isTrigger && this.gameObject.CompareTag("arrow"))
        {
            enemyTransform = collider.transform;
            CalcIsCrit();
            if (isCrit == true)
            {
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                collider.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentBow.damage * 2);
                Debug.Log("CRITICAL STRIKE FOR " + playerInventory.currentBow.damage * 2);
                DamagePopup(playerInventory.currentBow.damage * 2);
            }
            else
            {
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                collider.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentBow.damage);
                Debug.Log("NORMAL STRIKE FOR " + playerInventory.currentBow.damage);
                DamagePopup(playerInventory.currentBow.damage);
            }
        }
        //######################################### Spell ##################################################################
        if (collider.gameObject.CompareTag("enemy") && collider.isTrigger && this.gameObject.CompareTag("spell"))
        {
            enemyTransform = collider.transform;
            CalcIsCrit();
            if (isCrit == true)
            {
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                collider.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentSpellbook.SpellDamage * 2);
                Debug.Log("CRITICAL STRIKE FOR " + playerInventory.currentSpellbook.SpellDamage * 2);
                DamagePopup(playerInventory.currentSpellbook.SpellDamage * 2);
            }
            else
            {
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                collider.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentSpellbook.SpellDamage);
                Debug.Log("NORMAL STRIKE FOR " + playerInventory.currentSpellbook.SpellDamage);
                DamagePopup(playerInventory.currentSpellbook.SpellDamage);
            }
            if (playerInventory.currentSpellbook.itemName == "Spellbook of Fire")
            {
                dotTime = 1;
                dotDamage = 1;
                dotTicks = 3;

                StartCoroutine(DamageOverTime(collider.GetComponent<Enemy>()));
            }
        }

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

    private void PlayerHitEnemy(Enemy enemy)
    {
        enemyTransform = enemy.transform;
        var damage = playerInventory.currentWeapon.damage;
        if (IsCriticalHit())
        {
            damage *= 2;
            Debug.Log("CRITICAL STRIKE FOR " + damage);
        }
        else
        { 
            Debug.Log("NORMAL STRIKE FOR " + damage);
        }
        enemy.health -= damage;
        DamagePopup(damage);
        enemy.currentState = EnemyState.stagger;
        enemy.Knock(knockTime);
    }

    private bool IsCriticalHit() => (playerInventory.totalCritChance > 0 && Random.Range(0, 99) <= playerInventory.totalCritChance);

    public void DamagePopup(float damage)
    {
        if (normalDmgPopup == null) return;

        if (isCrit)
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
