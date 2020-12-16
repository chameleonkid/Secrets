using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
    public event Action<InventoryItem> OnSlotSelected;
    public event Action<InventoryItem> OnSlotUsed;

    private Sprite defaultSprite = default;
    private Image itemImage;
    private TextMeshProUGUI itemNumberText;

    // Variables from the item
    public InventoryItem item;

    private void Awake()
    {
        itemImage = this.GetComponentInChildrenFirst<Image>();
        defaultSprite = itemImage.sprite;
        itemNumberText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetItem(InventoryItem newItem)
    {
        item = newItem;

        if (item != null)
        {
            itemImage.sprite = item.itemImage;

            if (itemNumberText != null)
            {
                itemNumberText.text = item.numberHeld.ToString();
            }
        }
        else
        {
            itemImage.sprite = defaultSprite;

            if (itemNumberText != null)
            {
                itemNumberText.text = "";
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

    public void UseItem()
    {
        if (item)
        {
            if (item.usable && item.numberHeld > 0)
            {
                item.Use();

                if (itemNumberText != null)
                {
                    itemNumberText.text = item.numberHeld.ToString();
                }

                OnSlotUsed?.Invoke(item);
            }

            if (item.numberHeld <= 0)
            {
                Destroy(this.gameObject);
            }
        }
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
