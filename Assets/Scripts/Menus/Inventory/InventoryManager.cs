using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    //Inventory Information
    public GameObject blankInventorySlot;
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
    //InventorySlot when Item not exists
    public Sprite weaponSlotSprite;
    public Sprite armorSlotSprite;
    public Sprite helmetSlotSprite;
    public Sprite gloveSlotSprite;
    public Sprite legsSlotSprite;
    public Sprite shieldSlotSprite;
    public Sprite ringSlotSprite;
    public Sprite bowSlotSprite;
    public Sprite spellbookSprite;
    public Sprite amuletSprite;
    //InventoryStatsRefresh
    public CritValueTextManager critDisplay;
    public DamageValueTextManager dmgDisplay;
    public DefenseValueTextManager defDisplay;
    public SpellDamageValueTextManager spellDisplay;
    public RangeDamageValueTextManager rangeDisplay;

    public GameObject inventoryPanel;

    public TextMeshProUGUI descriptionText;
    public Inventory playerInventory;
    public InventoryItem currentItem;

    private void OnEnable() => setUp();

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
                        newSlot.Setup(playerInventory.contents[i], this);
                    }
                }
            }
        }
    }
    //######################################### Create for GearSlots #######################################################################

    public void MakeGearSlots()
    {
        if (playerInventory.currentWeapon)
        {
            weaponSlot.thisItem = playerInventory.currentWeapon;
            weaponSlot.itemImage.sprite = playerInventory.currentWeapon.itemImage;
        }
        if (playerInventory.currentArmor)
        {
            armorSlot.thisItem = playerInventory.currentArmor;
            armorSlot.itemImage.sprite = playerInventory.currentArmor.itemImage;
        }
        if (playerInventory.currentHelmet)
        {
            helmetSlot.thisItem = playerInventory.currentHelmet;
            helmetSlot.itemImage.sprite = playerInventory.currentHelmet.itemImage;
        }
        if (playerInventory.currentGloves)
        {
            gloveSlot.thisItem = playerInventory.currentGloves;
            gloveSlot.itemImage.sprite = playerInventory.currentGloves.itemImage;
        }
        if (playerInventory.currentLegs)
        {
            legsSlot.thisItem = playerInventory.currentLegs;
            legsSlot.itemImage.sprite = playerInventory.currentLegs.itemImage;
        }
        if (playerInventory.currentShield)
        {
            shieldSlot.thisItem = playerInventory.currentShield;
            shieldSlot.itemImage.sprite = playerInventory.currentShield.itemImage;
        }
        if (playerInventory.currentRing)
        {
            ringSlot.thisItem = playerInventory.currentRing;
            ringSlot.itemImage.sprite = playerInventory.currentRing.itemImage;
        }
        if (playerInventory.currentBow)
        {
            bowSlot.thisItem = playerInventory.currentBow;
            bowSlot.itemImage.sprite = playerInventory.currentBow.itemImage;
        }
        if (playerInventory.currentSpellbook)
        {
            spellbookSlot.thisItem = playerInventory.currentSpellbook;
            spellbookSlot.itemImage.sprite = playerInventory.currentSpellbook.itemImage;
        }
        if (playerInventory.currentAmulet)
        {
            amuletSlot.thisItem = playerInventory.currentAmulet;
            amuletSlot.itemImage.sprite = playerInventory.currentAmulet.itemImage;
        }
    }

    //####################################### Clear Main-Slots ##################################################################################

    public void clearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)       //Clear MainInventory
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }

        if (!playerInventory.currentWeapon)
        {
            weaponSlot.itemImage.sprite = weaponSlotSprite;
        }
        if (!playerInventory.currentArmor)
        {
            armorSlot.itemImage.sprite = armorSlotSprite;
        }
        if (!playerInventory.currentHelmet)
        {
            helmetSlot.itemImage.sprite = helmetSlotSprite;
        }
        if (!playerInventory.currentGloves)
        {
            gloveSlot.itemImage.sprite = gloveSlotSprite;
        }
        if (!playerInventory.currentLegs)
        {
            legsSlot.itemImage.sprite = legsSlotSprite;
        }
        if (!playerInventory.currentShield)
        {
            shieldSlot.itemImage.sprite = shieldSlotSprite;
        }
        if (!playerInventory.currentRing)
        {
            ringSlot.itemImage.sprite = ringSlotSprite;
        }
        if (!playerInventory.currentBow)
        {
            bowSlot.itemImage.sprite = bowSlotSprite;
        }
        if (!playerInventory.currentSpellbook)
        {
            spellbookSlot.itemImage.sprite = spellbookSprite;
        }
        if (!playerInventory.currentAmulet)
        {
            amuletSlot.itemImage.sprite = amuletSprite;
        }
    }

    public void SetupDescriptionAndButton(InventoryItem newItem)
    {
        if (newItem is InventoryArmor)
        {
            InventoryArmor currentItem = newItem as InventoryArmor;
            descriptionText.text = newItem.itemDescription + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (newItem is InventoryWeapon)
        {
            InventoryWeapon currentItem = newItem as InventoryWeapon;
            descriptionText.text = newItem.itemDescription + ("\n\n DMG: ") + currentItem.minDamage + " - " + currentItem.maxDamage;
        }
        else if (newItem is InventoryHelmet)
        {
            InventoryHelmet currentItem = newItem as InventoryHelmet;
            descriptionText.text = newItem.itemDescription + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (newItem is InventoryGlove)
        {
            InventoryGlove currentItem = newItem as InventoryGlove;
            descriptionText.text = newItem.itemDescription + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (newItem is InventoryLegs)
        {
            InventoryLegs currentItem = newItem as InventoryLegs;
            descriptionText.text = newItem.itemDescription + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (newItem is InventoryShield)
        {
            InventoryShield currentItem = newItem as InventoryShield;
            descriptionText.text = newItem.itemDescription + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (newItem is InventoryRing)
        {
            InventoryRing currentItem = newItem as InventoryRing;
            descriptionText.text = newItem.itemDescription + ("\n\n CRITICAL STRIKE CHANCE: ") + currentItem.criticalStrikeChance + ("%");
        }
        else if (newItem is InventoryBow)
        {
            InventoryBow currentItem = newItem as InventoryBow;
            descriptionText.text = newItem.itemDescription + ("\n\n DMG: ") + currentItem.minDamage + " - " + currentItem.maxDamage;
        }
        else if (newItem is InventorySpellbook)
        {
            InventorySpellbook currentItem = newItem as InventorySpellbook;
            descriptionText.text = newItem.itemDescription + ("\n\n SPELL-DMG: ") + currentItem.minSpellDamage + " - " + currentItem.maxSpellDamage; ;
        }
        else if (newItem is InventoryAmulet)
        {
            InventoryAmulet currentItem = newItem as InventoryAmulet;
            descriptionText.text = newItem.itemDescription + ("\n\n SPELL-DMG: ") + currentItem.minSpellDamage + " - " + currentItem.maxSpellDamage; ;
        }
        else
        {
            currentItem = newItem;
            descriptionText.text = newItem.itemDescription;
        }
    }

    public void setUp()
    {
        clearInventorySlots();
        descriptionText.text = "";

        MakeInventorySlots();
        MakeGearSlots();

        dmgDisplay.UpdateDamageValue();
        defDisplay.UpdateDefenseValue();
        critDisplay.UpdateCritValue();
        spellDisplay.UpdateSpellDamageValue();
        rangeDisplay.UpdateRangeDamageValue();
    }
}
