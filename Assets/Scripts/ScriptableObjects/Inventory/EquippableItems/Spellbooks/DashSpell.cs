using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook/DashSpell")]
public class DashSpell : InventorySpellbook
{
    [SerializeField] private float dashForce = 5f; // The force or speed of the dash
    [SerializeField] private float dashDistance = 3f; // Maximum distance for the dash
    [SerializeField] private float dashDuration = 0.2f; // Duration of the dash
    [SerializeField] private LayerMask obstacleLayer; // LayerMask for obstacles
    [SerializeField] private GameObject player;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    public void Dash(Character character)
    {
        character.GetComponent<Animator>().SetTrigger("isDashing");

        Vector2 facingDirection = character.GetAnimatorXY().normalized;
        Vector2 dashTarget = (Vector2)character.transform.position + facingDirection * dashDistance;

        if (character.canDash)
        {
            // Raycast to check for obstacles in the dash direction
            RaycastHit2D hit = Physics2D.Raycast(character.transform.position, facingDirection, dashDistance, obstacleLayer);

            if (hit.collider != null)
            {
                // Adjust the dash distance if an obstacle is detected
                dashTarget = hit.point;
            }

            // Enable trails for all child objects with SpriteRenderer
            EnableAllChildTrails(character);

            // Start the dash movement
            character.StartCoroutine(DashMovement(character, dashTarget));
        }
    }

    private void EnableAllChildTrails(Character character)
    {
        // Find all SpriteTrail components in the character and its children
        var trails = character.GetComponentsInChildren<SpriteTrail.SpriteTrail>();

        // Enable each trail
        foreach (var trail in trails)
        {
            trail.EnableTrail();
        }
    }

    private IEnumerator DashMovement(Character character, Vector2 dashTarget)
    {
        Vector2 startPosition = character.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            // Calculate the percentage of time passed relative to dash duration
            float t = elapsedTime / dashDuration;

            // Smoothly move the character toward the target
            Vector2 newPosition = Vector2.Lerp(startPosition, dashTarget, t);

            // Check for obstacles during the dash
            RaycastHit2D hit = Physics2D.Raycast(character.transform.position, newPosition - (Vector2)character.transform.position, Vector2.Distance(character.transform.position, newPosition), obstacleLayer);
            if (hit.collider != null)
            {
                // Stop the dash if an obstacle is detected
                character.transform.position = hit.point;
                break;
            }

            // Move the character to the new position
            character.transform.position = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the character reaches the dash target at the end
        character.transform.position = dashTarget;

        // Disable all trails once the dash is complete
        DisableAllChildTrails(character);
    }

    private void DisableAllChildTrails(Character character)
    {
        // Find all SpriteTrail components in the character and its children
        var trails = character.GetComponentsInChildren<SpriteTrail.SpriteTrail>();

        // Disable each trail
        foreach (var trail in trails)
        {
            trail.DisableTrail();
        }
    }
}