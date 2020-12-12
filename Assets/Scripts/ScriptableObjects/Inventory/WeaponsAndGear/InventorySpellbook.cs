using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Spellbook")]
public class InventorySpellbook : InventoryItem
{
    public int SpellDamage;
    private InventorySpellbook temp;
    public int manaCosts;
    public GameObject prefab;
    public float speed = 1;

    public void swapSpellbook()
    {
        if (myInventory.currentSpellbook)
        {
            temp = myInventory.currentSpellbook;
            myInventory.Add(myInventory.currentSpellbook);
            myInventory.equip(this);
            this.numberHeld--;
            if (itemSound)
            {
                SoundManager.RequestSound(itemSound);
            }
        }
        else
        {
            myInventory.equip(this);
            this.numberHeld--;
            if (itemSound)
            {
                SoundManager.RequestSound(itemSound);
            }
        }
    }
}

