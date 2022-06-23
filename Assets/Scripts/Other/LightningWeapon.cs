using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeapon : MonoBehaviour
{
    [SerializeField] private List<Transform> targetedEnemies;
    [SerializeField] private LineControllerTargeted linePrefab;
    [SerializeField] private bool canHitPlayer;
    [SerializeField] private bool canHitEnemy;
    [SerializeField] private Dictionary<Transform, LineControllerTargeted> enemiesBeingLasered;


    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Enemy>() && canHitEnemy)
        {
            LineControllerTargeted newLine = Instantiate(linePrefab);

            newLine.AssignTarget(this.transform.position, other.transform);
            enemiesBeingLasered.Add(other.transform, newLine);
        }
        if (other.GetComponent<PlayerMovement>() && canHitPlayer)
        {
            LineControllerTargeted newLine = Instantiate(linePrefab);
            newLine.AssignTarget(this.transform.position, other.transform);
            enemiesBeingLasered.Add(other.transform, newLine);
            Debug.Log(enemiesBeingLasered);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(enemiesBeingLasered.ContainsKey(other.transform))
        {
            Debug.Log("Something left the range of weapon");
        }
    }


}
