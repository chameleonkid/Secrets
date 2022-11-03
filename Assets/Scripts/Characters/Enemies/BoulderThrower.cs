using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderThrower : MonoBehaviour
{

    [Header("AbilityValues")]
    public GameObject projectile;
    public bool canAttack = false;
    public float fireDelay;
    [SerializeField] private Animator animator;
    [SerializeField] protected int amountOfProjectiles = 1;
    [SerializeField] protected float timeBetweenProjectiles = 1;
    [SerializeField] protected float fireDelaySeconds;

    [Header("Direction X and Y for Projectile")]
    [SerializeField] private float boulderXCorrection;
    [SerializeField] private float boulderYCorrection;

    [Header("Sound that is played while the thrower appears")]
    [SerializeField] private AudioClip appearSound;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canAttack = true;
            fireDelaySeconds = fireDelay;
        }
        if(canAttack == true)
        {
            //ThrowBoulder(); This is done via Animation
            animator.SetTrigger("ThrowBoulder");
            canAttack = false;
        }
    }

    private void ThrowBoulder()
    {
        StartCoroutine(ThrowBoulderCO());
    }


    protected virtual IEnumerator ThrowBoulderCO()
    {

        for (int i = 0; i <= amountOfProjectiles - 1; i++)
        {
            var direction = new Vector3(boulderXCorrection,boulderYCorrection);
            Projectile.Instantiate(projectile, transform.position, direction, Quaternion.identity, "enemy");
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }

    }
    void PlayAppearSound()
        {
        SoundManager.RequestSound(appearSound);
        }


}
