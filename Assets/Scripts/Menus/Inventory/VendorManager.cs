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
    [SerializeField] private CoinTextManager vendorCoinText = default;
    [SerializeField] private TextMeshProUGUI descriptionText = default;
    [SerializeField] private GameObject firstSelection = default;

    private Inventory _inventory;
    public override Inventory inventory {
        get => _inventory;
        protected set {
            if (_inventory != value) {
                if (_inventory != null) {
                    _inventory.items.OnContentsChanged -= UpdateItemSlots;
                }

                if (value != null) {
                    value.items.OnContentsChanged += UpdateItemSlots;
                }

                _inventory = value;
                vendorCoinText.inventory = value;
            }
        }
    }

    private void OnEnable() => OnInterfaceRequested += ActivateInterface;
    private void OnDisable() => OnInterfaceRequested -= ActivateInterface;

    private void Awake()
    {
        playerDisplay.OnSlotSelected += UpdateDescriptionPlayer;
        playerDisplay.OnSlotUsed += SellItem;
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

            EventSystemSelectDefault();
        }
    }

    private void OpenInterface(Inventory vendorInventory)
    {
        inventory = vendorInventory;
        vendorPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseInterface()
    {
        inventory = null;
        vendorPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpdateDescriptionPlayer(Item item) => UpdateDescription(item, item.sellPrice, "sell");
    private void UpdateDescriptionVendor(Item item) => UpdateDescription(item, item.buyPrice, "buy");
    private void UpdateDescription(Item item, int price, string action)
        => descriptionText.text = (item != null) ? $"{item.name}\n\n{action}: {price}" : "";

    protected override void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.items.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<ItemSlot>();
            slots.Add(newSlot);

            newSlot.OnSlotSelected += UpdateDescriptionVendor;
            newSlot.OnSlotUsed += BuyItem;
        }
    }

    private void BuyItem(Item item)
    {
        if (playerDisplay.inventory.coins >= item.buyPrice)
        {
            if (item.unique && playerDisplay.inventory.items[item] > 0)
            {
                descriptionText.text = $"You already own {item.name}!";
                return;
            }

            playerDisplay.inventory.items[item]++;
            playerDisplay.inventory.coins -= item.buyPrice;
            
            inventory.items[item]--;
            inventory.coins += item.buyPrice;

            descriptionText.text = $"Bought {item.name} for {item.buyPrice}.";

            if (inventory.items[item] <= 0)
            {
                EventSystemSelectDefault();
            }
        }
        else
        {
            descriptionText.text = $"Not enough money to buy {item}!";
        }
    }

    
    private void SellItem(Item item)
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

    private void EventSystemSelectDefault()
    {
        if (firstSelection)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelection);
        }
    }
}
