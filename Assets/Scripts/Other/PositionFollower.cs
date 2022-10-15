using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    public Transform target;


    private void Update()
    {
        if (target != null)
        {
            this.transform.position = target.position;
        }
    }
}

