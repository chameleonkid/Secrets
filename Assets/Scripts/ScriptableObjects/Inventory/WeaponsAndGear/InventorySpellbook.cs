using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Spellbook")]
public class InventorySpellbook : InventoryItem
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public int manaCosts;
    public GameObject prefab;
    public float speed = 1;

    public void swapSpellbook()
    {
        if (myInventory.currentSpellbook)
        {
            // Return currently equipped spellbook to inventory
            myInventory.Add(myInventory.currentSpellbook);
        }

        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
