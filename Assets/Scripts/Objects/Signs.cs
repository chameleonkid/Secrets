using UnityEngine;
using UnityEngine.UI;

public class Signs : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    private void Update()
    {
        if (playerInRange && player.inputInteract)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    protected override void OnExit(PlayerMovement player)
    {
        base.OnExit(player);
        dialogBox.SetActive(false);
    }
}
