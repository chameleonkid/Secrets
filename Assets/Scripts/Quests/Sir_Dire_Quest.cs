using UnityEngine;
using System;
using System.Collections;

public class Sir_Dire_Quest : ComponentTrigger<PlayerMovement>
{
    [Header("Parent Object to disable or enable")]
    [SerializeField] private GameObject SirDireGameObject;

    [Header("This Item will be given to your Inventory")]
    [SerializeField] private Item itemAfterYouKilledPumpkin;
    [Header("This Item will be given to your Inventory")]
    [SerializeField] private Item itemAfterYouLitFires;
    [Header("This Item will be given to your Inventory")]
    [SerializeField] private Item ItemAfterYouKilledFireKnight;
    [Header("The Inventory you give the item to. Should be Player!")]
    [SerializeField] private Inventory inventory;

    [Header("If the Pumpkin is Dead, spawn Sir Dire")]
    [SerializeField] private BoolValue PumpkinIsDead;

    [Header("Bools to check if you talked and safe the state")]
    [SerializeField] private BoolValue talkedToSirDirSave;
    [SerializeField] private bool hasTalkedToSirDire = false;

    [Header("Bools to check if you lit the fires and safe the state")]
    [SerializeField] private bool firesAreLit = false;
    [SerializeField] private BoolValue receivedFireItem;
    [SerializeField] private BoolValue firesAreLitSave1;
    [SerializeField] private BoolValue firesAreLitSave2;

    [Header("Bools to check if you killed Lord Darul")]
    [SerializeField] private bool killedLordDarul = false;
    [SerializeField] private BoolValue receivedDarulItem;
    [SerializeField] private BoolValue killedLordDarulSave;
    protected override bool? needOtherIsTrigger => true;
    private PlayerMovement player;

    [Header("Sir Dire thanks you and asks to you light the fires")]
    [SerializeField] private Dialogue firstDialouge = default;
    [Header("Sir Dire asks you to light the fires only")]
    [SerializeField] private Dialogue dialoguesFiresNotLit = default;
    [Header("Sir Dire asks you to find Lord Darul")]
    [SerializeField] private Dialogue dialoguesFindLordDarul = default;
    [Header("You finished the Questline")]
    [SerializeField] private Dialogue dialoguesFinished = default;


    public static event Action OnJasonEvansTriggered;   // This event could make other script react on talking to Jason

    public void Awake()
    {
        Campfire_Blue.OnFireLit += CheckForFiresLit;
        //Need to create static event when fires get lit, to check if fire was lit and set the bool

        if (PumpkinIsDead.RuntimeValue == true)
        {
            SirDireGameObject.SetActive(true);
        }
        else
        {
            SirDireGameObject.SetActive(false);
        }
        // This sets all the Savestates into local Bools
        hasTalkedToSirDire = talkedToSirDirSave.RuntimeValue;
        if(firesAreLitSave1.RuntimeValue && firesAreLitSave2.RuntimeValue)
        {
            firesAreLit = true;
        }
        killedLordDarul = killedLordDarulSave.RuntimeValue;

        // Only make the furthest progress true again
        if (firesAreLit)
        {
            hasTalkedToSirDire = false;
        }
        if (killedLordDarul)
        {
            firesAreLit = false;
        }

    }

    private void LateUpdate()
    {
        if (player != null && player.inputInteract && Time.timeScale > 0)
        {
            if (hasTalkedToSirDire)
            {
                Debug.Log("2nd time you talked to Sir Dire");
                TriggerDialogue(dialoguesFiresNotLit);
            }

            if (!hasTalkedToSirDire)
            {
                Debug.Log("1st time you talked to Sir Dire after Dungeon 1");
                hasTalkedToSirDire = true;
                talkedToSirDirSave.RuntimeValue = hasTalkedToSirDire;
                TriggerDialogue(firstDialouge);
                GiveItem(itemAfterYouKilledPumpkin);
            }

            if(firesAreLit)
            {
                if(receivedFireItem.RuntimeValue == false)
                {
                    Debug.Log("You lit the fires and need to find Lord Darul, also you get an Item");
                    receivedFireItem.RuntimeValue = true;
                    TriggerDialogue(dialoguesFindLordDarul);
                    GiveItem(itemAfterYouLitFires);
                }
                else
                {
                    Debug.Log("You lit the fires and need to find Lord Darul, NO Item");
                    TriggerDialogue(dialoguesFindLordDarul);
                }

            }
            if(killedLordDarul)
            {
                if (receivedDarulItem.RuntimeValue == false)
                {
                    receivedDarulItem.RuntimeValue = true;
                    Debug.Log("You killed the Fireknight and Sir Dire sends you into the swap, You get an Item");
                    TriggerDialogue(dialoguesFinished);
                    GiveItem(ItemAfterYouKilledFireKnight);
                }
                else
                {
                    Debug.Log("You killed the Fireknight and Sir Dire sends you into the swap, NO ITEM");
                    TriggerDialogue(dialoguesFinished);
                }

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

    public void GiveItem(Item item)
    {
        inventory.items[item]++;
    }

    public void CheckForFiresLit()
    {
        Debug.Log("Checking if both fires are lit");
        if (firesAreLitSave1.RuntimeValue && firesAreLitSave2.RuntimeValue)
        {
            firesAreLit = true;
        }
    }






}
