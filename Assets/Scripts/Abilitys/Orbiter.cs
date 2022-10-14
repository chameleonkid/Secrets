using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// put this script on the orbiting object prefab
public class Orbiter : MonoBehaviour
{

    public void UpdateOrbiter(float baseAngle, int index, int total)
    {
        float currentAngle = baseAngle + (index / (total - 1.0f) * 2 * Mathf.PI);
        float x = Mathf.Cos(currentAngle);
        float y = Mathf.Sin(currentAngle);
        transform.localPosition = new Vector3(x, y, 0);
    }

}