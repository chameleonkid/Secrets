using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory = default;
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI descriptionText = default;
    [SerializeField] private GameObject closeButton = default;
    [Header("Stat Displays")]
    [SerializeField] private TextMeshProUGUI critDisplay = default;
    [SerializeField] private TextMeshProUGUI dmgDisplay = default;
    [SerializeField] private TextMeshProUGUI defDisplay = default;
    [SerializeField] private TextMeshProUGUI spellDisplay = default;
    [SerializeField] private TextMeshProUGUI rangeDisplay = default;

    public InventoryItem currentItem;   //! What is the purpose of this?

    private InventoryDisplay itemDisplay;

    private void Awake()
    {
        itemDisplay = GetComponentInChildren<InventoryDisplay>(true);
        itemDisplay.OnSlotSelected = SetUpItemDescription;
        itemDisplay.OnSlotUsed = OnItemUsed;
        itemDisplay.SubscribeToEquipmentSlotSelected(SetUpItemDescription);
    }

    private void OnEnable() => Refresh();

    private void Refresh()
    {
        descriptionText.text = "";
        itemDisplay.UpdateDisplay();
        UpdateStatDisplays();
    }

    private void SetUpItemDescription(InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = (newItem != null) ? newItem.fullDescription : "";
    }

    private void OnItemUsed(InventoryItem item)
    {
        if (item == null || !item.usable || item.numberHeld <= 0) return;

        item.Use();
        if (item.numberHeld <= 0)
        {
            inventory.contents.Remove(item);
            EventSystem.current.SetSelectedGameObject(closeButton);
            Refresh();
        }

        var context = (item is EquippableItem) ? " was equipped" : " was used";
        descriptionText.text = item.itemName + context;
    }

    private void UpdateStatDisplays()
    {
        dmgDisplay.text = DamageDisplayText();
        defDisplay.text = DefenseDisplayText();
        critDisplay.text = CritDisplayText();
        spellDisplay.text = SpellDamageDisplayText();
        rangeDisplay.text = RangeDamageDisplayText();
    }

    private string DamageDisplayText() => (inventory.currentWeapon) ?
        inventory.currentWeapon.minDamage + " - " + inventory.currentWeapon.maxDamage : "" ;

    private string CritDisplayText() => (inventory.totalCritChance > 0) ?
        inventory.totalCritChance + "%" : "";

    private string DefenseDisplayText() => (inventory.totalDefense > 0) ? inventory.totalDefense.ToString() : "";

    private string RangeDamageDisplayText() => (inventory.currentBow) ?
        inventory.currentBow.minDamage + " - " + inventory.currentBow.maxDamage : "";

    private string SpellDamageDisplayText() => (inventory.currentSpellbook || inventory.currentAmulet) ?
        inventory.totalMinSpellDamage + " - "  + inventory.totalMaxSpellDamage : "";
}
