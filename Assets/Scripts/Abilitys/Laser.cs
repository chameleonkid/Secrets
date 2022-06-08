using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    
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
