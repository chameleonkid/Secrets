using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StargateButtonMenu : MonoBehaviour
{
    public VectorValue playerPosMemory;
    public BoolValue[] starGateActivation = new BoolValue[10];

    [SerializeField] private Button currentlySelectedButton;

    // Public variables to reference the stargate buttons
    [SerializeField] private Button[] stargateButtons = new Button[10];
    [SerializeField] private Button startgateExitButton;
    [SerializeField] private GameObject startgatePanel;

    // Dictionary to map stargate names to scene names
    private Dictionary<string, string> stargateSceneMap = new Dictionary<string, string>();

    // Dictionary to map stargate names to scene names
    private Dictionary<string, Vector2> stargatePositionMap = new Dictionary<string, Vector2>();

    private void Start()
    {
        startgatePanel.SetActive(false);
        // Initialize the stargateSceneMap dictionary
        InitializeStargateSceneMap();
        InitializeStargatePostionMap();

        // Add listeners to each stargate button to handle button clicks
        ActivateButtons();

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
           Debug.Log("Player for Stargate found");
        }

        startgateExitButton.onClick.AddListener(() => ClosePanel());

        currentlySelectedButton = FindFirstActiveButton();
        if (currentlySelectedButton != null)
        {
            currentlySelectedButton.Select();
        }

    }

    private Button FindFirstActiveButton()
    {
        for (int i = 0; i < stargateButtons.Length; i++)
        {
            if (stargateButtons[i].interactable)
            {
                return stargateButtons[i];
            }
        }
        return null;
    }

    private void ClosePanel()
    {
        Debug.Log("Closing StargatePanel");
        Time.timeScale = 1f;
        startgatePanel.SetActive(false);
    }

    private void InitializeStargateSceneMap()
    {
        // Populate the dictionary with stargate names and their corresponding scene names
        stargateSceneMap.Add("Stargate1", "Overworld_Windcross");
        stargateSceneMap.Add("Stargate2", "Overworld_Forest1");
        stargateSceneMap.Add("Stargate3", "OpenSea");
        stargateSceneMap.Add("Stargate4", "Overworld_Beach");
        stargateSceneMap.Add("Stargate5", "Overworld_Windcross");
        stargateSceneMap.Add("Stargate6", "Overworld_Forest1");
        stargateSceneMap.Add("Stargate7", "Overworld_Windcross");
        stargateSceneMap.Add("Stargate8", "Overworld_Forest1");
        stargateSceneMap.Add("Stargate9", "Overworld_Windcross");
        stargateSceneMap.Add("Stargate10", "Overworld_Forest1");
        // Add mappings for all stargates...
    }
    private void InitializeStargatePostionMap()
    {
        // Populate the dictionary with stargate names and their corresponding positions
        stargatePositionMap.Add("Stargate1", new Vector2(0, 0));
        stargatePositionMap.Add("Stargate2", new Vector2(1, 1));
        stargatePositionMap.Add("Stargate3", new Vector2(2, 2));
        stargatePositionMap.Add("Stargate4", new Vector2(3, 3));
        stargatePositionMap.Add("Stargate5", new Vector2(4, 4));
        stargatePositionMap.Add("Stargate6", new Vector2(5, 5));
        stargatePositionMap.Add("Stargate7", new Vector2(6, 6));
        stargatePositionMap.Add("Stargate8", new Vector2(7, 7));
        stargatePositionMap.Add("Stargate9", new Vector2(8, 8));
        stargatePositionMap.Add("Stargate10", new Vector2(9, 9));
        // Add mappings for all stargates...
    }

    public void ActivateButtons()
    {
        for (int i = 0; i < stargateButtons.Length; i++)
        {
            //check if Stargate is active
            bool isActivated = starGateActivation[i].RuntimeValue;
            stargateButtons[i].interactable = isActivated;
            string stargateName = "Stargate" + (i + 1); // Stargate names start from "Stargate1"
            stargateButtons[i].onClick.AddListener(() => TeleportToStargate(stargateName));
        }
    }


    private void TeleportToStargate(string stargateName)
    {
        Time.timeScale = 1f;
        // Check if the stargate name exists in the dictionary
        if (stargateSceneMap.ContainsKey(stargateName))
        {
            if (stargatePositionMap.ContainsKey(stargateName))
            {
                string sceneName = stargateSceneMap[stargateName];
                Debug.Log("Loading scene: " + sceneName);
                playerPosMemory.value = stargatePositionMap[stargateName];
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("No Vector 2 Position was defined");
            }

        }
        else
        {
            Debug.LogError("Stargate name " + stargateName + " is not mapped to a scene.");
        }
    }
}