using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    [Header("Colliders for Different Directions")]
    public PolygonCollider2D upCollider;
    public PolygonCollider2D downCollider;
    public PolygonCollider2D leftCollider;
    public PolygonCollider2D rightCollider;

    [Header("Sprite Renderers for Different Directions")]
    public SpriteRenderer upSpriteRenderer;
    public SpriteRenderer downSpriteRenderer;
    public SpriteRenderer leftSpriteRenderer;
    public SpriteRenderer rightSpriteRenderer;

    [SerializeField] private Inventory inventory;
    [SerializeField] private AudioClip blockSound; // Reference to the block sound

    private void Awake()
    {
        // Optionally load the block sound from the Resources folder
        if (blockSound == null)
        {
            Debug.Log("No Blocksound was set");
        }

        // Ensure all sprite renderers are initially disabled
        DeactivateAllSpriteRenderers();
    }

    private void DeactivateAllColliders()
    {
        upCollider.enabled = false;
        downCollider.enabled = false;
        leftCollider.enabled = false;
        rightCollider.enabled = false;
    }

    private void DeactivateAllSpriteRenderers()
    {
        upSpriteRenderer.enabled = false;
        downSpriteRenderer.enabled = false;
        leftSpriteRenderer.enabled = false;
        rightSpriteRenderer.enabled = false;
    }

    public void block(Vector2 direction)
    {
        DeactivateAllColliders();  // Make sure all other colliders are disabled
        DeactivateAllSpriteRenderers();  // Disable all sprite renderers

        if (direction == Vector2.up)
        {
            upSpriteRenderer.sprite = inventory.currentShield.shieldSpriteUp;
            upSpriteRenderer.enabled = true;
            upCollider.enabled = true;
        }
        else if (direction == Vector2.down)
        {
            downSpriteRenderer.sprite = inventory.currentShield.shieldSpriteDown;
            downSpriteRenderer.enabled = true;
            downCollider.enabled = true;
        }
        else if (direction == Vector2.left)
        {
            leftSpriteRenderer.sprite = inventory.currentShield.shieldSpriteLeft;
            leftSpriteRenderer.enabled = true;
            leftCollider.enabled = true;
        }
        else if (direction == Vector2.right)
        {
            rightSpriteRenderer.sprite = inventory.currentShield.shieldSpriteRight;
            rightSpriteRenderer.enabled = true;
            rightCollider.enabled = true;
        }
    }

    public void stopBlock()
    {
        DeactivateAllSpriteRenderers();
        DeactivateAllColliders();
    }
}