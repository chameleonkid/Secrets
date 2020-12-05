using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTBAT : MonoBehaviour



{
   // public Collider2D triggerCollider;
    public Rigidbody2D batRigidbody;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    public void OnTriggerEnter2D(Collider2D other)
    {   
        if(batRigidbody.gravityScale > 0 && other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            Debug.Log("was hit by player");         
            Debug.Log("shot down by");
            Debug.Log(other);
            batRigidbody.gravityScale = -1;

        }
        else if (batRigidbody.gravityScale <= 0 && other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            Debug.Log("Was hit by player");
            Debug.Log("shot up by");
            Debug.Log(other);
            batRigidbody.gravityScale = 1;

        }
    }

}
