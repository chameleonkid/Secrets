using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDisplay : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab = default;
    [SerializeField] private GameObject itemSlotParent = default;

    protected abstract Inventory inventory { get; set; }
    protected List<InventorySlot> slots = new List<InventorySlot>();

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

    private void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.contents.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<InventorySlot>();
            slots.Add(newSlot);
            // newSlot.OnSlotSelected += SetUpItemDescription;
            // newSlot.OnSlotUsed += OnItemUsed;
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
