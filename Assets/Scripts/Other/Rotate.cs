using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private float rotZ;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool clockWise = true;
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private float deactivationAngle;
    [SerializeField] private float deactivationAngle2;


    void Update()
    {
        if(rotationSpeed > 0)
        {
            if (clockWise == false)
            {
                rotZ += Time.deltaTime * rotationSpeed;
            }
            else
            {
                rotZ += -Time.deltaTime * rotationSpeed;
            }

            this.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        if(objectToDeactivate)
        {
            if (this.transform.rotation.eulerAngles.z < deactivationAngle || this.transform.rotation.eulerAngles.z > deactivationAngle2)
            {
                objectToDeactivate.SetActive(false);
            }
            else
            {
                objectToDeactivate.SetActive(true);
            }
        }


    }
}
