using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public event Action OnNumberHeldChanged;

    public string itemName;
    public string itemDescription;
    public virtual string fullDescription => itemDescription;
    public int itemBuyPrice;
    public int itemSellPrice;

    public Sprite itemImage;
    public AudioClip itemSound = default;

    [SerializeField] private int _numberHeld = default;
    public int numberHeld {
        get => _numberHeld;
        set {
            if (value < 0) {
                value = 0;
            }

            if (_numberHeld != value) {
                _numberHeld = value;
                OnNumberHeldChanged?.Invoke();
            }
        }
    }

    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;
    public int itemLvl;
    public Inventory myInventory;

    public void Use() => thisEvent.Invoke();

    public void decreaseAmount(int amountToDecrease) => numberHeld -= amountToDecrease;
}
