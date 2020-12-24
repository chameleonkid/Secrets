using System;
using UnityEngine;

public class InventoryDisplay : ItemDisplay
{
    [SerializeField] private Inventory _inventory;
    protected override Inventory inventory {
        get => _inventory;
        set => _inventory = value;
    }

    [Header("Equipment Slots")]
    [SerializeField] private ItemSlot weaponSlot = default;
    [SerializeField] private ItemSlot armorSlot = default;
    [SerializeField] private ItemSlot helmetSlot = default;
    [SerializeField] private ItemSlot gloveSlot = default;
    [SerializeField] private ItemSlot legsSlot = default;
    [SerializeField] private ItemSlot shieldSlot = default;
    [SerializeField] private ItemSlot ringSlot = default;
    [SerializeField] private ItemSlot bowSlot = default;
    [SerializeField] private ItemSlot spellbookSlot = default;
    [SerializeField] private ItemSlot amuletSlot = default;
    [SerializeField] private ItemSlot bootsSlot = default;

    public Action<InventoryItem> OnSlotSelected { get; set; }
    public Action<InventoryItem> OnSlotUsed { get; set; }

    private void OnEnable() => UpdateDisplay();

    public void SubscribeToEquipmentSlotSelected(Action<InventoryItem> action)
    {
        weaponSlot.OnSlotSelected += action;
        armorSlot.OnSlotSelected += action;
        helmetSlot.OnSlotSelected += action;
        gloveSlot.OnSlotSelected += action;
        legsSlot.OnSlotSelected += action;
        shieldSlot.OnSlotSelected += action;
        ringSlot.OnSlotSelected += action;
        bowSlot.OnSlotSelected += action;
        spellbookSlot.OnSlotSelected += action;
        amuletSlot.OnSlotSelected += action;
        bootsSlot.OnSlotSelected += action;
    }

    public void UnsubscribeFromEquipmentSlotSelected(Action<InventoryItem> action)
    {
        weaponSlot.OnSlotSelected -= action;
        armorSlot.OnSlotSelected -= action;
        helmetSlot.OnSlotSelected -= action;
        gloveSlot.OnSlotSelected -= action;
        legsSlot.OnSlotSelected -= action;
        shieldSlot.OnSlotSelected -= action;
        ringSlot.OnSlotSelected -= action;
        bowSlot.OnSlotSelected -= action;
        spellbookSlot.OnSlotSelected -= action;
        amuletSlot.OnSlotSelected -= action;
        bootsSlot.OnSlotSelected -= action;
    }

    public void UpdateDisplay()
    {
        UpdateItemSlots();
        UpdateEquipmentSlots();
    }

    private void UpdateEquipmentSlots()
    {
        weaponSlot.SetItem(inventory.currentWeapon);
        armorSlot.SetItem(inventory.currentArmor);
        helmetSlot.SetItem(inventory.currentHelmet);
        gloveSlot.SetItem(inventory.currentGloves);
        legsSlot.SetItem(inventory.currentLegs);
        shieldSlot.SetItem(inventory.currentShield);
        ringSlot.SetItem(inventory.currentRing);
        bowSlot.SetItem(inventory.currentBow);
        spellbookSlot.SetItem(inventory.currentSpellbook);
        amuletSlot.SetItem(inventory.currentAmulet);
        bootsSlot.SetItem(inventory.currentBoots);
    }

    protected override void InstantiateSlots()
    {
        for (int i = slots.Count; i < inventory.contents.Count; i++)
        {
            var newSlot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, itemSlotParent.transform).GetComponent<ItemSlot>();
            slots.Add(newSlot);

            if (OnSlotSelected != null)
            {
                newSlot.OnSlotSelected += OnSlotSelected;
            }
            
            if (OnSlotUsed != null)
            {
                newSlot.OnSlotUsed += OnSlotUsed;
            }
        }
    }
}
