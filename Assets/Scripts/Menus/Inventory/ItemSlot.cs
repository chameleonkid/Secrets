using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public event Action<InventoryItem> OnSlotSelected;
    public event Action<InventoryItem> OnSlotUsed;

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
}
