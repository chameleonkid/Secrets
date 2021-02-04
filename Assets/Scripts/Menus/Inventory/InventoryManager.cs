using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
    [SerializeField] private Material laserMaterial = default;

    [Header("Experience System")]
    [SerializeField] private XPSystem xPSystem = default;

    [Header("Sounds")]
    [SerializeField] private AudioClip cantEquipSound = default;
    [SerializeField] private AudioClip selectItemSound = default;
    [SerializeField] private AudioClip unequipFailSound = default;

    public Item currentItem;   //! What is the purpose of this?

    private InventoryDisplay inventoryDisplay;

    private void OnEnable() => inventory.OnFailToUnequip += OnFailedUnequip;
    private void OnDisable() => inventory.OnFailToUnequip -= OnFailedUnequip;

    private void Awake()
    {
        inventoryDisplay = GetComponentInChildren<InventoryDisplay>(true);
        inventoryDisplay.OnSlotSelected = SetUpItemDescription;
        inventoryDisplay.OnSlotUsed = OnItemUsed;
        inventoryDisplay.SubscribeToEquipmentSlotSelected(SetUpItemDescription);
        inventoryDisplay.SubscribeToEquipmentSlotUsed(Unequip);
    }

    public void ClosePanel()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1;
        CanvasManager.Instance.RegisterClosedCanvas(inventoryPanel);
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
        SoundManager.RequestSound(selectItemSound);
        currentItem = newItem;
        descriptionText.text = (newItem != null) ? newItem.fullDescription : "";
    }

    private void OnItemUsed(Item item)
    {
        if (item == null || !item.usable || inventory.items[item] <= 0) return;

        item.Use();

        if (item is EquippableItem)
        {
            if (xPSystem.level >= item.level)
            {
                inventory.Equip((EquippableItem)item);
                if (inventory.currentWeapon)
                {
                    SetWeaponColor();
                }
                if (inventory.currentSpellbook || inventory.currentSpellbookTwo)
                {
                    SetLaserColor();
                }
                descriptionText.text = "You are now wearing " + item.name;
            }

        }

        if (item.usable)
        {
            if (xPSystem.level >= item.level)
            {
                inventory.items[item]--;
                descriptionText.text = "You used " + item.name;
                var context = (item is EquippableItem) ? "You are now wearing " : "You used ";
                descriptionText.text = context + item.name;
            }
            else
            {
                SoundManager.RequestSound(cantEquipSound);

                descriptionText.text = "Your level is not high enaugh to use this. \n Requires Level: " + item.level;
            }
        }

        if (inventory.items[item] <= 0)
        {
            EventSystem.current.SetSelectedGameObject(closeButton);
        }

        inventoryDisplay.UpdateEquipmentSlots();
        UpdateStatDisplays();
  
    }

    private void Unequip(Item itemToUnequip) => inventory.Unequip(itemToUnequip as EquippableItem);

    private void OnFailedUnequip(Item triedItem)
    {
        SoundManager.RequestSound(unequipFailSound);
        if (triedItem == null)
        {
            descriptionText.text = "Can't take off nothing...";
        }
        else
        {
            descriptionText.text = $"Not enough space to unequip {triedItem.name}...";
        }
    }

    private void UpdateStatDisplays()
    {
        dmgDisplay.text = DamageDisplayText();
        defDisplay.text = DefenseDisplayText();
        critDisplay.text = CritDisplayText();
        spellDisplay.text = SpellDamageDisplayText();
        // rangeDisplay.text = RangeDamageDisplayText();
        lightRadiusDisplay.text = LightRadiusDisplayText();
    }

    private string DamageDisplayText() => (inventory.currentWeapon) ?
        inventory.currentWeapon.minDamage + " - " + inventory.currentWeapon.maxDamage : "" ;

    private string CritDisplayText() => (inventory.totalCritChance > 0) ?
        inventory.totalCritChance + "%" : "";

    private string DefenseDisplayText() => (inventory.totalDefense > 0) ? inventory.totalDefense.ToString() : "";

    // private string RangeDamageDisplayText() => (inventory.currentBow) ?
    //     inventory.currentBow.minDamage + " - " + inventory.currentBow.maxDamage : "";

    private string SpellDamageDisplayText() => (inventory.currentSpellbook || inventory.currentSpellbookTwo || inventory.currentAmulet) ?
        inventory.totalMinSpellDamage + " - "  + inventory.totalMaxSpellDamage : "";

    private string LightRadiusDisplayText() => (inventory.currentLamp) ? "" + inventory.currentLamp.outerRadius : "";

    private void SetWeaponColor() => swordMaterial.SetColor("_GlowColor", inventory.currentWeapon.glowColor);
    private void SetLaserColor() => laserMaterial.SetColor("_GlowColor", inventory.currentSpellbook.glowColor);
}
