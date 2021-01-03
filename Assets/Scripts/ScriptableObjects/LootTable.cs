using UnityEngine;

[System.Serializable]
public class Loot
{
    public PickUp drop;
    public float dropChance;
}

[CreateAssetMenu(menuName = "Scriptable Object/Loot Table")]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public void GenerateLoot(Vector3 position) => GenerateLoot(position, Quaternion.identity);
    public void GenerateLoot(Vector3 position, Quaternion rotation)
    {
        var loot = RollLootTable();
        if (loot != null)
        {
            Instantiate(loot.gameObject, position, rotation);
        }
    }

    private PickUp RollLootTable()
    {
        float cumProb = 0;
        float currentProb = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].dropChance;
            if (currentProb <= cumProb)
            {
                return loots[i].drop;
            }
        }
        return null;
    }
}
