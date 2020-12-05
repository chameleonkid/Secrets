using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

    //public Vector2 cameraChange;
  //  public Vector2 cameraMaxChange;
  //  public Vector2 cameraMinChange;
    public Vector3 playerChange;
   // private CameraMovement cam;
    public bool needText;
    public string AreaName;
    public GameObject IGAreaText;
    public Text AreaText;

    // Start is called before the first frame update
    void Start()
    {
        // cam = Camera.main.GetComponent<CameraMovement>();
    //    cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Teleport der Kamera und des Spielers bei Areawechsel
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
       //     cam.minPosition += cameraMinChange;
       //     cam.maxPosition += cameraMaxChange;
            other.transform.position += playerChange;
            if(needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }
    
    private IEnumerator placeNameCo()
    {
        IGAreaText.SetActive(true);
        AreaText.text = AreaName;
        yield return new WaitForSeconds(4f);
        IGAreaText.SetActive(false);
    }

}
