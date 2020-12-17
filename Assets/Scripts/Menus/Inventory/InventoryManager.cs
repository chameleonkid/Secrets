using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    //Inventory Information
    public GameObject blankInventorySlot;
    public GameObject inventoryPanel;
    //InventorySlots when Item exists
    public InventorySlot weaponSlot;
    public InventorySlot armorSlot;
    public InventorySlot helmetSlot;
    public InventorySlot gloveSlot;
    public InventorySlot legsSlot;
    public InventorySlot shieldSlot;
    public InventorySlot ringSlot;
    public InventorySlot bowSlot;
    public InventorySlot spellbookSlot;
    public InventorySlot amuletSlot;
    public InventorySlot bootsSlot;
    // Stats display 
    [SerializeField] private TextMeshProUGUI critDisplay = default;
    [SerializeField] private TextMeshProUGUI dmgDisplay = default;
    [SerializeField] private TextMeshProUGUI defDisplay = default;
    [SerializeField] private TextMeshProUGUI spellDisplay = default;
    [SerializeField] private TextMeshProUGUI rangeDisplay = default;

    public TextMeshProUGUI descriptionText;
    public Inventory playerInventory;
    public InventoryItem currentItem;   //! What is the purpose of this?

    private GameObject closeButton;

    private void Awake() {
        closeButton = GameObject.Find("CloseButton");
        SubscribeToEquipmentSlots();
    }

    private void OnEnable() => Refresh();

    private void Refresh()
    {
        descriptionText.text = "";
        ClearInventorySlots();
        UpdateEquipmentSlots();
        MakeInventorySlots();
        UpdateDisplays();
    }

    // Destroy all item slots
    private void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    private void UpdateEquipmentSlots()
    {
        weaponSlot.SetItem(playerInventory.currentWeapon);
        armorSlot.SetItem(playerInventory.currentArmor);
        helmetSlot.SetItem(playerInventory.currentHelmet);
        gloveSlot.SetItem(playerInventory.currentGloves);
        legsSlot.SetItem(playerInventory.currentLegs);
        shieldSlot.SetItem(playerInventory.currentShield);
        ringSlot.SetItem(playerInventory.currentRing);
        bowSlot.SetItem(playerInventory.currentBow);
        spellbookSlot.SetItem(playerInventory.currentSpellbook);
        amuletSlot.SetItem(playerInventory.currentAmulet);
        bootsSlot.SetItem(playerInventory.currentBoots);
    }

    private void SubscribeToEquipmentSlots()
    {
        weaponSlot.OnSlotSelected += SetUpItemDescription;
        armorSlot.OnSlotSelected += SetUpItemDescription;
        helmetSlot.OnSlotSelected += SetUpItemDescription;
        gloveSlot.OnSlotSelected += SetUpItemDescription;
        legsSlot.OnSlotSelected += SetUpItemDescription;
        shieldSlot.OnSlotSelected += SetUpItemDescription;
        ringSlot.OnSlotSelected += SetUpItemDescription;
        bowSlot.OnSlotSelected += SetUpItemDescription;
        spellbookSlot.OnSlotSelected += SetUpItemDescription;
        amuletSlot.OnSlotSelected += SetUpItemDescription;
        bootsSlot.OnSlotSelected += SetUpItemDescription;
    }

    // Instantiate inventory slots and set items
    public void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.contents.Count; i++)
            {
                if (playerInventory.contents[i].numberHeld > 0) //bottle can be replaced with items that can hold 0 charges
                {
                    var newSlotGameObj = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    newSlotGameObj.transform.SetParent(inventoryPanel.transform, false);
                    var newSlot = newSlotGameObj.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.SetItem(playerInventory.contents[i]);
                        newSlot.OnSlotSelected += SetUpItemDescription; // Does unsubscribing need to be handled if the item slots are destroyed?
                        newSlot.OnSlotUsed += OnItemUsed;               // Does unsubscribing need to be handled if the item slots are destroyed?
                    }
                }
            }
        }
    }

    private void SetUpItemDescription(InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = (newItem != null) ? newItem.fullDescription : "";
    }

    private void OnItemUsed(InventoryItem usedItem)
    {
        if (usedItem.numberHeld <= 0)
        {
            playerInventory.contents.Remove(usedItem);
            EventSystem.current.SetSelectedGameObject(closeButton);
            Refresh();
        }

        descriptionText.text = usedItem.name + " was used";
    }

    private void UpdateDisplays()
    {
        dmgDisplay.text = DamageDisplayText();
        defDisplay.text = DefenseDisplayText();
        critDisplay.text = CritDisplayText();
        spellDisplay.text = SpellDamageDisplayText();
        rangeDisplay.text = RangeDamageDisplayText();
    }

    private string DamageDisplayText() => (playerInventory.currentWeapon) ?
        playerInventory.currentWeapon.maxDamage + " - " + playerInventory.currentWeapon.maxDamage : "" ;

    private string CritDisplayText() => (playerInventory.totalCritChance > 0) ?
        playerInventory.totalCritChance + "%" : "";

    private string DefenseDisplayText() => (playerInventory.totalDefense > 0) ? playerInventory.totalDefense.ToString() : "";

    private string RangeDamageDisplayText() => (playerInventory.currentBow) ?
        playerInventory.currentBow.minDamage + " - " + playerInventory.currentBow.maxDamage : "";

    private string SpellDamageDisplayText() => (playerInventory.currentSpellbook || playerInventory.currentAmulet) ?
        playerInventory.totalMinSpellDamage + " - "  + playerInventory.totalMaxSpellDamage : "";
}
