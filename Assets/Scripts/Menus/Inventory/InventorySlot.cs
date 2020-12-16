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
        itemImage = GetChildImage();
        defaultSprite = itemImage.sprite;
        itemNumberText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private Image GetChildImage()
    {
        var attachedImage = GetComponent<Image>();
        var childImages = GetComponentsInChildren<Image>();
        for (int i = 0; i < childImages.Length; i++)
        {
            if (childImages[i] != attachedImage)
            {
                return childImages[i];
            }
        }
        return attachedImage;
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

    // public void SwapVendorItem()
    // {
    //     if (item)
    //     {
    //         if (item && item.numberHeld > 0)
    //         {
    //             playerInventory.Add(item);
    //             vendorInventory.RemoveItem(item);
    //             thisVendorManager.MakeInventorySlots();
    //         }
    //     }
    // }
}
