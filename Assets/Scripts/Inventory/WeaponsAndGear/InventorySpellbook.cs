using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Spellbook")]
public class InventorySpellbook : InventoryItem
{
    public int SpellDamage;
    private InventorySpellbook temp;

    public void swapSpellbook()
    {
        if (myInventory.currentSpellbook)
        {
            temp = myInventory.currentSpellbook;
            myInventory.Add(myInventory.currentSpellbook);
            myInventory.equip(this);
            this.numberHeld--;
        }
        else
        {
            myInventory.equip(this);
            this.numberHeld--;
        }
    }
}

