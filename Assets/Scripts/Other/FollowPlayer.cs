using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : BoundedNPC
{
    //place this script on the player gameobject
    public override float health { get => 1; set { } }   //! Temp

    public GameObject player; // in the inspector drag the gameobject the will be following the player to this field
    public int followDistance;
    private List<Vector3> storedPositions;

    [SerializeField] private float followOffSetX;
    [SerializeField] private float followOffSetY;
    [SerializeField] private float FollowDelaySecondsMin=0;
    [SerializeField] private float FollowDelaySecondsMax=5;


    void Awake()
    {
        base.Awake();
        SetAnimatorXY(Vector2.down);
        storedPositions = new List<Vector3>(); //create a blank list

        if (!player)
        {
            Debug.Log("The FollowingMe gameobject was not set");
        }

        if (followDistance == 0)
        {
            Debug.Log("Please set distance higher then 0");
        }
    }
    void FixedUpdate()
    {
        if (storedPositions.Count == 0)
        {
            Debug.Log("blank list");
            storedPositions.Add(player.transform.position); //store the players currect position
            return;
        }
        else if (storedPositions[storedPositions.Count - 1] != player.transform.position)
        {
            //Debug.Log("Add to list");  
            storedPositions.Add(player.transform.position); //store the position every frame
        }

        if (storedPositions.Count > followDistance)
        {
            StartCoroutine(FollowCO());
        }
        else
        {
            animator.SetBool("isMoving", false);
            SetAnimatorXY(directionVector);
        }
    }

    private IEnumerator FollowCO()
    {
        var followDelayInSeconds = Random.Range(FollowDelaySecondsMin, FollowDelaySecondsMax);
        yield return new WaitForSeconds(followDelayInSeconds); //set to 0 maybe
        Vector3 position = new Vector3(storedPositions[0].x + followOffSetX, storedPositions[0].y + followOffSetY, storedPositions[0].z);
        transform.position = position; //move
        animator.SetBool("isMoving", true);
        SetAnimatorXY(transform.position);
        storedPositions.RemoveAt(0); //delete the position that player just moved to
    }

}

