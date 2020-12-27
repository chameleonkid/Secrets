public abstract class EquippableItem : Item
{
    public void SwapEquipment() => myInventory.Equip(this);
}
