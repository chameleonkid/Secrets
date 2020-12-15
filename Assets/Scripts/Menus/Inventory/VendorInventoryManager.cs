using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorInventoryManager : MonoBehaviour
{
    public Inventory vendorInventory;
    public GameObject blankInventorySlot;
    public GameObject inventoryPanel;

    public InventoryAmulet testAmu;
    public InventoryArmor testArmor;
    public InventoryWeapon testWeapon;


    private void OnEnable()
    {
        clearInventorySlots();
        setUp();
    }

    private void Awake()
    {
        vendorInventory.Add(testAmu);
        vendorInventory.Add(testArmor);
        vendorInventory.Add(testWeapon);
    }

    public void MakeInventorySlots()
    {
        if (vendorInventory)
        {
            for (int i = 0; i < vendorInventory.contents.Count; i++)
            {
                if (vendorInventory.contents[i].numberHeld > 0) //bottle can be replaced with items that can hold 0 charges
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform, false);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.SetupVendor(vendorInventory.contents[i], this);
                    }
                }
            }
        }

    }

    public void setUp()
    {
        MakeInventorySlots();
    }


    public void clearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)       //Clear MainInventory
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }




}
