using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHitAoE : MonoBehaviour
{
    [SerializeField] private GameObject warning = default;
    [SerializeField] private GameObject areaOfEffect = default;
    [SerializeField] private float warningDuration = default;
    [SerializeField] private float effectDuration = default;


    public void Awake()
    {
        StartRotation();
    }

    public void StartRotation()
    {
        StartWarning();
        Invoke("StartAoe", this.warningDuration);
    }

    public void StartWarning()
    {
        if(warning)
        {
            warning.SetActive(true);
        }
    }

    public void StopWarning()
    {
        if (warning)
        {
            warning.SetActive(false);
        }
    }

    public void StartAoe()
    {
        StopWarning();
        areaOfEffect.SetActive(true);
        Destroy(this.gameObject,effectDuration);
    }

}
