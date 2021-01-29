using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Inventory")]
public class Inventory : ScriptableObject
{
    public Schwer.ItemSystem.Inventory items = new Schwer.ItemSystem.Inventory();

    public event Action OnEquipmentChanged;

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
    [Header("Currently worn Items")]
    public InventoryWeapon currentWeapon;
    public InventoryArmor currentArmor;
    public InventoryHelmet currentHelmet;
    public InventoryGlove currentGloves;
    public InventoryLegs currentLegs;
    public InventoryShield currentShield;
    public InventoryRing currentRing;
    public InventorySpellbook currentSpellbook;
    public InventorySpellbook currentSpellbookTwo;
    public InventorySpellbook currentSpellbookThree;
    public InventoryAmulet currentAmulet;
    public InventoryBoots currentBoots;
    public InventoryLamp currentLamp;
    public InventoryCloak currentCloak;
    public InventoryBelt currentBelt;
    public InventoryShoulder currentShoulder;
    public InventorySeal currentSeal;

    [Header("Quest Items")]
    public QuestSeed currentSeed;
    public QuestRune currentRune;
    public QuestGem currentGem;
    public QuestPearl currentPearl;
    public QuestDragonEgg currentDragonEgg;
    public QuestArtifact currentArtifact;
    public QuestCrown currentCrown;
    public QuestScepter currentScepter;

    [Header("Calculated Stats")]
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

