using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private float rotZ;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool clockWise = true;

    // Update is called once per frame
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

    }
}
