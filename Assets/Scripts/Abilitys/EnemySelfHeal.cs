using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelfHeal : MonoBehaviour
{
    [Header("Enemy Values")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [Header("Healing Values")]
    [SerializeField] private float selfHealAmount;
    [SerializeField] private float currentHealDelay = 1;
    [SerializeField] private float originalHealDelay = 1;
    [SerializeField] private float timeTillNextHeal;
    [SerializeField] private bool canHeal = false;
    [SerializeField] private float startHealPercent = 75;
    [SerializeField] private float fastHealPercent = 50;
    [SerializeField] private int xTimesFasterHealingValue = 4;


    void Awake()
    {
        originalHealDelay = currentHealDelay;
        
        maxHealth = GetComponent<Enemy>().GetMaxHealth();
    }


    void FixedUpdate()
    {
        currentHealth = GetComponent<Enemy>().health;
        var percentHeal = maxHealth / 100f;
        timeTillNextHeal -= Time.deltaTime;
        if (timeTillNextHeal <= 0)
        {
            canHeal = true;
            timeTillNextHeal = currentHealDelay;
        }


        if (currentHealth <= (percentHeal * startHealPercent))
        {
            if (this.currentHealth <= (percentHeal * fastHealPercent))
            {
                currentHealDelay = (originalHealDelay / xTimesFasterHealingValue);
            }
            else
            {
                currentHealDelay = originalHealDelay;
            }
            StartSelfHeal();
        }
    }

    private void StartSelfHeal()
    {
        StartCoroutine(SelfHealCo());
        canHeal = false;
    }

    IEnumerator SelfHealCo()
    {
        if (canHeal == true)
        {
            if (currentHealth < maxHealth)
            {
                if (currentHealth + selfHealAmount > maxHealth)
                {
                    GetComponent<Enemy>().health = maxHealth;
                }
                GetComponent<Enemy>().health += selfHealAmount;
                DamagePopUpManager.RequestHealPopUp(selfHealAmount, this.transform);
            }
            yield return new WaitForSeconds(0);
        }
        canHeal = false;
    }

}
