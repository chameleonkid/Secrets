using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public event Action<Item> OnSlotSelected;
    public event Action<Item> OnSlotUsed;

    [SerializeField] private TextMeshProUGUI numberHeldDisplay = default;
    [SerializeField] private Image itemImage = default;
    [SerializeField] private Sprite defaultSprite = default;

    private Item item;

   // private void OnValidate() => defaultSprite = itemImage.sprite;

    public void SetItem(Item item, int count)
    {
        this.item = item;

        if (item != null)
        {
            itemImage.sprite = item.sprite;

            if (numberHeldDisplay != null)
            {
                numberHeldDisplay.text = count.ToString();
            }
        }
        else
        {
         //   itemImage.sprite = defaultSprite;

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

    public void UseItem() => OnSlotUsed?.Invoke(item);  // Let relevant manager handle use behaviour
}
