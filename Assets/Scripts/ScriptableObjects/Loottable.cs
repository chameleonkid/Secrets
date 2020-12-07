using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
 // public PowerUps thisLoot;
    public float lootChance;
    public PhysicalInventoryItem thisItem;
}

[CreateAssetMenu]
public class Loottable : ScriptableObject
{

    public Loot[] loots;


    public PhysicalInventoryItem LootPowerUp()
    {       
        float cumProb = 0;
        float currentProb = Random.Range(0, 100);
        
        for(int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if (currentProb <= cumProb)
            {
                return loots[i].thisItem;               
            }
        }
        return null;
    }
  
}
