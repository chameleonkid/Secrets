using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorInterface : MonoBehaviour
{
    private static event Action<Inventory> OnInterfaceRequested;
    public static void RequestInterface(Inventory vendorInventory) => OnInterfaceRequested?.Invoke(vendorInventory);
    
    [SerializeField] private GameObject itemSlotPrefab = default;

    [SerializeField] private GameObject vendorPanel = default;
    [SerializeField] private GameObject itemSlotContainer = default;
    [SerializeField] private GameObject firstSelection = default;

    private Inventory activeVendorInventory;
    private List<InventorySlot> slots = new List<InventorySlot>();

    private void OnEnable() => OnInterfaceRequested += ActivateInterface;
    private void OnDisable() => OnInterfaceRequested -= ActivateInterface;

    private void Update()
    {
        if (!vendorPanel.activeInHierarchy) return;
        
        if (Input.GetButtonDown("Inventory"))
        {
            CloseInterface();
        }
    }

    private void ActivateInterface(Inventory vendorInventory) {
        if (CanvasManager.Instance.IsFreeOrActive(vendorPanel))
        {
            OpenInterface(vendorInventory);
            UpdateSlots();

            if (firstSelection)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstSelection);
            }
        }
    }

    private void OpenInterface(Inventory vendorInventory)
    {
        activeVendorInventory = vendorInventory;
        vendorPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseInterface()
    {
        activeVendorInventory = null;
        vendorPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpdateSlots()
    {
        if (activeVendorInventory != null)
        {
            if (slots.Count < activeVendorInventory.contents.Count)
            {
                InstantiateSlots();
            }
            else if (slots.Count > activeVendorInventory.contents.Count)
            {
                DestroySlots();
            }
        }

        UpdateSlotContents();
    }

    private void InstantiateSlots()
    {
        for (int i = slots.Count; i < activeVendorInventory.contents.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotContainer.transform).GetComponent<InventorySlot>();
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

    private void UpdateSlotContents()
    {
        for (int i = 0; i < activeVendorInventory.contents.Count; i++)
        {
            slots[i].SetItem(activeVendorInventory.contents[i]);
        }
    }
}
