using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    [SerializeField] private float RotationSpeed;
    [SerializeField] private float distanceToObject;
    [SerializeField] private Transform target;


    private void Awake()
    {
        target = Object.FindObjectOfType<PlayerMovement>().transform;
        this.transform.position = new Vector3(target.transform.position.x + distanceToObject, 0, 0);
    }
    void FixedUpdate()
    {    
        transform.RotateAround(target.transform.position, Vector3.forward, RotationSpeed * Time.deltaTime);
    }
}
    

