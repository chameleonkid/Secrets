using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour
{
    public float damage;
    public PlayerInventory playerInventory;

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
        DamagePopUpManager.RequestDamagePopUp(damage, isCritical, enemy.transform);
        enemy.Knock(knockTime);
    }
}
