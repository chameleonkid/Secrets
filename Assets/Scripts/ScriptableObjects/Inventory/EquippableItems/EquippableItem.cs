public abstract class EquippableItem : InventoryItem
{
    public void SwapEquipment() => myInventory.Equip(this);
}
