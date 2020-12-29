using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ItemDisplay : MonoBehaviour
{
    [SerializeField] protected GameObject itemSlotPrefab = default;
    [SerializeField] protected GameObject itemSlotParent = default;

    protected abstract Inventory inventory { get; set; }
    protected List<ItemSlot> slots = new List<ItemSlot>();

    protected void UpdateItemSlotContents()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            var item = inventory.items.ElementAt(i);
            slots[i].SetItem(item.Key, item.Value);
        }
    }

    protected void UpdateItemSlots()
    {
        if (inventory != null)
        {
            if (slots.Count < inventory.items.Count)
            {
                InstantiateSlots();
            }
            else if (slots.Count > inventory.items.Count)
            {
                DestroySlots();
            }

            UpdateItemSlotContents();
        }
    }

    protected virtual void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.items.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<ItemSlot>();
            slots.Add(newSlot);
        }
    }

    private void DestroySlots()
    {
        for (int i = slots.Count - 1; i > inventory.items.Count - 1; i--)
        {
            var slot = slots[i];
            slots.Remove(slot);
            Destroy(slot.gameObject);
        }
    }
}
