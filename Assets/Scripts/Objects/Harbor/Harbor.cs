using UnityEngine.SceneManagement;
using UnityEngine;

public class Harbor : ComponentTrigger<ShipMovement>
{
    protected override bool? needOtherIsTrigger => true;

    [SerializeField] private AudioClip harborSound;
    [SerializeField] private ShipMovement myShip;
    [SerializeField] private VectorValue playerPos;

    [Header("This is the World that will be loaded")]
    [SerializeField] private string sceneToLoad;
    [Header("This is the position the player will be")]
    [SerializeField] private Vector2 harborPlayerPos;

    protected override void OnEnter(ShipMovement other)
    {
        myShip = other;

        Debug.Log("isInHarborRange");
        if (harborSound)
        {
            SoundManager.RequestSound(harborSound); //When you enter the area to land, the bell rings
            Debug.Log("ENTER HARBOR");
        }

        // I want this to happen on Shipmovement Interact
        playerPos.value = harborPlayerPos;
        SceneManager.LoadScene(sceneToLoad);
    }
}
