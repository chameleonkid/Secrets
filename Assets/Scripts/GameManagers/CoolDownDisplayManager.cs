using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownDisplayManager : MonoBehaviour
{
    [SerializeField] private Image meleeWeaponCooldown = default;
    [SerializeField] private Image meleeWeaponSprite = default;
    [SerializeField] private Image spell0Cooldown = default;
    [SerializeField] private Image spell0Sprite = default;
    [SerializeField] private Image spell1Cooldown = default;
    [SerializeField] private Image spell1Sprite = default;
    [SerializeField] private Image lampCooldown = default;
    [SerializeField] private Image lampSprite = default;
    [SerializeField] private Inventory inventory = default;
    [SerializeField] private PlayerMovement player = default;
    [SerializeField] private Color originalColor = default;
    // [SerializeField] private float fillingTime = default;

    private void OnEnable()
    {
        player.OnAttackTriggered += SetMeleeCoolDown;
        player.OnSpellTriggered += SetSpell0CoolDown;
        player.OnSpellTwoTriggered += SetSpell1CoolDown;
        player.OnLampTriggered += SetLampCoolDown;
        inventory.OnEquipmentChanged += ChangeCoolDownSprites;
        originalColor = meleeWeaponCooldown.color;
        ChangeCoolDownSprites();
    }

    private void OnDisable()
    {
        player.OnAttackTriggered -= SetMeleeCoolDown;
        player.OnSpellTriggered -= SetSpell0CoolDown;
        player.OnSpellTwoTriggered -= SetSpell0CoolDown;
        player.OnLampTriggered += SetLampCoolDown;
        inventory.OnEquipmentChanged -= ChangeCoolDownSprites;
    }

    private void ChangeCoolDownSprites()
    {
        if (inventory.currentWeapon)
        {
            meleeWeaponSprite.sprite = inventory.currentWeapon.sprite;
        }
        if (inventory.currentSpellbook)
        {
            spell0Sprite.sprite = inventory.currentSpellbook.sprite;
        }
        if (inventory.currentSpellbookTwo)
        {
            spell1Sprite.sprite = inventory.currentSpellbookTwo.sprite;
        }
        if (inventory.currentLamp)
        {
            lampSprite.sprite = inventory.currentLamp.sprite;
        }
    }

    private void SetCoolDownIcons() => meleeWeaponSprite.sprite = inventory.currentWeapon.sprite;

    private void SetMeleeCoolDown()
    {
        meleeWeaponCooldown.color = new Color(255, 0, 0);
        StartCoroutine(CooldownCountDownCo(inventory.currentWeapon.swingTime));
    }

    private void SetSpell0CoolDown()
    {
        spell0Cooldown.color = new Color(255, 0, 0);
        StartCoroutine(SpellCooldownCountDownCo(inventory.currentSpellbook.coolDown));
    }

    private void SetSpell1CoolDown()
    {
        spell1Cooldown.color = new Color(255, 0, 0);
        StartCoroutine(SpellTwoCooldownCountDownCo(inventory.currentSpellbookTwo.coolDown));
    }

    private void SetLampCoolDown()
    {
        if (lampCooldown.color == new Color(255,0,0))
        {
            lampCooldown.color = originalColor;
        }
        else
        {
            lampCooldown.color = new Color(255, 0, 0);
        }
    }

    private IEnumerator CooldownCountDownCo(float weaponCooldown)
    {
        float fillingTime = 0;
        fillingTime = weaponCooldown;
        while (fillingTime > 0)
        {
            fillingTime -= Time.deltaTime;
            meleeWeaponCooldown.fillAmount = (fillingTime) / weaponCooldown;
            // meleeWeaponCooldown.color = new Color(255, 255 - meleeWeaponCooldown.fillAmount * 255, 255 - meleeWeaponCooldown.fillAmount * 255,255);   I wanted the Background to become more bright with each iteration, but actually its instantly white...
            yield return null;
        }
        meleeWeaponCooldown.fillAmount = 1;
        meleeWeaponCooldown.color = new Color(255, 255, 255, 255); // Visual Indicator for "The CD is done" in form of an "Flash"
        yield return new WaitForSeconds(0.1f);
        meleeWeaponCooldown.color = originalColor;
    }

    private IEnumerator SpellCooldownCountDownCo(float spellBookCountDown)
    {
        float fillingTime = 0;
        fillingTime = spellBookCountDown;
        while (fillingTime > 0)
        {
            fillingTime -= Time.deltaTime;
            spell0Cooldown.fillAmount = (fillingTime) / spellBookCountDown;
            // meleeWeaponCooldown.color = new Color(255, 255 - meleeWeaponCooldown.fillAmount * 255, 255 - meleeWeaponCooldown.fillAmount * 255,255);   I wanted the Background to become more bright with each iteration, but actually its instantly white...
            yield return null;
        }
        spell0Cooldown.fillAmount = 1;
        spell0Cooldown.color = new Color(255, 255, 255, 255); // Visual Indicator for "The CD is done" in form of an "Flash"
        yield return new WaitForSeconds(0.1f);
        spell0Cooldown.color = originalColor;
    }

    private IEnumerator SpellTwoCooldownCountDownCo(float spellBookCountDown)
    {
        float fillingTime = 0;
        fillingTime = spellBookCountDown;
        while (fillingTime > 0)
        {
            fillingTime -= Time.deltaTime;
            spell1Cooldown.fillAmount = (fillingTime) / spellBookCountDown;
            // meleeWeaponCooldown.color = new Color(255, 255 - meleeWeaponCooldown.fillAmount * 255, 255 - meleeWeaponCooldown.fillAmount * 255,255);   I wanted the Background to become more bright with each iteration, but actually its instantly white...
            yield return null;
        }
        spell1Cooldown.fillAmount = 1;
        spell1Cooldown.color = new Color(255, 255, 255, 255); // Visual Indicator for "The CD is done" in form of an "Flash"
        yield return new WaitForSeconds(0.1f);
        spell1Cooldown.color = originalColor;
    }
}
