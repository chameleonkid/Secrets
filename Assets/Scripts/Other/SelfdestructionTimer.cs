using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfdestructionTimer : MonoBehaviour
{

    [SerializeField] private float timer = default;

    private void Update()
    {
        Destroy(this.gameObject, timer);
    }

}


