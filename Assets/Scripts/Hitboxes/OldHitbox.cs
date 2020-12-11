using UnityEngine;

public class OldHitbox : MonoBehaviour
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
            if (other.isTrigger)
            {
                OnHit(other);
            }
        }
    }

    private void OnHit(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            OnHitEnemy(other.GetComponent<Enemy>());
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            OnHitPlayer(other.GetComponent<PlayerMovement>());
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
        if (player.currentState != Character.State.stagger) //! Unreliable! Cannot determine execution order between this and knockback (where stagger is applied). Consider adding a private float timer to Player/Character to properly implement invincibility frames.
        {
            playerInventory.calcDefense();
            if (damage - playerInventory.totalDefense > 0)              // if more Dmg than armorvalue was done
            {
                player.health -= damage - playerInventory.totalDefense;
                Debug.Log(damage - playerInventory.totalDefense + " taken with armor!");
            }
            else                                                        // if more amor than dmg please dont heal me with negative-dmg :)
            {
                Debug.Log(damage - playerInventory.totalDefense + " not enough DMG to pierce the armor!");
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
    }
}
