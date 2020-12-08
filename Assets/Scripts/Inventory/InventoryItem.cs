using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;


    [SerializeField] private int _numberHeld = default;
    public int numberHeld {
        get => _numberHeld;
        set {
            _numberHeld = value;
            if (value < 0) {
                _numberHeld = 0;
            }
        }
    }

    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;
    public int itemLvl;
    public PlayerInventory myInventory;

    public void Use()
    {
        thisEvent.Invoke();
    }

    public void decreaseAmount(int amountToDecrease)
    {
        numberHeld -= amountToDecrease;
    }




}
