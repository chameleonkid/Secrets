using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownDisplayManager : MonoBehaviour
{

    [SerializeField] private Image meleeWeaponCooldown = default;
    [SerializeField] private Image meleeWeaponSprite = default;
    [SerializeField] private Inventory inventory = default;
    [SerializeField] private PlayerMovement player = default;
    [SerializeField] private Color originalColor= default;
    [SerializeField] private float fillingTime = default;

    private void OnEnable()
    {
        player.OnAttackTriggered += SetMeleeCoolDown;
        // inventoryDisplay.OnSlotUsed = SetCoolDownIcons();
        originalColor = meleeWeaponCooldown.color;
    }

    private void OnDisable()
    {
        player.OnAttackTriggered -= SetMeleeCoolDown;
        //   inventoryDisplay.OnSlotUsed = SetCoolDownIcons();
    }

    private void SetCoolDownIcons()
    {
        meleeWeaponSprite.sprite = inventory.currentWeapon.sprite;  // This needs an event when an Item is equippid (where can i find those?)
    }

    private void SetMeleeCoolDown()
    {
        meleeWeaponSprite.sprite = inventory.currentWeapon.sprite;  //This is for Testing only!
        meleeWeaponCooldown.color = new Color(255,0,0);
        StartCoroutine(CooldownCountDownCo(inventory.currentWeapon.swingTime));
    }

    private IEnumerator CooldownCountDownCo(float weaponCooldown)
    {
      
        fillingTime = weaponCooldown;
        while (fillingTime > 0)
        {
            fillingTime -= Time.deltaTime;
            meleeWeaponCooldown.fillAmount = (fillingTime) / weaponCooldown;
          //  meleeWeaponCooldown.color = new Color(255, 255 - meleeWeaponCooldown.fillAmount * 255, 255 - meleeWeaponCooldown.fillAmount * 255,255);   I wanted the Background to become more bright with each iteration, but actually its instantly white...
            yield return null;
        }
        meleeWeaponCooldown.fillAmount = 1;
        meleeWeaponCooldown.color = new Color (255,255,255,255); //Visual Indicator for "The CD is done" in form of an "Flash"
        yield return new WaitForSeconds(0.1f);
        meleeWeaponCooldown.color = originalColor; 
    }
}
