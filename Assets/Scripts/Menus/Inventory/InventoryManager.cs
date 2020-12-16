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

    //InventoryStatsRefresh
    public CritValueTextManager critDisplay;
    public DamageValueTextManager dmgDisplay;
    public DefenseValueTextManager defDisplay;
    public SpellDamageValueTextManager spellDisplay;
    public RangeDamageValueTextManager rangeDisplay;

    public TextMeshProUGUI descriptionText;
    public Inventory playerInventory;
    public InventoryItem currentItem;   //! What is the purpose of this?

    private GameObject closeButton;

    private void Awake() => closeButton = GameObject.Find("CloseButton");

    private void OnEnable() => SetUp();

    public void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.contents.Count; i++)
            {
                if (playerInventory.contents[i].numberHeld > 0) //bottle can be replaced with items that can hold 0 charges
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform, false);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.SetItem(playerInventory.contents[i]);
                        newSlot.OnSlotSelected += SetUpItemDescription; // Does unsubscribing need to be handled if the item slots are destroyed?
                        newSlot.OnSlotUsed += OnItemUsed;     // Does unsubscribing need to be handled if the item slots are destroyed?
                    }
                }
            }
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

    private void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    private void SetUpItemDescription(InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = (newItem != null) ? newItem.fullDescription : "";
    }

    private void OnItemUsed(InventoryItem usedItem)
    {
        descriptionText.text = usedItem.name + " was used";

        if (usedItem.numberHeld <= 0)
        {
            playerInventory.contents.Remove(usedItem);
            EventSystem.current.SetSelectedGameObject(closeButton);
        }
    }

    private void SetUp()
    {
        descriptionText.text = "";
        ClearInventorySlots();
        UpdateEquipmentSlots();
        MakeInventorySlots();
        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        dmgDisplay.UpdateDamageValue();
        defDisplay.UpdateDefenseValue();
        critDisplay.UpdateCritValue();
        spellDisplay.UpdateSpellDamageValue();
        rangeDisplay.UpdateRangeDamageValue();
    }
}
