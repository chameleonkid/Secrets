using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrail : MonoBehaviour 
{
    public Vector3 m_RotationSpeed;

    private void Update()
    {
        transform.Rotate(m_RotationSpeed * Time.deltaTime);
    }
}
