using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Vector3 playerChange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() && !other.isTrigger)
        {
            other.GetComponent<PlayerMovement>().LockMovement(2);
            other.transform.position = playerChange;
        }
    }

}
