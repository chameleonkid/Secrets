using UnityEngine;

public class InventoryDisplay : ItemDisplay
{
    [SerializeField] private Inventory _inventory;
    protected override Inventory inventory {
        get => _inventory;
        set => _inventory = value;
    }

    [Header("Equipment Slots")]
    [SerializeField] private InventorySlot weaponSlot = default;
    [SerializeField] private InventorySlot armorSlot = default;
    [SerializeField] private InventorySlot helmetSlot = default;
    [SerializeField] private InventorySlot gloveSlot = default;
    [SerializeField] private InventorySlot legsSlot = default;
    [SerializeField] private InventorySlot shieldSlot = default;
    [SerializeField] private InventorySlot ringSlot = default;
    [SerializeField] private InventorySlot bowSlot = default;
    [SerializeField] private InventorySlot spellbookSlot = default;
    [SerializeField] private InventorySlot amuletSlot = default;
    [SerializeField] private InventorySlot bootsSlot = default;

    private void OnEnable() => UpdateDisplay();

    private void UpdateDisplay()
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
}
