using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public BoolValue storeOpen; //###### Door Memory
    public Item key;

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

    private void Update()
    {
        if (playerInRange && player.inputInteract)
        {
            if (thisDoorType == DoorType.key && playerInventory.items[key] >= 1)
            {
                playerInventory.items[key]--;
                Open();
            }
        }
    }

    public void Open()
    {
        //Turn off the Door sprite renderer
        doorSprite.enabled = false;
        // set open to true
        open = true;
        //turn off the doors box-collider
        physicsCollider.enabled = false;
        storeOpen.RuntimeValue = open; //Changed from Runtime to INITIAL!
    }

    public void Close()
    {
        //Turn off the Door sprite renderer
        doorSprite.enabled = true;
        // set open to true
        open = false;
        //turn off the doors box-collider
        physicsCollider.enabled = true;
        storeOpen.RuntimeValue = false;
    }
}
