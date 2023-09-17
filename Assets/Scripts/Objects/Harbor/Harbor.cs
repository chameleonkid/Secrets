using UnityEngine;
using UnityEngine.SceneManagement;

public class Harbor : ComponentTrigger<ShipMovement>
{
    protected override bool? needOtherIsTrigger => true;

    [SerializeField] private AudioClip enterHarborSound;
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
            if (enterHarborSound)
            {
                SoundManager.RequestSound(enterHarborSound);
            }
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    protected override void OnExit(ShipMovement ship)
    {
        this.ship = null;
    }
}
