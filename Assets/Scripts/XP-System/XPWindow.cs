using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevelText = default;
    [SerializeField] private TextMeshProUGUI experienceNeededText = default;
    [SerializeField] private Image experienceBar = default;
    [SerializeField] private XPSystem levelSystem = default;

    private void OnEnable()
    {
        levelSystem.OnExperienceChanged += UpdateExperienceDisplay;
        levelSystem.OnLevelChanged += UpdateCurrentLevelText;

        UpdateCurrentLevelText();
        UpdateExperienceBarFill();
    }

    private void OnDisable()
    {
        levelSystem.OnExperienceChanged -= UpdateExperienceDisplay;
        levelSystem.OnLevelChanged -= UpdateCurrentLevelText;
    }

    private void UpdateExperienceDisplay()
    {
        UpdateExperienceBarFill();
        UpdateExperienceNeededText();
    }

    private void UpdateExperienceBarFill() => experienceBar.fillAmount = levelSystem.GetExperienceNormalized();

    private void UpdateCurrentLevelText() => currentLevelText.text = ("Level " + levelSystem.level);

    private void UpdateExperienceNeededText() => experienceNeededText.text = ("" + levelSystem.experience + " / " + levelSystem.experienceToNextLevel);
}
