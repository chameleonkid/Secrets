using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook/DashSpell")]
public class DashSpell : InventorySpellbook
{
    private Collider2D characterCollider;
    [SerializeField] private float dashForce;

    public void Dash(Character character)
    {
        characterCollider = character.GetComponent<Collider2D>();
        character.GetComponent<Animator>().SetTrigger("isDashing");
        Vector2 facingDirection = character.GetAnimatorXY().normalized; // Normalize the direction vector
        Vector2 dashTarget = (Vector2)character.transform.position + facingDirection * dashForce; // Scale by dash distance
        // Debug.Log("Player is at: " + character.transform.position);
        // Debug.Log("DashTarget it at: " + dashTarget);
        if (character.canDash)
        {
            // Debug.Log("Dashing to " + dashTarget);
            character.Dash(character, facingDirection ,dashForce);
            // character.TeleportTowards(dashTarget, character.maxDashDistance * character.speedModifier);
        }
    }
}
