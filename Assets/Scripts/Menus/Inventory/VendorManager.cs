using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorManager : ItemDisplay
{
    private static event Action<Inventory> OnInterfaceRequested;
    public static void RequestInterface(Inventory vendorInventory) => OnInterfaceRequested?.Invoke(vendorInventory);

    [SerializeField] private GameObject vendorPanel = default;
    [SerializeField] private GameObject firstSelection = default;

    protected override Inventory inventory { get; set; }

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
            UpdateItemSlots();

            if (firstSelection)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstSelection);
            }
        }
    }

    private void OpenInterface(Inventory vendorInventory)
    {
        inventory = vendorInventory;
        vendorPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseInterface()
    {
        inventory = null;
        vendorPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
