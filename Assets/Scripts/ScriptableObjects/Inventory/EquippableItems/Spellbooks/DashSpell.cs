using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook/DashSpell")]
public class DashSpell : InventorySpellbook
{
    private Collider2D characterCollider;

    public void Dash(Character character)
    {
        characterCollider = character.GetComponent<Collider2D>();
        Vector2 facingDirection = character.GetAnimatorXY();
        Vector2 temp;
        temp.x = facingDirection.x * character.maxDashDistance;
        temp.y = facingDirection.y * character.maxDashDistance;
        Vector2 dashTarget = (Vector2)character.transform.position + temp;
        // Debug.Log("Player is at: " + character.transform.position);
        // Debug.Log("DashTarget it at: " + dashTarget);
        if (character.canDash)
        {
            // Debug.Log("Dashing to " + dashTarget);
            character.Dash(character, temp);
            // character.TeleportTowards(dashTarget, character.maxDashDistance * character.speedModifier);
        }
    }
}
