using UnityEngine;
using System.Collections.Generic;

public class FollowPlayer : Character
{
    public override float health { get => 1; set {} }   //! Temp

    [Header("Follow")]
    public GameObject player; // in the inspector drag the gameobject the will be following the player to this field

    [SerializeField] private float followOffSetX;
    [SerializeField] private float followOffSetY;
    [SerializeField] private float FollowDelaySecondsMin=0;
    [SerializeField] private float FollowDelaySecondsMax=5;

    private List<Vector3> storedPositions = new List<Vector3>();
    private float delayCountdown;
    private bool movingLastFrame;

    protected override void Awake()
    {
        base.Awake();

        delayCountdown = Random.Range(FollowDelaySecondsMin, FollowDelaySecondsMax);
    }

    private void Update()
    {
        // Store target's current position each frame
        storedPositions.Add(player.transform.position);
        
        // Mirrors previous delay implementation — followers will snap to target position after delay
        if (delayCountdown > 0) {
            delayCountdown -= Time.deltaTime;
        }
        else if (storedPositions.Count > 0) {
            var position = storedPositions[0];
            position.x += followOffSetX;
            position.y += followOffSetY;

            SetAnimatorXY(position - transform.position);

            if (transform.position != position) {
                transform.position = position;
                animator.SetBool("isMoving", true);
                movingLastFrame = true;
            }
            else {
                // Need to delay by a frame to prevent oscillating while moving
                animator.SetBool("isMoving", movingLastFrame);
                movingLastFrame = false;
            }

            storedPositions.RemoveAt(0);
        }
    }
}
