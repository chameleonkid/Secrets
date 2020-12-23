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
