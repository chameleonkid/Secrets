using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] private float laserDamage = 5;
    [SerializeField] private Transform laserFirePoint;
    [SerializeField] private LineRenderer m_lineRenderer;
    [Header("Check this box to make the laser hit enemies as well")]
    [SerializeField] private bool canHitEnemies = false;
    [SerializeField] private Transform m_transform;
    
    // Start is called before the first frame update
    void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    void ShootLaser()
    {
        if( Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            Draw2DRay(laserFirePoint.position, _hit.point);
            if(_hit.collider.GetComponent<PlayerMovement>())
            {
                Debug.Log("Player was hit by Ray");
                _hit.collider.GetComponent<PlayerMovement>().TakeDamage(laserDamage, false);
            }
            if (canHitEnemies == true && _hit.collider.GetComponent<Enemy>())
            {
                Debug.Log("Enemy was hit by Ray");
                _hit.collider.GetComponent<Character>().TakeDamage(laserDamage, false);
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
              
    }

    void Draw2DRay(Vector2 startPos,Vector2 endPos )
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }
}
