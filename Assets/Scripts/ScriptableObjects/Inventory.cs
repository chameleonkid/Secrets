using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;
    public int arrows;

    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if(itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

    public bool checkForItem(Item item)
    {
        if (items.Contains(item))
        {
        return true;
        }
        return false;
    }

/*############################# TEST ########################################
    public void RemoveItem(Item itemToDrop)
    {

            if (!items.Contains(itemToDrop))
            {
                items.Remove(itemToDrop);
            }
        
    }
 //############################# TEST ########################################*/

}
