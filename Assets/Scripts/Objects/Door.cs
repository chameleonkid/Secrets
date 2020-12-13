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
    public InventoryItem key;

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
        if (Input.GetButtonDown("Interact"))
        {
            //Debug.Log("E was pressed");
            if (playerInRange && thisDoorType == DoorType.key)
            {
                //    Debug.Log("Player is in Range and Door = Key");
                if (playerInventory.contents.Contains(key)) // Keys left in Inventory?
                {
                    key.numberHeld--;
                    Open();
                }
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
