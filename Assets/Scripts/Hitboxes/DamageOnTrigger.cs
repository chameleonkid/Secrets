using UnityEngine;

public class DamageOnTrigger : Hitbox
{
    [SerializeField] private float _damage = 1;
    public float damage { get => _damage; set => _damage = value; }
    public bool isCritical { get; set; } = false;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        Debug.Log(other + "was hit by "+ this );
        if (hit != null)
        {
            if (other.GetComponent<Enemy>() && this.GetComponentInParent<PlayerMovement>())
            {
                isCritical = this.GetComponentInParent<PlayerMovement>().IsCriticalHit();
                damage = this.GetComponentInParent<PlayerMovement>().myInventory.currentWeapon.damage;
                var dmg = isCritical ? damage * 2 : damage;
                Debug.Log(this + " hit " + other + " for " + dmg + " and it was critical? " + isCritical);
                hit.health -= dmg; //this was hit.health -= (damage);  but i dont think this made sense???
                DamagePopUpManager.RequestDamagePopUp(dmg, isCritical, hit.transform);
            }
            if(other.GetComponent<PlayerMovement>())
            {
                var defense = other.GetComponent<PlayerMovement>().myInventory.totalDefense;
                var dmg = isCritical ? (damage * 2) -defense : damage - defense;
                if (dmg >= 0)
                {
                    Debug.Log(other + " hit " + this + " for " + dmg + " and it was critical? " + isCritical);
                    hit.health -= (dmg); //this was hit.health -= (damage);  but i dont think this made sense???
                    DamagePopUpManager.RequestDamagePopUp(dmg, isCritical, hit.transform);
                }
                else
                {
                    Debug.Log(other + " hit " + this + " for " + dmg + " and it was critical? " + isCritical);
                    Debug.Log("DMG was to low to pierce armor");
                    DamagePopUpManager.RequestDamagePopUp(0, isCritical, hit.transform);
                }
            }
            if (this.gameObject.CompareTag("spell"))
            {
                damage = this.GetComponentInParent<PlayerMovement>().myInventory.totalSpellDamage; //how can i get a reference to the playerInventory...
                var dmg = isCritical ? damage * 2 : damage;
                Debug.Log(this + " hit " + other + " for " + dmg + " and it was critical? " + isCritical);
                hit.health -= dmg; //this was hit.health -= (damage);  but i dont think this made sense???
                DamagePopUpManager.RequestDamagePopUp(dmg, isCritical, hit.transform);
            }
            if (this.gameObject.CompareTag("arrow"))
            {
                damage = this.GetComponentInParent<PlayerMovement>().myInventory.currentBow.damage; //how can i get a reference to the playerInventory...
                var dmg = isCritical ? damage * 2 : damage;
                Debug.Log(this + " hit " + other + " for " + dmg + " and it was critical? " + isCritical);
                hit.health -= dmg; //this was hit.health -= (damage);  but i dont think this made sense???
                DamagePopUpManager.RequestDamagePopUp(dmg, isCritical, hit.transform);
            }



        }
    }
}
