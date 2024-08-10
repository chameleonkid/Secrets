using UnityEngine;
using UnityEngine.UI;

public class HairColorPicker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider redSlider = default;
    [SerializeField] private Slider greenSlider = default;
    [SerializeField] private Slider blueSlider = default;
    [SerializeField] private Image hairpreview1 = default;
    [SerializeField] private Image hairpreview2 = default;
    [SerializeField] private Image hairpreview3 = default;


    [SerializeField] private CharacterCreation characterCreation = default; // Reference to the CharacterCreation script

    private void Start()
    {
        // Set up listeners for the sliders
        redSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });

        // Initialize color preview to current hair color
        Color initialColor = characterCreation.hairColorRenderer.color;
        redSlider.value = initialColor.r;
        greenSlider.value = initialColor.g;
        blueSlider.value = initialColor.b;
        UpdateColorPreview();
    }

    // Update the preview image and the selected hair color
    public void UpdateColorPreview(float value = 0)
    {
        Color selectedColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        characterCreation.hairColorRenderer.color = selectedColor;
        hairpreview1.color = selectedColor;
        hairpreview2.color = selectedColor;
        hairpreview3.color = selectedColor;
    }

    // Method to close the color picker panel and confirm the selection
    public void ConfirmColorSelection()
    {
        Color selectedColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        characterCreation.characterAppearance.hairColor = selectedColor;
    }
}