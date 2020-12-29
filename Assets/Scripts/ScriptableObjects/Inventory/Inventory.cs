using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class Inventory : ScriptableObject
{
    public Schwer.ItemSystem.Inventory items = new Schwer.ItemSystem.Inventory();
    public int coins;

    public Item currentItem;
    
    public InventoryWeapon currentWeapon;
    public InventoryArmor currentArmor;
    public InventoryHelmet currentHelmet;
    public InventoryGlove currentGloves;
    public InventoryLegs currentLegs;
    public InventoryShield currentShield;
    public InventoryRing currentRing;
    public InventoryBow currentBow;
    public InventorySpellbook currentSpellbook;
    public InventoryAmulet currentAmulet;
    public InventoryBoots currentBoots;

    public float totalDefense;
    public float totalCritChance;
    public int totalMinSpellDamage;
    public int totalMaxSpellDamage;

#if UNITY_EDITOR
        // Needed in order to allow changes to the Inventory in the editor to be saved.

        private void OnEnable() => items.OnContentsChanged += MarkDirtyIfChanged;
        private void OnDisable() => items.OnContentsChanged -= MarkDirtyIfChanged;

        private void MarkDirtyIfChanged(Item item, int count) => UnityEditor.EditorUtility.SetDirty(this);
#endif

    public void Equip(Item item)
    {
        switch (item)
        {
            default:
                // Exit the function early if item is not equippable.
                return;
            case InventoryWeapon weapon:
                Swap(ref currentWeapon, weapon);
                break;
            case InventoryArmor armor:
                Swap(ref currentArmor, armor);
                break;
            case InventoryHelmet helmet:
                Swap(ref currentHelmet, helmet);
                break;
            case InventoryGlove gloves:
                Swap(ref currentGloves, gloves);
                break;
            case InventoryLegs legs:
                Swap(ref currentLegs, legs);
                break;
            case InventoryShield shield:
                Swap(ref currentShield, shield);
                break;
            case InventoryRing ring:
                Swap(ref currentRing, ring);
                break;
            case InventoryBow bow:
                Swap(ref currentBow, bow);
                break;
            case InventorySpellbook spellbook:
                Swap(ref currentSpellbook, spellbook);
                break;
            case InventoryAmulet amulet:
                Swap(ref currentAmulet, amulet);
                break;
            case InventoryBoots boots:
                Swap(ref currentBoots, boots);
                break;
        }
        // Applies to all equippables.
        CalcDefense();
        CalcCritChance();
        CalcSpellDamage();
    }

    private void Swap<T>(ref T currentlyEquipped, T newEquip) where T : EquippableItem
    {
        if (currentlyEquipped != null)
        {
            items[currentlyEquipped]++;
        }

        currentlyEquipped = newEquip;
        newEquip.numberHeld--;

        if (newEquip.itemSound != null)
        {
            SoundManager.RequestSound(newEquip.itemSound);
        }
    }

    public void CalcDefense()
    {
        totalDefense = 0;

        if (currentArmor)
        {
            totalDefense += currentArmor.armorDefense;
        }
        if (currentHelmet)
        {
            totalDefense += currentHelmet.armorDefense;
        }
        if (currentGloves)
        {
            totalDefense += currentGloves.armorDefense;
        }
        if (currentLegs)
        {
            totalDefense += currentLegs.armorDefense;
        }
        if (currentShield)
        {
            totalDefense += currentShield.armorDefense;
        }
        if (currentBoots)
        {
            totalDefense += currentBoots.armorDefense;
        }
    }

    public void CalcCritChance()
    {
        totalCritChance = 0;

        if (currentRing)
        {
            totalCritChance += currentRing.criticalStrikeChance;
        }
    }

    public void CalcSpellDamage()               //Add other items in the same way to add the min/max amount
    {
        totalMinSpellDamage = 0;
        totalMaxSpellDamage = 0;

        if (currentSpellbook)
        {
            totalMinSpellDamage += currentSpellbook.minSpellDamage;
            totalMaxSpellDamage += currentSpellbook.maxSpellDamage;
        }
        if (currentAmulet)
        {
            totalMinSpellDamage += currentAmulet.minSpellDamage;
            totalMaxSpellDamage += currentAmulet.maxSpellDamage;
        }
    }
}

