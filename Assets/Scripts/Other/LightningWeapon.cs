using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWeapon : MonoBehaviour
{
    [SerializeField] private List<Transform> targetedEnemies;
    [SerializeField] private List<LineControllerTargeted> allLines;
    [SerializeField] private LineControllerTargeted linePrefab;
    [SerializeField] private bool canHitPlayer;
    [SerializeField] private bool canHitEnemy;


    private void Awake()
    {
        allLines = new List<LineControllerTargeted>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Enemy>() && canHitEnemy)
        {
            LineControllerTargeted newLine = Instantiate(linePrefab);
            allLines.Add(newLine);
            newLine.AssignTarget(this.transform.position, other.transform);
        }
        if (other.GetComponent<PlayerMovement>() && canHitPlayer)
        {
            LineControllerTargeted newLine = Instantiate(linePrefab);
            allLines.Add(newLine);
            newLine.AssignTarget(this.transform.position, other.transform);
        }
    }

}
