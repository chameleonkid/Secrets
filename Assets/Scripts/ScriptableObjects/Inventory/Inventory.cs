using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Inventory")]
public class Inventory : ScriptableObject
{
    public Schwer.ItemSystem.Inventory items = new Schwer.ItemSystem.Inventory();

    public event Action OnCoinCountChanged;
    [SerializeField] private int _coins;
    public int coins {
        get => _coins;
        set {
            if (value != _coins) {
                _coins = value;
                OnCoinCountChanged?.Invoke();
            }
        }
    }


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
    public InventoryLamp currentLamp;

    public float totalDefense;
    public float totalCritChance;
    public int totalMinSpellDamage;
    public int totalMaxSpellDamage;



#if UNITY_EDITOR
    // Needed in order to allow changes to the Inventory in the editor to be saved.

    private bool shouldMarkDirty;

    private void OnEnable() => UnityEditor.EditorApplication.playModeStateChanged += MarkDirty;

    private void OnDisable() => UnityEditor.EditorApplication.playModeStateChanged -= MarkDirty;

    private void MarkDirtyIfChanged(Item item, int count) => shouldMarkDirty = true;

    private void MarkDirty(UnityEditor.PlayModeStateChange stateChange) {
        // Can't run outside of Play Mode as that seems to throw errors (can't make certain calls on serialization thread)
        if (stateChange == UnityEditor.PlayModeStateChange.EnteredEditMode) {
            items.OnContentsChanged -= MarkDirtyIfChanged;

            if (shouldMarkDirty) {
                UnityEditor.EditorUtility.SetDirty(this);
                shouldMarkDirty = false;
            }
        }
        else if (stateChange == UnityEditor.PlayModeStateChange.EnteredPlayMode) {
            items.OnContentsChanged += MarkDirtyIfChanged;
        }
    }
#endif

    public void Equip(EquippableItem item)
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
            case InventoryLamp lamp:
                Swap(ref currentLamp, lamp);
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
        items[newEquip]--;

        if (newEquip.sound != null)
        {
            SoundManager.RequestSound(newEquip.sound);
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

