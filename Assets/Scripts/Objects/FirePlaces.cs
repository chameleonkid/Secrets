using UnityEngine;

public class FirePlaces : Interactable
{
    public GameObject firePlace;

    private void Update()
    {
        if (playerInRange && player.inputInteract)
        {
            firePlace.SetActive(!firePlace.activeInHierarchy);
        }
    }
}
