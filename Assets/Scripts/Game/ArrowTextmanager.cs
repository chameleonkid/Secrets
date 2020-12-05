using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowTextmanager : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI ArrowDisplay;
    public InventoryItem arrow;

    void Start()
    {
        UpdateArrowCount();
    }



    public void UpdateArrowCount()
    {
        if (playerInventory.myInventory.Find(x => x.itemName.Contains("Arrow")))
        {
            ArrowDisplay.text = "" + playerInventory.myInventory.Find(x => x.itemName.Contains("Arrow")).numberHeld;
        }
    }
}
