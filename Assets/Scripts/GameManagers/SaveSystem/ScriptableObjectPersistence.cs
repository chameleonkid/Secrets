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
    [SerializeField] private Inventory _playerInventory = default;
    public Inventory playerInventory => _playerInventory;

    [SerializeField] private BoolValue[] _chests = default;
    public BoolValue[] chests => _chests;
    [SerializeField] private BoolValue[] _doors = default;
    public BoolValue[] doors => _doors;
    [SerializeField] private XPSystem _xpSystem = default;
    public XPSystem xpSystem => _xpSystem;

    [Header("UI Updating")]
    public Signals coinSignal;

    public void ResetScriptableObjects()
    {
        ResetPlayer();
        ResetInventory();
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
    }

    public void ResetInventory()
    {
        playerInventory.coins = 0;
        coinSignal.Raise();

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

        playerInventory.totalDefense = 0;
        playerInventory.totalCritChance = 0;
        playerInventory.totalMaxSpellDamage = 0;
        playerInventory.totalMinSpellDamage = 0;
    }

    public void ResetXP() => _xpSystem.ResetExperienceSystem();
}
