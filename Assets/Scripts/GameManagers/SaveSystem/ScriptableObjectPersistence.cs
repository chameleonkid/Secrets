using Schwer.ItemSystem;
using UnityEngine;

[RequireComponent(typeof(SimpleSave))]
public class ScriptableObjectPersistence : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ItemDatabase _itemDatabase = default;
    public ItemDatabase itemDatabase => _itemDatabase;

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
    
    [SerializeField] private Inventory[] _vendorInventories = default;
    public Inventory[] vendorInventories => _vendorInventories;

    [SerializeField] private BoolValue[] _chests = default;
    public BoolValue[] chests => _chests;
    [SerializeField] private BoolValue[] _doors = default;
    public BoolValue[] doors => _doors;
    [SerializeField] private BoolValue[] _bosses = default;
    public BoolValue[] bosses => _bosses;

    [SerializeField] private XPSystem _xpSystem = default;
    public XPSystem xpSystem => _xpSystem;

    public void ResetScriptableObjects()
    {
        ResetPlayer();
        ResetInventory();
        ResetVendorInventories();
        ResetBools();
        ResetXP();

        Debug.Log("Reset scriptable object save data.");
    }

    public void ResetPlayer()
    {
        health.max = 10;
        health.current = health.max;
        mana.max = 100;
        mana.current = mana.max;
        lumen.max = 10;
        lumen.current = 10;
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
    }

    public void ResetInventory()
    {
        playerInventory.coins = 0;
        playerInventory.items = new Schwer.ItemSystem.Inventory();

        playerInventory.currentItem = null;
        playerInventory.currentWeapon = null;
        playerInventory.currentArmor = null;
        playerInventory.currentHelmet = null;
        playerInventory.currentGloves = null;
        playerInventory.currentLegs = null;
        playerInventory.currentShield = null;
        playerInventory.currentRing = null;
        playerInventory.currentBow = null;
        playerInventory.currentSpellbook = null;
        playerInventory.currentAmulet = null;
        playerInventory.currentBoots = null;
        playerInventory.currentLamp = null;

        playerInventory.totalDefense = 0;
        playerInventory.totalCritChance = 0;
        playerInventory.totalMaxSpellDamage = 0;
        playerInventory.totalMinSpellDamage = 0;
    }

    public void ResetVendorInventories()
    {
        foreach (var v in vendorInventories)
        {
            v.coins = 0;
            v.items = new Schwer.ItemSystem.Inventory();

            // Would vendors ever have gear?
            v.currentItem = null;
            v.currentWeapon = null;
            v.currentArmor = null;
            v.currentHelmet = null;
            v.currentGloves = null;
            v.currentLegs = null;
            v.currentShield = null;
            v.currentRing = null;
            v.currentBow = null;
            v.currentSpellbook = null;
            v.currentAmulet = null;
            v.currentBoots = null;
            v.currentLamp = null;

            // Would vendors ever need this?
            v.totalDefense = 0;
            v.totalCritChance = 0;
            v.totalMaxSpellDamage = 0;
            v.totalMinSpellDamage = 0;
        }
    }

    public void ResetXP() => _xpSystem.ResetExperienceSystem();
}
