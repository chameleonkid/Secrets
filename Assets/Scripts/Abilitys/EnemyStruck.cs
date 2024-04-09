using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStruck : MonoBehaviour
{

    [SerializeField] private float destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

}
