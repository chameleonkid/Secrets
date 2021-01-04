using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory = default;
    [Header("Components")]
    [SerializeField] private GameObject inventoryPanel = default;
    [SerializeField] private TextMeshProUGUI descriptionText = default;
    [SerializeField] private GameObject closeButton = default;
    [Header("Stat Displays")]
    [SerializeField] private TextMeshProUGUI critDisplay = default;
    [SerializeField] private TextMeshProUGUI dmgDisplay = default;
    [SerializeField] private TextMeshProUGUI defDisplay = default;
    [SerializeField] private TextMeshProUGUI spellDisplay = default;
    [SerializeField] private TextMeshProUGUI rangeDisplay = default;
    [SerializeField] private TextMeshProUGUI lightRadiusDisplay = default;
    [SerializeField] private Material swordMaterial = default;
    [Header("Cooldown Displays")]



    public Item currentItem;   //! What is the purpose of this?

    private InventoryDisplay inventoryDisplay;

    private void Awake()
    {
        inventoryDisplay = GetComponentInChildren<InventoryDisplay>(true);
        inventoryDisplay.OnSlotSelected = SetUpItemDescription;
        inventoryDisplay.OnSlotUsed = OnItemUsed;
        inventoryDisplay.SubscribeToEquipmentSlotSelected(SetUpItemDescription);
    }

    public void ClosePanel()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void OpenPanel()
    {
        descriptionText.text = "";
        UpdateStatDisplays();

        inventoryPanel.SetActive(true);
        Time.timeScale = 0;

        if (closeButton)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeButton);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && CanvasManager.Instance.IsFreeOrActive(inventoryPanel.gameObject))
        {
            if (inventoryPanel.activeInHierarchy)
            {
                ClosePanel();
            }
            else
            {
                OpenPanel();
            }
        }
    }

    private void SetUpItemDescription(Item newItem)
    {
        currentItem = newItem;
        descriptionText.text = (newItem != null) ? newItem.fullDescription : "";
    }

    private void OnItemUsed(Item item)
    {
        if (item == null || !item.usable || inventory.items[item] <= 0) return;

        item.Use();

        if (item is EquippableItem)
        {
            inventory.Equip((EquippableItem)item);
            setWeaponColor();

        }

        if (item.usable)
        {
            inventory.items[item]--;
        }
        
        if (inventory.items[item] <= 0)
        {
            EventSystem.current.SetSelectedGameObject(closeButton);
        }

        inventoryDisplay.UpdateEquipmentSlots();
        UpdateStatDisplays();
        var context = (item is EquippableItem) ? " was equipped" : " was used";
        descriptionText.text = item.name + context;
    }

    private void UpdateStatDisplays()
    {
        dmgDisplay.text = DamageDisplayText();
        defDisplay.text = DefenseDisplayText();
        critDisplay.text = CritDisplayText();
        spellDisplay.text = SpellDamageDisplayText();
        rangeDisplay.text = RangeDamageDisplayText();
        lightRadiusDisplay.text = LightRadiusDisplayText();
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

    private string LightRadiusDisplayText() => (inventory.currentLamp) ? "" + inventory.currentLamp.outerRadius : "";

    private void setWeaponColor()
    {
        swordMaterial.SetColor("_GlowColor", inventory.currentWeapon.glowColor);
    }

}
