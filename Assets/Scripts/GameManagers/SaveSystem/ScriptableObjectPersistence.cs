using Schwer.ItemSystem;
using UnityEngine;

public class ScriptableObjectPersistence : MonoBehaviour
{
    [Header("Databases")]
    [SerializeField] private ItemDatabase _itemDatabase = default;
    public ItemDatabase itemDatabase => _itemDatabase;

    [SerializeField] private SkinTexturesDatabase[] _skinTexturesDatabases = default;
    public SkinTexturesDatabase[] skinTexturesDatabases => _skinTexturesDatabases;

    [Header("Save Data")]
    [SerializeField] private StringValue _saveName = default;
    public StringValue saveName => _saveName;

    [SerializeField] private VectorValue _playerPosition = default;
    public VectorValue playerPosition => _playerPosition;
    [SerializeField] private ConstrainedFloat _health = default;
    public ConstrainedFloat health => _health;
    [SerializeField] private ConstrainedFloat _mana = default;
    public ConstrainedFloat mana => _mana;
    [SerializeField] private ConstrainedFloat _lumen = default;
    public ConstrainedFloat lumen => _lumen;

    [SerializeField] private Inventory _playerInventory = default;
    public Inventory playerInventory => _playerInventory;

    [SerializeField] private VendorInventorySet[] _vendorInventories = default;
    public VendorInventorySet[] vendorInventories => _vendorInventories;

    [SerializeField] private BoolValue[] _chests = default;
    public BoolValue[] chests => _chests;
    [SerializeField] private BoolValue[] _doors = default;
    public BoolValue[] doors => _doors;
    [SerializeField] private BoolValue[] _bosses = default;
    public BoolValue[] bosses => _bosses;

    [SerializeField] private BoolValue[] _healthCrystals = default;
    public BoolValue[] healthCrystals => _healthCrystals;
    [SerializeField] private BoolValue[] _manaCrystals = default;
    public BoolValue[] manaCrystals => _manaCrystals;

    [SerializeField] private BoolValue[] _cutscenes = default;
    public BoolValue[] cutscenes => _cutscenes;

    [SerializeField] private CharacterAppearance _characterAppearance = default;
    public CharacterAppearance characterAppearance => _characterAppearance;

    [SerializeField] private XPSystem _xpSystem = default;
    public XPSystem xpSystem => _xpSystem;

    [SerializeField] private FloatValue _timeOfDay = default;
    public FloatValue timeOfDay => _timeOfDay;

    public void ResetScriptableObjects()
    {
        ResetSaveName();
        ResetPlayer();
        ResetInventory();
        ResetVendorInventories();
        ResetBools();
        ResetXP();
        timeOfDay.value = 0;
  
        Debug.Log("Reset scriptable object save data.");
    }

    private void ResetSaveName() => saveName.RuntimeValue = saveName.initialValue;

    public void ResetPlayer()
    {
        playerPosition.ResetValue();
        health.max = 25;
        health.current = health.max;
        mana.max = 100;
        mana.current = mana.max;
        lumen.max = 100;
        lumen.current = 100;
    }

    public void ResetBools()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].RuntimeValue = chests[i].initialValue;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].RuntimeValue = doors[i].initialValue;
        }
        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].RuntimeValue = bosses[i].initialValue;
        }
        for (int i = 0; i < healthCrystals.Length; i++)
        {
            healthCrystals[i].RuntimeValue = healthCrystals[i].initialValue;
        }
        for (int i = 0; i < manaCrystals.Length; i++)
        {
            manaCrystals[i].RuntimeValue = manaCrystals[i].initialValue;
        }
        for (int i = 0; i < cutscenes.Length; i++)
        {
            cutscenes[i].RuntimeValue = cutscenes[i].initialValue;
        }
    }

    public void ResetInventory()
    {
        playerInventory.coins = 0;
        playerInventory.items = new Schwer.ItemSystem.Inventory();

        playerInventory.currentItem = null;
        playerInventory.ResetEquipment();

        playerInventory.totalDefense = 0;
        playerInventory.totalCritChance = 0;
        playerInventory.totalMaxSpellDamage = 0;
        playerInventory.totalMinSpellDamage = 0;
    }

    public void ResetVendorInventories()
    {
        foreach (var v in vendorInventories)
        {
            var reg = v.regular;
            var ini = v.initial;

            if (reg != null && ini == null)
            {
                Debug.LogWarning($"Runtime inventory '{reg.name}' is missing its default equivalent.");
                continue;
            }
            else if (reg == null && ini != null)
            {
                Debug.LogWarning($"Default inventory '{ini.name}' is missing its runtime equivalent.");
                continue;
            }
            else if (reg == null && ini == null)
            {
                Debug.LogWarning("Null entry in vendorInventories (skipped)!");
                continue;
            }

            reg.coins = ini.coins;
            reg.items = ini.items;

            // Would vendors ever have gear?
            reg.currentItem = ini.currentItem;
            reg.currentWeapon = ini.currentWeapon;
            reg.currentArmor = ini.currentArmor;
            reg.currentHelmet = ini.currentHelmet;
            reg.currentGloves = ini.currentGloves;
            reg.currentLegs = ini.currentLegs;
            reg.currentShield = ini.currentShield;
            reg.currentRing = ini.currentRing;
            reg.currentSpellbook = ini.currentSpellbook;
            reg.currentSpellbookTwo = ini.currentSpellbookTwo;
            reg.currentSpellbookThree = ini.currentSpellbookThree;
            reg.currentAmulet = ini.currentAmulet;
            reg.currentBoots = ini.currentBoots;
            reg.currentLamp = ini.currentLamp;
            reg.currentCloak = ini.currentCloak;
            reg.currentBelt = ini.currentBelt;
            reg.currentShoulder = ini.currentShoulder;
            reg.currentSeal = ini.currentSeal;

            reg.currentSeed = ini.currentSeed;
            reg.currentRune = ini.currentRune;
            reg.currentGem = ini.currentGem;
            reg.currentPearl = ini.currentPearl;
            reg.currentDragonEgg = ini.currentDragonEgg;
            reg.currentArtifact = ini.currentArtifact;
            reg.currentCrown = ini.currentCrown;
            reg.currentScepter = ini.currentScepter;

            // Would vendors ever need this?
            reg.totalDefense = ini.totalDefense;
            reg.totalCritChance = ini.totalCritChance;
            reg.totalMaxSpellDamage = ini.totalMaxSpellDamage;
            reg.totalMinSpellDamage = ini.totalMinSpellDamage;
        }
    }

    public void ResetXP() => _xpSystem.ResetExperienceSystem();

    [System.Serializable]
    public struct VendorInventorySet
    {
        public Inventory regular;
        public Inventory initial;

        public VendorInventorySet(Inventory regular, Inventory initial)
        {
            this.regular = regular;
            this.initial = initial;
        }
    }
}
