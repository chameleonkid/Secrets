using UnityEngine;

public class VendorInventoryManager : MonoBehaviour
{
    public Inventory vendorInventory;
    public GameObject blankInventorySlot;
    public GameObject inventoryPanel;

    private void OnEnable() => setUp();

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
                        // newSlot.SetupVendor(vendorInventory.contents[i], this);
                    }
                }
            }
        }
    }

    public void setUp()
    {
        MakeInventorySlots();
    }
}
