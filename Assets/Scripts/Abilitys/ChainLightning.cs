using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    [SerializeField] private CircleCollider2D coll;
    public float damage;
    public LayerMask enemyLayer;

    public GameObject chainLightningEffect;
    public GameObject beenStruck;
    public int amountToChain;

    public GameObject startObject;
    public GameObject endObject;

    [SerializeField] private Animator anim;
    [SerializeField] private int singleSpawns;

    public ParticleSystem parti;

    // Start is called before the first frame update
    void Start()
    {

        if(amountToChain <= 0)
        {
            Destroy(gameObject);
        }

        coll = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        parti = GetComponent<ParticleSystem>();

        startObject = gameObject;

        singleSpawns = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)) && !collision.GetComponentInChildren<EnemyStruck>())
        {
            if (singleSpawns != 0)
            {


                endObject = collision.gameObject;
                amountToChain -= 1;
                Instantiate(chainLightningEffect, collision.gameObject.transform.position, Quaternion.identity);
                Instantiate(beenStruck, collision.gameObject.transform);

                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, false);

                anim.StopPlayback();
                coll.enabled = false;

                singleSpawns--;
                
                parti.Play();
                var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = startObject.transform.position;
                parti.Emit(emitParams, 1);
                emitParams.position = endObject.transform.position;
                parti.Emit(emitParams, 1);
                emitParams.position = (startObject.transform.position + endObject.transform.position) / 2;
                parti.Emit(emitParams, 1);

                Destroy(gameObject, 1f);
            }

        }
    }
}
