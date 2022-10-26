using UnityEngine;

public class ItemDoor : Interactable
{
    [Header("Door variables")]
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public BoolValue storeOpen;
    public Item itemToOpen;
    [SerializeField] private Dialogue dialogue = default;

    //########################## Doormemory ##################################
    private void Start()
    {
        open = storeOpen.RuntimeValue;
        if (open)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
    //########################## Doormemory END ##################################

    private void LateUpdate()
    {
        if (playerInRange && player.inputInteract && Time.timeScale > 0)
        {
            if (playerInventory.Contains(itemToOpen))
            {
                dialogue.lines[0].text = "You hold up " + itemToOpen.name + " and the door begins to open!";
                TriggerDialogue();
                Open();
            }
            else
            {
                dialogue.lines[0].text = "It seems like you need " + itemToOpen.name + " to open this door.";
                TriggerDialogue();
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
        storeOpen.RuntimeValue = open;
    }

    public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;
        storeOpen.RuntimeValue = false;
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);
}
