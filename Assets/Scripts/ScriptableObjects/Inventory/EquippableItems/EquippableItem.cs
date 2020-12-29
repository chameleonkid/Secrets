public abstract class EquippableItem : ItemOld
{
    public void SwapEquipment() => myInventory.Equip(this);
}
