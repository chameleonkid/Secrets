using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAStuff : MonoBehaviour
{
    [SerializeField] private FloatValue normalizedTimeOfDay = default;
    [SerializeField] private ConstrainedFloat playerHealth;
    [SerializeField] private ConstrainedFloat playerMana;
    [SerializeField] private ConstrainedFloat playerLumen;

    public void FillAllStats()
    {
        playerHealth.current = playerHealth.max;
        playerMana.current = playerMana.max;
        playerLumen.current = playerLumen.max;
    }

    public void InsaneStats()
    {
        playerHealth.max = 50000f;
        playerMana.max = 50000f;
        playerLumen.max = 50000f;

        playerHealth.current = playerHealth.max;
        playerMana.current = playerMana.max;
        playerLumen.current = playerLumen.max;
    }

    public void WeakStats()
    {
        playerHealth.max = 100f;
        playerMana.max = 100f;
        playerLumen.max = 100f;

        playerHealth.current = playerHealth.max;
        playerMana.current = playerMana.max;
        playerLumen.current = playerLumen.max;
    }

    public void SetTimeTo6Am()
    {
        normalizedTimeOfDay.value = 0.25f;
    }

    public void SetTimeTo8pm()
    {
        normalizedTimeOfDay.value = 0.85f;
    }
}
