using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector3 playerChange;
    public bool needText;
    public string AreaName;
    public GameObject IGAreaText;
    public Text AreaText;

    private void OnTriggerEnter2D(Collider2D other)
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
