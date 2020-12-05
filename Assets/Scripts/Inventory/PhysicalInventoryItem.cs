using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public InventoryItem thisItem;
 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("powerup") && other.isTrigger)
        {
            addItemToInventory();
            Destroy(this.gameObject);
        }
    }


    void addItemToInventory()
    {
        if(playerInventory && thisItem)
        {
            if(playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld++;
            }
        }
    }

}
