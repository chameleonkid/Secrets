using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public event Action<InventoryItem> OnSlotSelected;
    public event Action<InventoryItem> OnSlotUsed;

    [SerializeField] private Button button = default;
    [SerializeField] private TextMeshProUGUI numberHeldDisplay = default;
    [SerializeField] private Image itemImage = default;
    private Sprite defaultSprite = default;

    private InventoryItem item;

    private void OnValidate() => defaultSprite = itemImage.sprite;

    public void SetItem(InventoryItem newItem)
    {
        item = newItem;

        if (item != null)
        {
            itemImage.sprite = item.itemImage;

            if (numberHeldDisplay != null)
            {
                numberHeldDisplay.text = item.numberHeld.ToString();
            }
        }
        else
        {
            itemImage.sprite = defaultSprite;

            if (numberHeldDisplay != null)
            {
                numberHeldDisplay.text = "";
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (item)
        {
            OnSlotSelected?.Invoke(item);
        }
    }

    public void UseItem() {
        OnSlotUsed?.Invoke(item);   // Let relevant manager handle use behaviour
        SetItem(item);              // Refresh number held text
    }

    public void SwapItemToPlayer()
    {
        // if (item)
        // {
        //     if (item.unique && playerInventory.coins >= item.itemBuyPrice)
        //     {
        //         playerInventory.Add(item);
        //         playerInventory.coins -= item.itemBuyPrice;
        //         vendorInventory.RemoveItem(item);
        //         thisVendorManager.clearInventorySlots();
        //         thisVendorManager.MakeInventorySlots();
        //     }
        //     else if(playerInventory.coins >= item.itemBuyPrice)
        //     {
        //         playerInventory.coins -= item.itemBuyPrice;
        //         playerInventory.Add(item);
        //     }
        //     else
        //     {
        //         Debug.Log("Not enaugh money! to buy " + item);
        //     }
        // }
    }

    
    public void SwapItemToVendor()
    {
        if (item)
        {
            if (item.unique)
            {
                // vendorInventory.Add(item);
                // playerInventory.RemoveItem(item);
                // thisVendorManager.clearInventorySlots();
                // thisVendorManager.MakeInventorySlots();
            }
        }
    }
}
