using UnityEngine;
using System;
using System.Collections;

public class Jason_Evans_Quest : ComponentTrigger<PlayerMovement>
{
    [Header("These Objects will be disabled")]
    [SerializeField] private GameObject[] objectsToBeDisabled;
    [Header("These Objects will be Spawned")]
    [SerializeField] private GameObject[] objectsToSpawn;
    [Header("In which radius of the trap will be spawned?:")]
    [SerializeField] private float spawnRadius;
    [Header("Bools to check if you talked and safe the state")]
    [SerializeField] private BoolValue talkedToJasonEvansSave;
    [SerializeField] private bool hasTalkedToJason = false;
    protected override bool? needOtherIsTrigger => true;
    private PlayerMovement player;

    [Header("The first time you talk to Jason enemies will spawn!")]
    [SerializeField] private Dialogue dialogueWithAmbush = default;
    [Header("After the initial Talk he justs asks if you checked the castle yet")]
    [SerializeField] private Dialogue dialogueWithoutAmbush = default;


    public static event Action OnJasonEvansTriggered;   // This event could make other script react on talking to Jason

    public void Awake()
    {
        hasTalkedToJason = talkedToJasonEvansSave.RuntimeValue;
        if (hasTalkedToJason)
        {
            if(objectsToBeDisabled.Length > 0)
            {
                DisableObjects();
            }
        }
    }

    private void LateUpdate()
    {
        if (player != null && player.inputInteract && Time.timeScale > 0)
        {
            Debug.Log("Interaction" + player.inputInteract);
            if(!hasTalkedToJason)
            {
                Debug.Log("1st time you talked to jason");
                hasTalkedToJason = true;
                talkedToJasonEvansSave.RuntimeValue = hasTalkedToJason;
                OnJasonEvansTriggered?.Invoke();
                DisableObjects();
                TriggerDialogue(dialogueWithAmbush);
                StartCoroutine(SpawnObjectsCo());
            }
            else
            {
                Debug.Log("2nd time you talked to jason");
                TriggerDialogue(dialogueWithoutAmbush);
            }

        }
    }

    public void TriggerDialogue(Dialogue dialogue) => DialogueManager.RequestDialogue(dialogue);

    protected override void OnEnter(PlayerMovement player)
    {
        this.player = player;
    }

    protected override void OnExit(PlayerMovement player)
    {
        DialogueManager.RequestEndDialogue();
        this.player = null;
    }

    public void DisableObjects()
    {
        for(int i=0; i < objectsToBeDisabled.Length; i++)
        {
            objectsToBeDisabled[i].SetActive(false);
        }
    }

    public IEnumerator SpawnObjectsCo()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            Vector2 homePos = this.transform.position;
            var randomPosition = homePos + UnityEngine.Random.insideUnitCircle * spawnRadius;
            var spawn = Instantiate(objectsToSpawn[i], randomPosition, Quaternion.identity);  //this.transform.position needs to vary slightly for iteration
        }
    }


}
