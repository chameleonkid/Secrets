using System.Collections;
using Schwer.States;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : ComponentTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => false;

    public Vector3 playerChange;
    public bool needText;
    public string AreaName;
    public GameObject IGAreaText;
    public Text AreaText;

    protected override void OnEnter(PlayerMovement player)
    {
        player.currentState = new Locked(player, 2);
        player.transform.position += playerChange;
        if (needText)
        {
            StartCoroutine(PlaceNameCo());
        }
    }

    private IEnumerator PlaceNameCo()
    {
        IGAreaText.SetActive(true);
        AreaText.text = AreaName;
        yield return new WaitForSeconds(4f);
        IGAreaText.SetActive(false);
    }
}
