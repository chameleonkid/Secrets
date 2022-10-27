using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtNightActivator : MonoBehaviour
{

    [SerializeField] private FloatValue normalizedTimeOfDay = default;

    private void Update()
    {
        ActivateAtNight(normalizedTimeOfDay.value);
    }
    private void ActivateAtNight(float timeNormalized)
    {
       
            if (timeNormalized <= 0.23f || timeNormalized >= 0.85f)
            {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            }
            else
            {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}

