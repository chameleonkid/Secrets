using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampWitchBoss : TurretEnemy
{
    [Header("AbilityValues Boss")]
    public GameObject[] projectileTwo;
    public bool canTeleport = false;
    public float canTeleportTimer;
    [SerializeField] private float canTeleportCD;

    [Header("Teleport Positions")]
    [SerializeField] private List<Vector3> TeleportPositionList = default;

    [Header("TeleportPointHolder")]
    [SerializeField] private Transform points;

    [Header("Boss Attack Sounds")]
    [SerializeField] private AudioClip attack1Sound;
    [SerializeField] private AudioClip attack2Sound;

    protected override void Update()
    {
        if (currentState is Schwer.States.Knockback) return;

        base.Update();

        canTeleportCD -= Time.deltaTime;
        if (canTeleportCD <= 0)
        {
            canTeleport = true;
            canTeleportCD = canTeleportTimer;
        }

        if(canTeleport == true)
        {
            TeleportSwampWitch();
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
        base.InsideChaseRadiusUpdate();

        if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
        {

            currentStateEnum = StateEnum.idle;
        }
    }

    protected virtual void TeleportSwampWitch()
    {
        StartCoroutine(TeleportCo());
        canTeleport = false;
        currentStateEnum = StateEnum.walk;
    }

    protected virtual IEnumerator TeleportCo()
    {
        var originalMovespeed = this.moveSpeed;
        animator.SetTrigger("canTeleport");
        // SoundManager.RequestSound(attackSounds.GetRandomElement());
        yield return new WaitForSeconds(1f);
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;

        Vector3 rndTeleportPoint = TeleportPositionList[Random.Range(0, TeleportPositionList.Count)];
        this.transform.position = rndTeleportPoint;
        /*
        int rndTeleportPosition = Random.Range(0, 100);
        if (rndTeleportPosition < 20)
        {
        this.transform.position = TeleportPositionList.
        }
        if (rndTeleportPosition > 20 && rndTeleportPosition <= 40)
        {
     
        }
        if (rndTeleportPosition > 40 && rndTeleportPosition <= 60)
        {

        }
        if (rndTeleportPosition > 60 && rndTeleportPosition <= 80)
        {

        }
        else
        {

        }
        */




    }

    public void HalfCooldownSpellTwo()
    {
        canTeleportTimer = canTeleportTimer / 2;
    }

    public void PlayAttack1Sound()
    {
        SoundManager.RequestSound(attack1Sound);
    }

    public void PlayAttack2Sound()
    {
        SoundManager.RequestSound(attack2Sound);
    }
}
