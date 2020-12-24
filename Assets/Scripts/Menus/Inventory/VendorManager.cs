using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorManager : ItemDisplay
{
    private static event Action<Inventory> OnInterfaceRequested;
    public static void RequestInterface(Inventory vendorInventory) => OnInterfaceRequested?.Invoke(vendorInventory);

    [SerializeField] private InventoryDisplay playerDisplay = default;
    [SerializeField] private GameObject vendorPanel = default;
    [SerializeField] private TextMeshProUGUI descriptionText = default;
    [SerializeField] private GameObject firstSelection = default;

    protected override Inventory inventory { get; set; }

    private void OnEnable() => OnInterfaceRequested += ActivateInterface;
    private void OnDisable() => OnInterfaceRequested -= ActivateInterface;

    private void Awake()
    {
        playerDisplay.OnSlotSelected += UpdateDescriptionPlayer;
        playerDisplay.OnSlotUsed += SwapItemToVendor;
        playerDisplay.SubscribeToEquipmentSlotSelected(UpdateDescriptionPlayer);
    }

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

    private void UpdateDescriptionPlayer(Item item) => UpdateDescription(item, item.itemSellPrice, "sell");
    private void UpdateDescriptionVendor(Item item) => UpdateDescription(item, item.itemBuyPrice, "buy");
    private void UpdateDescription(Item item, int price, string action)
        => descriptionText.text = (item != null) ? $"{item.name}\n\n{action}: {price}" : "";

    protected override void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.contents.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<ItemSlot>();
            slots.Add(newSlot);

            newSlot.OnSlotSelected += UpdateDescriptionVendor;
            newSlot.OnSlotUsed += SwapItemToPlayer;
        }
    }

    private void SwapItemToPlayer(Item item)
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

    
    private void SwapItemToVendor(Item item)
    {
        // if (item)
        // {
        //     if (item.unique)
        //     {
        //         vendorInventory.Add(item);
        //         playerInventory.RemoveItem(item);
        //         thisVendorManager.clearInventorySlots();
        //         thisVendorManager.MakeInventorySlots();
        //     }
        // }
    }
}
