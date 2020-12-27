using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDisplay : MonoBehaviour
{
    [SerializeField] protected GameObject itemSlotPrefab = default;
    [SerializeField] protected GameObject itemSlotParent = default;

    protected abstract Inventory inventory { get; set; }
    protected List<ItemSlot> slots = new List<ItemSlot>();

    protected void UpdateItemSlotContents()
    {
        for (int i = 0; i < inventory.contents.Count; i++)
        {
            slots[i].SetItem(inventory.contents[i]);
        }
    }

    protected void UpdateItemSlots()
    {
        if (inventory != null)
        {
            if (slots.Count < inventory.contents.Count)
            {
                InstantiateSlots();
            }
            else if (slots.Count > inventory.contents.Count)
            {
                DestroySlots();
            }

            UpdateItemSlotContents();
        }
    }

    protected virtual void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.contents.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<ItemSlot>();
            slots.Add(newSlot);
        }
    }

    private void DestroySlots()
    {
        for (int i = slots.Count - 1; i > 0; i--)
        {
            var slot = slots[i];
            slots.Remove(slot);
            Destroy(slot.gameObject);
        }
    }
}
