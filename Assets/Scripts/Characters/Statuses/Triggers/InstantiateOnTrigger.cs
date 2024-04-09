using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnTrigger : MonoBehaviour
{
    public GameObject instantiationObject;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>())
        {
            Instantiate(instantiationObject, other.transform.position, Quaternion.identity);
        }
    }
}
