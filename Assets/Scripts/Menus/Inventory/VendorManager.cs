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

    [SerializeField] private AudioClip sellSound = default;
    [SerializeField] private AudioClip buySound = default;
    [SerializeField] private AudioClip cantSellOrBuySound = default;

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
        if (CanvasManager.Instance.IsFreeOrActive(vendorPanel) && !vendorPanel.activeInHierarchy)
        {
            OpenInterface(vendorInventory);
            UpdateItemSlots();

            EventSystemSelectDefault();
        }
    }

    private void OpenInterface(Inventory vendorInventory)
    {
        descriptionText.text = "";
        inventory = vendorInventory;
        vendorPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseInterface()
    {
        inventory = null;
        vendorPanel.SetActive(false);
        CanvasManager.Instance.RegisterClosedCanvas(vendorPanel);
        Time.timeScale = 1;
    }

    private void UpdateDescriptionPlayer(Item item) => UpdateDescription(item, item.sellPrice, "sell for:\n");
    private void UpdateDescriptionVendor(Item item) => UpdateDescription(item, item.buyPrice, "buy for:\n");
    private void UpdateDescription(Item item, int price, string action)
        => descriptionText.text = (item != null) ? $"{item.name}\n {item.fullDescription} \n{action} {price}" : " Gold ";

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
                SoundManager.RequestSound(cantSellOrBuySound);
                return;
            }
            SoundManager.RequestSound(buySound);
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
            descriptionText.text = $"Not enough money to buy {item.name}!";
            SoundManager.RequestSound(cantSellOrBuySound);
        }
    }

    
    private void SellItem(Item item)
    {
        if (inventory.coins >= item.sellPrice)
        {
            SoundManager.RequestSound(sellSound);
            playerDisplay.inventory.items[item]--;
            playerDisplay.inventory.coins += item.sellPrice;
            
            inventory.items[item]++;
            inventory.coins -= item.sellPrice;

            descriptionText.text = $"Sold {item.name} for {item.sellPrice}.";

            if (playerDisplay.inventory.items[item] <= 0)
            {
                EventSystemSelectDefault();
            }
        }
        else
        {
            descriptionText.text = $"Vendor does not have enough money to buy your {item.name}!";
            SoundManager.RequestSound(cantSellOrBuySound);
        }
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
