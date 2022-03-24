using UnityEngine.SceneManagement;
using UnityEngine;

public class Harbor : ComponentTrigger<ShipMovement>
{
    protected override bool? needOtherIsTrigger => true;

    [SerializeField] private AudioClip harborSound;
    [SerializeField] private VectorValue playerPos;

    [Header("This is the World that will be loaded")]
    [SerializeField] private string sceneToLoad;
    [Header("This is the position the player will be")]
    [SerializeField] private Vector2 harborPlayerPos;

    private ShipMovement ship;

    private void Update()
    {
        if (ship != null && ship.inputInteract)
        {
            playerPos.value = harborPlayerPos;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    protected override void OnEnter(ShipMovement ship)
    {
        this.ship = ship;

        Debug.Log("isInHarborRange");
        if (harborSound)
        {
            SoundManager.RequestSound(harborSound); //When you enter the area to land, the bell rings
            Debug.Log("ENTER HARBOR");
        }
    }

    protected override void OnExit(ShipMovement ship)
    {
        this.ship = null;
    }
}
