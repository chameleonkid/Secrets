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
            if (hit != null && other.isTrigger)
            {
                OnHit(hit);
            }
        }
    }

    private void OnHit(Rigidbody2D hit)
    {
        var knockback = hit.transform.position - transform.position;
        knockback = knockback.normalized * thrust;
        hit.AddForce(knockback, ForceMode2D.Impulse);

        if (hit.gameObject.CompareTag("enemy"))
        {
            OnHitEnemy(hit.GetComponent<Enemy>());
        }
        else if (hit.gameObject.CompareTag("Player"))
        {
            OnHitPlayer(hit.GetComponent<PlayerMovement>());
        }
    }

    private void OnHitEnemy(Enemy enemy)
    {
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
            if (playerInventory.currentSpellbook.itemName == "Spellbook of Fire" && enemy.gameObject.activeInHierarchy)
            {
                StartCoroutine(DamageOverTime(enemy, 3, 1, 1));
            }
        }
    }

    private void OnHitPlayer(PlayerMovement player)
    {
        if (player.currentState != PlayerState.stagger)
        {
            player.currentState = PlayerState.stagger;
            playerInventory.calcDefense();
            if (damage - playerInventory.totalDefense > 0)                                  // if more Dmg than armorvalue was done
            {
                player.Knock(knockTime, damage - playerInventory.totalDefense);
                Debug.Log(damage - playerInventory.totalDefense + " taken with armor!");

            }
            else                                                                            // if more amor than dmg please dont heal me with negative-dmg :)
            {
                player.Knock(knockTime, 0);
                Debug.Log(damage - playerInventory.totalDefense + " not enaugh DMG to pierce the armor");
            }
        }
    }

    private bool IsCriticalHit() => (playerInventory.totalCritChance > 0 && Random.Range(0, 99) <= playerInventory.totalCritChance);

    private void PlayerHitEnemy(Enemy enemy, float damage)
    {
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
        DamagePopup(damage, isCritical, enemy.transform);
        enemy.Knock(knockTime);
    }

    public void DamagePopup(float damage, Transform parent) => DamagePopup(damage, false, parent);
    public void DamagePopup(float damage, bool isCritical, Transform parent)
    {
        if (normalDmgPopup == null || critDmgPopup == null) return;

        var popup = isCritical ? critDmgPopup : normalDmgPopup;
        var instance = Instantiate(popup, transform.position, Quaternion.identity, parent);
        instance.SetText(damage);
    }

    private IEnumerator DamageOverTime(Enemy enemy, float ticks, float tickDuration, float tickDamage)
    {
        for (int i = 0; i <= ticks; i++)
        {
            if (enemy.health > 0)
            {
                yield return new WaitForSeconds(tickDuration);
                enemy.health -= tickDamage;
                DamagePopup(tickDamage, enemy.transform);   //! TEMP

            }
            else
            {
                Debug.Log(enemy + "should be dead!");
            }
        }
    }
}
