using UnityEngine;
public class DestructableObject : MonoBehaviour
{

    [SerializeField] private float currentHealth;
    [SerializeField] private HealthbarBehaviour healthbar;
    [SerializeField] protected FloatValue maxHealth = default;

    private void Awake()
    {
        currentHealth = maxHealth.value;
        if (healthbar != null)
        {
            //healthbar.SetMaxHealth(maxHealth);
            healthbar.UpdateHealthBar(currentHealth, maxHealth.value);
            healthbar.gameObject.SetActive(false);  // Hide the health bar initially
        }
    }

    public void TakeDamage(float damage, bool isCritical)
    {
        currentHealth -= damage;

        if (healthbar != null)
        {
            healthbar.gameObject.SetActive(true);  // Show the health bar
            healthbar.UpdateHealthBar(currentHealth, maxHealth.value);  // Update health bar
        }

        if (currentHealth <= 0)
        {
            DestroyBlock();  // Destroy the ice block
        }
    }


    private void DestroyBlock()
    {
        // Play destroy effect, sound, or other logic
        Destroy(gameObject);  // Destroy the ice block
    }
}