            case InventorySpellbook spellbook:
                if(!currentSpellbook)
                {
                    Swap(ref currentSpellbook, spellbook);
                }
                else if(!currentSpellbookTwo)
                {
                    Swap(ref currentSpellbookTwo, spellbook);
                }
                else
                {
                    Swap(ref currentSpellbookThree, spellbook);
                }
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
            case InventoryCloak cloak:
                Swap(ref currentCloak, cloak);
                break;
            case InventoryBelt belt:
                Swap(ref currentBelt, belt);
                break;
            case InventoryShoulder shoulder:
                Swap(ref currentShoulder, shoulder);
                break;
            case InventorySeal seal:
                Swap(ref currentSeal, seal);
                break;
            case QuestSeed seed:
                Swap(ref currentSeed, seed);
                break;
            case QuestRune rune:
                Swap(ref currentRune, rune);
                break;
            case QuestGem gem:
                Swap(ref currentGem, gem);
                break;
            case QuestPearl pearl:
                Swap(ref currentPearl, pearl);
                break;
            case QuestArtifact artifact:
                Swap(ref currentArtifact, artifact);
                break;
            case QuestCrown crown:
                Swap(ref currentCrown, crown);
                break;
            case QuestScepter scepter:
                Swap(ref currentScepter, scepter);
                break;
            case QuestDragonEgg dragonEgg:
                Swap(ref currentDragonEgg, dragonEgg);
                break;
        }

        CalcDefense();
        CalcCritChance();
        CalcSpellDamage();
    }


    public void Unequip(EquippableItem item)
    {
        switch (item)
        {
            default:
                // Exit the function early if item is not equippable.
                return;
            case InventoryWeapon weapon:
                Swap(ref currentWeapon, null);
                break;
            case InventoryArmor armor:
                Swap(ref currentArmor, null);
                break;
            case InventoryHelmet helmet:
                Swap(ref currentHelmet, null);
                break;
            case InventoryGlove gloves:
                Swap(ref currentGloves, null);
                break;
            case InventoryLegs legs:
                Swap(ref currentLegs, null);
                break;
            case InventoryShield shield:
                Swap(ref currentShield, null);
                break;
            case InventoryRing ring:
                Swap(ref currentRing, null);
                break;

            case InventorySpellbook spellbook:
                if (currentSpellbook == spellbook)
                {
                    Swap(ref currentSpellbook, null);
                }
                else if (currentSpellbookTwo == spellbook)
                {
                    Swap(ref currentSpellbookTwo, null);
                }
                else
                {
                    Swap(ref currentSpellbookThree, null);
                }
                break;

            case InventoryAmulet amulet:
                Swap(ref currentAmulet, null);
                break;
            case InventoryBoots boots:
                Swap(ref currentBoots, null);
                break;
            case InventoryLamp lamp:
                Swap(ref currentLamp, null);
                break;
            case InventoryCloak cloak:
                Swap(ref currentCloak, null);
                break;
            case InventoryBelt belt:
                Swap(ref currentBelt, null);
                break;
            case InventoryShoulder shoulder:
                Swap(ref currentShoulder, null);
                break;
            case InventorySeal seal:
                Swap(ref currentSeal, null);
                break;
            case QuestSeed seed:
                Swap(ref currentSeed, null);
                break;
            case QuestRune rune:
                Swap(ref currentRune, null);
                break;
            case QuestGem gem:
                Swap(ref currentGem, null);
                break;
            case QuestPearl pearl:
                Swap(ref currentPearl, null);
                break;
            case QuestArtifact artifact:
                Swap(ref currentArtifact, null);
                break;
            case QuestCrown crown:
                Swap(ref currentCrown, null);
                break;
            case QuestScepter scepter:
                Swap(ref currentScepter, null);
                break;
            case QuestDragonEgg dragonEgg:
                Swap(ref currentDragonEgg, null);
                break;
        }

        CalcDefense();
        CalcCritChance();
        CalcSpellDamage();
    }

    //! Currently doesn't account for `items.maxCapacity`!
    private void Swap<T>(ref T currentlyEquipped, T newEquip) where T : EquippableItem
    {
        // Don't do anything if trying to equip the same item.
        if (currentlyEquipped == newEquip) return;

        if (currentlyEquipped != null)
        {
            items[currentlyEquipped]++;
        }

        currentlyEquipped = newEquip;

        if (newEquip != null)
        {
            items[newEquip]--;

            if (newEquip.sound != null)
            {
                SoundManager.RequestSound(newEquip.sound);
            }
        }

        OnEquipmentChanged?.Invoke();
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
        if (currentCloak)
        {
            totalDefense += currentCloak.armorDefense;
        }
        if (currentBelt)
        {
            totalDefense += currentBelt.armorDefense;
        }
        if (currentShoulder)
        {
            totalDefense += currentShoulder.armorDefense;
        }
        if (currentSeal)
        {
            totalDefense += currentSeal.armorDefense;
        }
    }

    public void CalcCritChance()
    {
        totalCritChance = 0;

        if (currentRing)
        {
            totalCritChance += currentRing.criticalStrikeChance;
        }
        if (currentCloak)
        {
            totalCritChance += currentCloak.criticalStrikeChance;
        }
        if (currentBelt)
        {
            totalCritChance += currentBelt.criticalStrikeChance;
        }
        if (currentShoulder)
        {
            totalCritChance += currentShoulder.criticalStrikeChance;
        }
        if (currentSeal)
        {
            totalCritChance += currentSeal.criticalStrikeChance;
        }
    }

    public void CalcSpellDamage()               //Add other items in the same way to add the min/max amount
    {
        totalMinSpellDamage = 0;
        totalMaxSpellDamage = 0;

        if (currentSpellbook || currentSpellbookTwo || currentSpellbookThree)
        {
            if(currentSpellbook)
            {
                totalMinSpellDamage += currentSpellbook.minSpellDamage;
                totalMaxSpellDamage += currentSpellbook.maxSpellDamage;
            }
            if (currentSpellbookTwo)
            {
                totalMinSpellDamage += currentSpellbookTwo.minSpellDamage;
                totalMaxSpellDamage += currentSpellbookTwo.maxSpellDamage;
            }
            if (currentSpellbookThree)
            {
                totalMinSpellDamage += currentSpellbookThree.minSpellDamage;
                totalMaxSpellDamage += currentSpellbookThree.maxSpellDamage;
            }
        }
        if (currentAmulet)
        {
            totalMinSpellDamage += currentAmulet.minSpellDamage;
            totalMaxSpellDamage += currentAmulet.maxSpellDamage;
        }
        if (currentCloak)
        {
            totalMinSpellDamage += currentCloak.minSpellDamage;
            totalMaxSpellDamage += currentCloak.maxSpellDamage;
        }
    }

    public void ResetEquipment()
    {
        currentWeapon = null;
        currentArmor = null;
        currentHelmet = null;
        currentGloves = null;
        currentLegs = null;
        currentShield = null;
        currentRing = null;
        currentSpellbook = null;
        currentSpellbookTwo = null;
        currentSpellbookThree = null;
        currentAmulet = null;
        currentBoots = null;
        currentLamp = null;
        currentCloak = null;
        currentBelt = null;
        currentShoulder = null;
        currentSeal = null;
        currentSeed = null;
        currentRune = null;
        currentGem = null;
        currentPearl = null;
        currentArtifact = null;
        currentDragonEgg = null;
        currentCrown = null;
        currentScepter = null;
    }

    [Serializable]
    public class SerializableInventory
    {
        [ES3Serializable] private Schwer.ItemSystem.SerializableInventory items;

        [ES3Serializable] private int coins;

        [ES3Serializable] private int weaponID = int.MinValue;
        [ES3Serializable] private int armorID = int.MinValue;
        [ES3Serializable] private int helmetID = int.MinValue;
        [ES3Serializable] private int glovesID = int.MinValue;
        [ES3Serializable] private int legsID = int.MinValue;
        [ES3Serializable] private int shieldID = int.MinValue;
        [ES3Serializable] private int ringID = int.MinValue;
        [ES3Serializable] private int spellbook1ID = int.MinValue;
        [ES3Serializable] private int spellbook2ID = int.MinValue;
        [ES3Serializable] private int spellbook3ID = int.MinValue;
        [ES3Serializable] private int amuletID = int.MinValue;
        [ES3Serializable] private int bootsID = int.MinValue;
        [ES3Serializable] private int lampID = int.MinValue;
        [ES3Serializable] private int cloakID = int.MinValue;
        [ES3Serializable] private int shoulderID = int.MinValue;
        [ES3Serializable] private int sealID = int.MinValue;

        [ES3Serializable] private int seedID = int.MinValue;
        [ES3Serializable] private int runeID = int.MinValue;
        [ES3Serializable] private int gemID = int.MinValue;
        [ES3Serializable] private int pearlID = int.MinValue;
        [ES3Serializable] private int eggID = int.MinValue;
        [ES3Serializable] private int artifactID = int.MinValue;
        [ES3Serializable] private int crownID = int.MinValue;
        [ES3Serializable] private int scepterID = int.MinValue;

        public SerializableInventory() {}  // Parameterless constructor necessary to be compatible with ES3
        public SerializableInventory(Inventory inventory)
        {
            items = inventory.items.Serialize();
            coins = inventory.coins;

            weaponID = GetIDOrMinValue(inventory.currentWeapon);
            armorID = GetIDOrMinValue(inventory.currentArmor);
            helmetID = GetIDOrMinValue(inventory.currentHelmet);
            glovesID = GetIDOrMinValue(inventory.currentGloves);
            legsID = GetIDOrMinValue(inventory.currentLegs);
            shieldID = GetIDOrMinValue(inventory.currentShield);
            ringID = GetIDOrMinValue(inventory.currentRing);
            spellbook1ID = GetIDOrMinValue(inventory.currentSpellbook);
            spellbook2ID = GetIDOrMinValue(inventory.currentSpellbookTwo);
            spellbook3ID = GetIDOrMinValue(inventory.currentSpellbookThree);
            amuletID = GetIDOrMinValue(inventory.currentAmulet);
            bootsID = GetIDOrMinValue(inventory.currentBoots);
            lampID = GetIDOrMinValue(inventory.currentLamp);
            cloakID = GetIDOrMinValue(inventory.currentCloak);
            shoulderID = GetIDOrMinValue(inventory.currentShoulder);
            sealID = GetIDOrMinValue(inventory.currentSeal);

            seedID = GetIDOrMinValue(inventory.currentSeed);
            runeID = GetIDOrMinValue(inventory.currentRune);
            gemID = GetIDOrMinValue(inventory.currentGem);
            pearlID = GetIDOrMinValue(inventory.currentPearl);
            eggID = GetIDOrMinValue(inventory.currentDragonEgg);
            artifactID = GetIDOrMinValue(inventory.currentArtifact);
            crownID = GetIDOrMinValue(inventory.currentCrown);
            scepterID = GetIDOrMinValue(inventory.currentScepter);
        }

        public void DeserializeInto(Inventory target, Schwer.ItemSystem.ItemDatabase itemDatabase)
        {
            target.items = items.Deserialize(itemDatabase);
            target.coins = coins;

            target.currentWeapon = (weaponID != int.MinValue ? itemDatabase.GetItem(weaponID) as InventoryWeapon : null);
            target.currentArmor = (armorID != int.MinValue ? itemDatabase.GetItem(armorID) as InventoryArmor : null);
            target.currentHelmet = (helmetID != int.MinValue ? itemDatabase.GetItem(helmetID) as InventoryHelmet : null);
            target.currentGloves = (glovesID != int.MinValue ? itemDatabase.GetItem(glovesID) as InventoryGlove : null);
            target.currentLegs = (legsID != int.MinValue ? itemDatabase.GetItem(legsID) as InventoryLegs : null);
            target.currentShield = (shieldID != int.MinValue ? itemDatabase.GetItem(shieldID) as InventoryShield : null);
            target.currentRing = (ringID != int.MinValue ? itemDatabase.GetItem(ringID) as InventoryRing : null);
            target.currentSpellbook = (spellbook1ID != int.MinValue ? itemDatabase.GetItem(spellbook1ID) as InventorySpellbook : null);
            target.currentSpellbookTwo = (spellbook2ID != int.MinValue ? itemDatabase.GetItem(spellbook2ID) as InventorySpellbook : null);
            target.currentSpellbookThree = (spellbook3ID != int.MinValue ? itemDatabase.GetItem(spellbook3ID) as InventorySpellbook : null);
            target.currentAmulet = (amuletID != int.MinValue ? itemDatabase.GetItem(amuletID) as InventoryAmulet : null);
            target.currentBoots = (bootsID != int.MinValue ? itemDatabase.GetItem(bootsID) as InventoryBoots : null);
            target.currentLamp = (lampID != int.MinValue ? itemDatabase.GetItem(lampID) as InventoryLamp : null);
            target.currentCloak = (cloakID != int.MinValue ? itemDatabase.GetItem(cloakID) as InventoryCloak : null);
            target.currentShoulder = (shoulderID != int.MinValue ? itemDatabase.GetItem(shoulderID) as InventoryShoulder : null);
            target.currentSeal = (sealID != int.MinValue ? itemDatabase.GetItem(sealID) as InventorySeal : null);

            target.currentSeed = (seedID != int.MinValue ? itemDatabase.GetItem(seedID) as QuestSeed : null);
            target.currentRune = (runeID != int.MinValue ? itemDatabase.GetItem(runeID) as QuestRune : null);
            target.currentGem = (gemID != int.MinValue ? itemDatabase.GetItem(gemID) as QuestGem : null);
            target.currentPearl = (pearlID != int.MinValue ? itemDatabase.GetItem(pearlID) as QuestPearl : null);
            target.currentDragonEgg = (eggID != int.MinValue ? itemDatabase.GetItem(eggID) as QuestDragonEgg : null);
            target.currentArtifact = (artifactID != int.MinValue ? itemDatabase.GetItem(artifactID) as QuestArtifact : null);
            target.currentCrown = (crownID != int.MinValue ? itemDatabase.GetItem(crownID) as QuestCrown : null);
            target.currentScepter = (scepterID != int.MinValue ? itemDatabase.GetItem(scepterID) as QuestScepter : null);
        }

        private int GetIDOrMinValue(Item item)
        {
            if (item != null) return item.id;
            else return int.MinValue;
        }
    }
}
