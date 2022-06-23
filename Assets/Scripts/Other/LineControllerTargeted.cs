using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControllerTargeted : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Texture[] textures;

    [SerializeField] private int animationStep;
    [SerializeField] private float fps = 30f;
    [SerializeField] private float fpsCounter = 30f;

    private void Awake()
    {
        lineRenderer = GetComponent <LineRenderer>();
    }

    public void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        target = newTarget;
    }

    private void Update()
    {
        Debug.Log("In LineController Target is ", target);
        lineRenderer.SetPosition(1, target.position);

        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if(animationStep == textures.Length)
            {
                animationStep = 0;
            }
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0f;
        }
        if(target.GetComponent<PlayerMovement>())
        {
            target.GetComponent<PlayerMovement>().TakeDamage(10, false);
        }
        if (target.GetComponent<Enemy>())
        {
            target.GetComponent<Character>().TakeDamage(10, false);
        }

    }


}
