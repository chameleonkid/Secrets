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
    //InventoryStatsRefresh
    public CritValueTextManager critDisplay;
    public DamageValueTextManager dmgDisplay;
    public DefenseValueTextManager defDisplay;
    public SpellDamageValueTextManager spellDisplay;

    public GameObject inventoryPanel;

    public TextMeshProUGUI descriptionText;
    public PlayerInventory playerInventory;
    public InventoryItem currentItem;

    public void SetTextAndButton(string Description)
    {
        descriptionText.text = Description;
    }

    public void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i].numberHeld > 0) //bottle can be replaced with items that can hold 0 charges
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform, false);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this);
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
    }

    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, InventoryItem NewItem)
    {
        //  currentItem = NewItem;
        //  descriptionText.text = newDescriptionString;

        //################### TESTING #################################################

        if (NewItem is InventoryArmor)
        {
            InventoryArmor currentItem = NewItem as InventoryArmor;
            descriptionText.text = newDescriptionString + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (NewItem is InventoryWeapon)
        {
            InventoryWeapon currentItem = NewItem as InventoryWeapon;
            descriptionText.text = newDescriptionString + ("\n\n DMG: ") + currentItem.minDamage + " - " + currentItem.maxDamage;
        }
        else if (NewItem is InventoryHelmet)
        {
            InventoryHelmet currentItem = NewItem as InventoryHelmet;
            descriptionText.text = newDescriptionString + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (NewItem is InventoryGlove)
        {
            InventoryGlove currentItem = NewItem as InventoryGlove;
            descriptionText.text = newDescriptionString + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (NewItem is InventoryLegs)
        {
            InventoryLegs currentItem = NewItem as InventoryLegs;
            descriptionText.text = newDescriptionString + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (NewItem is InventoryShield)
        {
            InventoryShield currentItem = NewItem as InventoryShield;
            descriptionText.text = newDescriptionString + ("\n\n ARMOR: ") + currentItem.armorDefense;
        }
        else if (NewItem is InventoryRing)
        {
            InventoryRing currentItem = NewItem as InventoryRing;
            descriptionText.text = newDescriptionString + ("\n\n CRITICAL STRIKE CHANCE: ") + currentItem.criticalStrikeChance + ("%");
        }
        else if (NewItem is InventoryBow)
        {
            InventoryBow currentItem = NewItem as InventoryBow;
            descriptionText.text = newDescriptionString + ("\n\n DMG: ") + currentItem.damage;
        }
        else if (NewItem is InventorySpellbook)
        {
            InventorySpellbook currentItem = NewItem as InventorySpellbook;
            descriptionText.text = newDescriptionString + ("\n\n DMG: ") + currentItem.SpellDamage;
        }
        else
        {
            currentItem = NewItem;
            descriptionText.text = newDescriptionString;
        }
    }

    public void setUp()
    {
        clearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("");
        MakeGearSlots();
        dmgDisplay.UpdateDamageValue();
        defDisplay.UpdateDefenseValue();
        critDisplay.UpdateCritValue();
        spellDisplay.UpdateSpellDamageValue();
    }

    void OnEnable()
    {
        setUp();
    }
}
