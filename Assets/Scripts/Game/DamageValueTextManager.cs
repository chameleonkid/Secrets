using UnityEngine;
using TMPro;

public class DamageValueTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI damageDisplay;

    private void Start()
    {
        UpdateDamageValue();
    }

    public void UpdateDamageValue()
    {
        if (playerInventory.currentWeapon)
        {
            damageDisplay.text = playerInventory.currentWeapon.minDamage + " - " + playerInventory.currentWeapon.maxDamage;
        }
        else
        {
            damageDisplay.text = "";
        }
    }

    private void OnEnable()
    {
        UpdateDamageValue();
    }
}
