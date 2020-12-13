using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
    // UI-Inventory References
    public TextMeshProUGUI itemNumberText;
    public Image itemImage;

    // Variables from the item
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public GameObject myEventSystem;
    public GameObject firstButtonInventory;
    public Inventory playerInventory;

    private void Start()
    {
        myEventSystem = GameObject.Find("EventSystem");
        firstButtonInventory = GameObject.Find("CloseButton");
    }

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;

        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem);
        }
    }

    public void UseItem()
    {
        if (thisItem)
        {
            if (thisItem.usable && thisItem.numberHeld > 0)
            {
                thisItem.Use();                         // Use Item
                itemNumberText.text = "" + thisItem.numberHeld;
                thisManager.descriptionText.text = thisItem.name + " was used";
                //thisManager.setUp(); // 05_06_2020 Testing
            }
            if (thisItem.numberHeld <= 0)
            {
                //Destroy(item); maybe, but not neccessary
                Destroy(this.gameObject);
                playerInventory.contents.Remove(thisItem);
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(firstButtonInventory);
                thisManager.setUp(); // 05_06_2020 Testing
            }
        }
    }
}
