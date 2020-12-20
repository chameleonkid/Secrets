using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private bool shieldActive = false;




    public void triggerShield()
    {
        shieldActive = !shieldActive;
        shield.SetActive(shieldActive);
    }


}




