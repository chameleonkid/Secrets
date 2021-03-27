using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepTrigger : MonoBehaviour
{

    [SerializeField] private FloatValue normalizedTimeOfDay = default;
    [SerializeField] private ConstrainedFloat playerHealth = default;
    [SerializeField] private ConstrainedFloat playerMana = default;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        normalizedTimeOfDay.value = 0.25f;
        playerHealth.current = playerHealth.max;
        playerMana.current = playerMana.max;
    }

}
