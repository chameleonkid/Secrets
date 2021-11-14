using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{


    public LootTable thisLoot;
    private Animator anim;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float lootGenerateDelay;
    [SerializeField] private Collider2D thisCollider;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private bool destroyableByAny;
    [SerializeField] private InventoryWeapon.WeaponType destroyableBy;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        thisCollider.enabled = true;
    }

    public void Smash()
    {
        if (destroyableByAny || playerInventory.currentWeapon.weaponType == destroyableBy)
        {
            anim.SetBool("Smash", true);
            StartCoroutine(breakCo());
        }

    }

    IEnumerator breakCo()
    {
        thisCollider.enabled = false;
        SoundManager.RequestSound(breakSound);
        yield return new WaitForSeconds(lootGenerateDelay);
        thisLoot?.GenerateLoot(transform.position);
        yield return new WaitForSeconds(destroyDelay);
        this.gameObject.SetActive(false);
    }
}
