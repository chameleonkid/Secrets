using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    [Header("Colliders for Different Directions")]
    public PolygonCollider2D upCollider;
    public PolygonCollider2D downCollider;
    public PolygonCollider2D leftCollider;
    public PolygonCollider2D rightCollider;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Inventory inventory;
    [SerializeField] private AudioClip blockSound; // Reference to the block sound

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Optionally load the block sound from the Resources folder
        if (blockSound == null)
        {
            blockSound = Resources.Load<AudioClip>("Sounds/BlockSound");
        }
    }

    private void DeactivateAllColliders()
    {
        upCollider.enabled = false;
        downCollider.enabled = false;
        leftCollider.enabled = false;
        rightCollider.enabled = false;
    }

    public void block(Vector2 direction)
    {
        DeactivateAllColliders(); // Make sure all other colliders are disabled

        if (direction == Vector2.up)
        {
            spriteRenderer.sprite = inventory.currentShield.shieldSpriteUp;
            upCollider.enabled = true;
        }
        else if (direction == Vector2.down)
        {
            spriteRenderer.sprite = inventory.currentShield.shieldSpriteDown;
            downCollider.enabled = true;
        }
        else if (direction == Vector2.left)
        {
            spriteRenderer.sprite = inventory.currentShield.shieldSpriteLeft;
            leftCollider.enabled = true;
        }
        else if (direction == Vector2.right)
        {
            spriteRenderer.sprite = inventory.currentShield.shieldSpriteRight;
            rightCollider.enabled = true;
        }
        spriteRenderer.enabled = true;
    }

    public void stopBlock()
    {
        spriteRenderer.enabled = false;
        DeactivateAllColliders();
    }
}

