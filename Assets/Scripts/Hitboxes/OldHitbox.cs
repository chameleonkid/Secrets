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

        if (other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            OnHitPlayer(other.GetComponent<PlayerMovement>());
        }
    }

    private void OnHitPlayer(PlayerMovement player)
    {
        Debug.Log("Pre-stagger check");
        if (player.currentState != Character.State.stagger) //! Unreliable! Cannot determine execution order between this and knockback (where `FlashCo` disables the player's trigger collider). Consider adding a private float timer to Player/Character to properly implement invincibility frames.
        {
            Debug.Log("Post-stagger check");
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
}
