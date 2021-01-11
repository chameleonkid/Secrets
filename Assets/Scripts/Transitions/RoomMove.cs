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

    //Teleport der Kamera und des Spielers bei Areawechsel
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<PlayerMovement>().LockMovement(2);
            other.transform.position += playerChange;
            if (needText) 
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
