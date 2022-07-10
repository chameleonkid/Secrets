using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampWitchBoss : TurretEnemy
{
    [Header("AbilityValues Boss")]
    public bool canTeleport = false;
    public float canTeleportTimer;
    [SerializeField] private float canTeleportCD;

    [Header("Teleport Positions")]
    [SerializeField] private List<Vector3> TeleportPositionList = default;

    [Header("TeleportPointHolder")]
    [SerializeField] private Transform points;

    [Header("Boss Attack Sounds")]
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private AudioClip teleportSound;

    [Header("Waterball ability")]
    [SerializeField] private float canThrowWaterCD;
    public float canThrowWaterTimer;
    public bool canThrowWater;
    [SerializeField] private float WaterYPosition;

    protected override void Update()
    {
        //############## TELEPORT SPELL #######################
        canTeleportCD -= Time.deltaTime;
        if (canTeleportCD <= 0)
        {
            canTeleport = true;
            canTeleportCD = canTeleportTimer;
            setFireDelaySeconds(5);
        }

        if(canTeleport == true)
        {
            animator.SetTrigger("canTeleport");
            canTeleport = false;
        }
        //############## Water-Projectile Spell #######################
        //###################### Throw Boulder Ability ######################################
        canThrowWaterCD -= Time.deltaTime;
        if (canThrowWaterCD <= 0)
        {
            canThrowWater = true;
            canThrowWaterCD = canThrowWaterTimer;
        }
        if (canThrowWater == true)
        {
            animator.SetTrigger("canThrowWater");
            canThrowWater = false;
            //FireProjectile(); -------> Set in the animation!!!
        }

    }

    protected override void Awake()
    {
        base.Awake();
        TeleportPositionList = new List<Vector3>();
        foreach (Transform teleportPoint in points)
        {
            TeleportPositionList.Add(teleportPoint.position);
        }
        
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentStateEnum = StateEnum.waiting;
    }

    protected override void InsideChaseRadiusUpdate()
    {

    }

    protected virtual void TeleportSwampWitch()
    { 
        Vector3 rndTeleportPoint = TeleportPositionList[Random.Range(0, TeleportPositionList.Count)];
        this.transform.position = rndTeleportPoint;
    }


    public void HalfCooldownSpellTwo()
    {
        canTeleportTimer = canTeleportTimer / 2;
    }

    public void PlayAttackSound()
    {
        SoundManager.RequestSound(attackSound.GetRandomElement());
    }

    public void PlayTeleportSound()
    {
        SoundManager.RequestSound(teleportSound);
    }
}
