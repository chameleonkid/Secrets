using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPWindow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI experienceNeededText;
    [SerializeField] private Image experienceBar;
    [SerializeField] private XPSystem levelSystem;
    [SerializeField] private Button addXPButton;
    // Start is called before the first frame update
    void Awake()
    {
        currentLevelText = transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>();
        experienceNeededText.text = ("" + levelSystem.GetExperience() + " / " + levelSystem.GetExperienceNeeded());
        experienceNeededText = transform.Find("XPNeededText").GetComponent<TextMeshProUGUI>();
        experienceBar = transform.Find("XPBar").GetComponent<Image>();
        addXPButton = transform.Find("AddXPButton").GetComponent<Button>();

    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBar.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        currentLevelText.text = ("Level " + levelNumber);
    }

    private void SetExperienceNeededText()
    {
        experienceNeededText.text = ("" + levelSystem.GetExperience() + " / " + levelSystem.GetExperienceNeeded());
    }

    public void SetLevelSystem(XPSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }




    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {  
        SetLevelNumber(levelSystem.GetLevelNumber());
        levelSystem.SetExperienceToNextLevel();
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
        SetExperienceNeededText();
    }
}
