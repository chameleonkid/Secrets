using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    public PlayerInventory playerInventory;
    private bool isCrit;
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
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                //################################## Enemy is taking Damage ###############################################################
                if (other.gameObject.CompareTag("enemy") && other.isTrigger && this.gameObject.CompareTag("Player"))
                {
                    enemyTransform = other.transform;
                    CalcIsCrit();
                    if (isCrit == true)
                    {
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentWeapon.damage * 2);
                        Debug.Log("CRITICAL STRIKE FOR " + playerInventory.currentWeapon.damage * 2);
                        DamagePopup(playerInventory.currentWeapon.damage * 2);
                    }
                    else
                    {
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentWeapon.damage);
                        Debug.Log("NORMAL STRIKE FOR " + playerInventory.currentWeapon.damage);
                        DamagePopup(playerInventory.currentWeapon.damage);
                    }
                }
                //######################################### ARROW ##################################################################
                if (other.gameObject.CompareTag("enemy") && other.isTrigger && this.gameObject.CompareTag("arrow"))
                {
                    enemyTransform = other.transform;
                    CalcIsCrit();
                    if (isCrit == true)
                    {
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentBow.damage * 2);
                        Debug.Log("CRITICAL STRIKE FOR " + playerInventory.currentBow.damage * 2);
                        DamagePopup(playerInventory.currentBow.damage * 2);
                    }
                    else
                    {
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentBow.damage);
                        Debug.Log("NORMAL STRIKE FOR " + playerInventory.currentBow.damage);
                        DamagePopup(playerInventory.currentBow.damage);
                    }
                }
                //######################################### Spell ##################################################################
                if (other.gameObject.CompareTag("enemy") && other.isTrigger && this.gameObject.CompareTag("spell"))
                {
                    enemyTransform = other.transform;
                    CalcIsCrit();
                    if (isCrit == true)
                    {
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentSpellbook.SpellDamage * 2);
                        Debug.Log("CRITICAL STRIKE FOR " + playerInventory.currentSpellbook.SpellDamage * 2);
                        DamagePopup(playerInventory.currentSpellbook.SpellDamage * 2);
                    }
                    else
                    {
                        hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime, playerInventory.currentSpellbook.SpellDamage);
                        Debug.Log("NORMAL STRIKE FOR " + playerInventory.currentSpellbook.SpellDamage);
                        DamagePopup(playerInventory.currentSpellbook.SpellDamage);
                    }
                    if (playerInventory.currentSpellbook.itemName == "Spellbook of Fire")
                    {
                        dotTime = 1;
                        dotDamage = 1;
                        dotTicks = 3;

                        StartCoroutine(DamageOverTime(other.GetComponent<Enemy>()));
                    }
                }

                //################################## Player is taking Damage ###############################################################
                if (other.gameObject.CompareTag("Player") && other.isTrigger)
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        playerInventory.calcDefense();
                        if (damage - playerInventory.totalDefense > 0)                                                          //if more Dmg than armorvalue was done
                        {
                            other.GetComponent<PlayerMovement>().Knock(knockTime, damage - playerInventory.totalDefense);
                            Debug.Log(damage - playerInventory.totalDefense + " taken with armor!");

                        }
                        else                                                                                                    //if more amor than dmg please dont heal me with negative-dmg :)
                        {
                            other.GetComponent<PlayerMovement>().Knock(knockTime, 0);
                            Debug.Log(damage - playerInventory.totalDefense + " not enaugh DMG to pierce the armor");
                        }
                    }
                }
            }
        }
    }

    public void CalcIsCrit()
    {
        isCrit = false;
        if (playerInventory.totalCritChance > 0)
        {
            int temp = Random.Range(0, 99);
            if (temp <= playerInventory.totalCritChance)
            {
                isCrit = true;
            }
        }
    }

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
                enemy.TakeDamage(dotDamage);
                DamagePopup(dotDamage);
            }
            else
            {
                Debug.Log(enemy + "should be dead!");
            }
        }
    }
}
