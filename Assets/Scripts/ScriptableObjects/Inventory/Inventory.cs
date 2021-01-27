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
                totalMinSpellDamage += currentSpellbookTwo.minSpellDamage;
                totalMaxSpellDamage += currentSpellbookTwo.maxSpellDamage;
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

    #region Serialisation
    [Serializable]
    public class SerializableInventory
    {
        private Schwer.ItemSystem.SerializableInventory items;

        private int weaponID;
        private int armorID;
        private int helmetID;
        private int glovesID;
        private int legsID;
        private int shieldID;
        private int ringID;
        private int spellbook1ID;
        private int spellbook2ID;
        private int spellbook3ID;
        private int amuletID;
        private int bootsID;
        private int lampID;
        private int cloakID;
        private int shoulderID;
        private int sealID;

        private int seedID;
        private int runeID;
        private int gemID;
        private int pearlID;
        private int eggID;
        private int artifactID;
        private int crownID;
        private int scepterID;

        public SerializableInventory(Inventory inventory)
        {
            items = inventory.items.Serialize();

            weaponID = inventory.currentWeapon.id;
            armorID = inventory.currentArmor.id;
            helmetID = inventory.currentHelmet.id;
            glovesID = inventory.currentGloves.id;
            legsID = inventory.currentLegs.id;
            shieldID = inventory.currentShield.id;
            ringID = inventory.currentRing.id;
            spellbook1ID = inventory.currentSpellbook.id;
            spellbook2ID = inventory.currentSpellbookTwo.id;
            spellbook3ID = inventory.currentSpellbookThree.id;
            amuletID = inventory.currentAmulet.id;
            bootsID = inventory.currentBoots.id;
            lampID = inventory.currentLamp.id;
            cloakID = inventory.currentCloak.id;
            shoulderID = inventory.currentShoulder.id;
            sealID = inventory.currentSeal.id;

            seedID = inventory.currentSeed.id;
            runeID = inventory.currentRune.id;
            gemID = inventory.currentGem.id;
            pearlID = inventory.currentPearl.id;
            eggID = inventory.currentDragonEgg.id;
            artifactID = inventory.currentArtifact.id;
            crownID = inventory.currentCrown.id;
            scepterID = inventory.currentScepter.id;
        }

        public void DeserializeInto(Inventory target, Schwer.ItemSystem.ItemDatabase itemDatabase)
        {
            target.items = items.Deserialize(itemDatabase);

            target.currentWeapon = (itemDatabase.GetItem(weaponID) as InventoryWeapon);
            target.currentArmor = (itemDatabase.GetItem(armorID) as InventoryArmor);
            target.currentHelmet = (itemDatabase.GetItem(helmetID) as InventoryHelmet);
            target.currentGloves = (itemDatabase.GetItem(glovesID) as InventoryGlove);
            target.currentLegs = (itemDatabase.GetItem(legsID) as InventoryLegs);
            target.currentShield = (itemDatabase.GetItem(shieldID) as InventoryShield);
            target.currentRing = (itemDatabase.GetItem(ringID) as InventoryRing);
            target.currentSpellbook = (itemDatabase.GetItem(spellbook1ID) as InventorySpellbook);
            target.currentSpellbookTwo = (itemDatabase.GetItem(spellbook2ID) as InventorySpellbook);
            target.currentSpellbookThree = (itemDatabase.GetItem(spellbook3ID) as InventorySpellbook);
            target.currentAmulet = (itemDatabase.GetItem(amuletID) as InventoryAmulet);
            target.currentBoots = (itemDatabase.GetItem(bootsID) as InventoryBoots);
            target.currentLamp = (itemDatabase.GetItem(lampID) as InventoryLamp);
            target.currentCloak = (itemDatabase.GetItem(cloakID) as InventoryCloak);
            target.currentShoulder = (itemDatabase.GetItem(shoulderID) as InventoryShoulder);
            target.currentSeal = (itemDatabase.GetItem(sealID) as InventorySeal);

            target.currentSeed = (itemDatabase.GetItem(seedID) as QuestSeed);
            target.currentRune = (itemDatabase.GetItem(runeID) as QuestRune);
            target.currentGem = (itemDatabase.GetItem(gemID) as QuestGem);
            target.currentPearl = (itemDatabase.GetItem(pearlID) as QuestPearl);
            target.currentDragonEgg = (itemDatabase.GetItem(eggID) as QuestDragonEgg);
            target.currentArtifact = (itemDatabase.GetItem(artifactID) as QuestArtifact);
            target.currentCrown = (itemDatabase.GetItem(crownID) as QuestCrown);
            target.currentScepter = (itemDatabase.GetItem(scepterID) as QuestScepter);
        }
    }
    #endregion
}
