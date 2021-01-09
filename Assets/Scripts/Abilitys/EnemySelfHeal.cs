using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelfHeal : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float selfHealAmount;
    [SerializeField] private float healDelay = 1;
    [SerializeField] private float originalHealDelay = 1;
    [SerializeField] private float healDelaySeconds;
    [SerializeField] private bool canHeal = false;


    void Awake()
    {
        originalHealDelay = healDelay;
        
        maxHealth = GetComponent<Enemy>().GetMaxHealth();
    }


    void FixedUpdate()
    {
        currentHealth = GetComponent<Enemy>().health;
        var percentHeal = maxHealth / 100f;
        healDelaySeconds -= Time.deltaTime;
        if (healDelaySeconds <= 0)
        {
            canHeal = true;
            healDelaySeconds = healDelay;
        }


        if (currentHealth <= (percentHeal * 75))
        {
            if (this.currentHealth <= (percentHeal * 50))
            {
                healDelay = (originalHealDelay / 4);
            }
            else
            {
                healDelay = originalHealDelay;
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
