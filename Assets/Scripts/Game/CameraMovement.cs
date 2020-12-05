using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("PostionVariables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [Header("Animator")]
    public Animator anim;

    [Header("PositionReset")]
    public VectorValue camMin;
    public VectorValue camMax;

    // Start is called before the first frame update
    void Start()
    {
        maxPosition = camMax.initialValue;
        minPosition = camMin.initialValue;
        anim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    
    void LateUpdate()
    {
    if(transform.position != target.position)
        {
            Vector3 targetposition = new Vector3(target.position.x, target.position.y, transform.position.z);
            //Maximale und Minimale Vectoren für die Kamera
              targetposition.x = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
              targetposition.y = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetposition, smoothing);


        }
    }

    public void BeginKick()
    {
        anim.SetBool("KickActive", true);
        StartCoroutine(KickCo());
    }
    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("KickActive", false);
    }

}
