using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAndBarziersParticlesOn : MonoBehaviour
{
    [SerializeField] private BoolValue optionTorchAndBrazier;
    [SerializeField] private GameObject fireParticleSystem;

    // Update is called once per frame
    void Update()
    {
        if(optionTorchAndBrazier.RuntimeValue == true)
        {
            fireParticleSystem.SetActive(true);
        }
        else
        {
            fireParticleSystem.SetActive(false);
        }
    }
}
