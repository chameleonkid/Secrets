using UnityEngine;

public class CharacterAppearanceSetter : MonoBehaviour
{
    [SerializeField] private CharacterAppearance characterAppearance = default;

    [SerializeField] private SpriteRenderer hairRenderer = default;
    [SerializeField] private SpriteSkinRPC hairStyle = default;

    [SerializeField] private SpriteSkinRPC eyesSkin = default;
    [SerializeField] private SpriteSkinRPC bodySkin = default;
    [SerializeField] private SpriteSkinRPC armorSkin = default;

    [SerializeField] private Inventory inventory;
    [SerializeField] private Texture2D defaultMaleArmorTexture;
    [SerializeField] private Texture2D defaultFemaleArmorTexture;

    [SerializeField] private bool isMale = false;

    private void Awake()
    {

        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned in CharacterAppearanceSetter at Start.");
            return;
        }

        // Ensure default texture is assigned
        if (defaultMaleArmorTexture == null || defaultFemaleArmorTexture  == null)
        {
            Debug.LogWarning("Default armor texture is not assigned. Make sure to assign it in the Inspector.");
        }
    }

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnEquipmentChanged += UpdateCharacterAppearance;
        }
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnEquipmentChanged -= UpdateCharacterAppearance;
        }
    }


    private void SetAppearance()
    {
        if (characterAppearance.eyeColor)
        {
            eyesSkin.folderPath = characterAppearance.eyeFolderPath;
            eyesSkin.newSprite = characterAppearance.eyeColor;
            eyesSkin.ResetRenderer();
        }

        if (characterAppearance.hairStyle)
        {
            hairStyle.folderPath = characterAppearance.hairFolderPath;
            hairStyle.newSprite = characterAppearance.hairStyle;
            hairRenderer.color = characterAppearance.hairColor;
            hairStyle.ResetRenderer();
        }

        if (characterAppearance.bodyStyle)
        {
            bodySkin.folderPath = characterAppearance.bodyFolderPath;
            bodySkin.newSprite = characterAppearance.bodyStyle;
            bodySkin.ResetRenderer();
        }

        if (characterAppearance.armorStyle)
        {
            armorSkin.folderPath = characterAppearance.armorFolderPath;
            armorSkin.newSprite = characterAppearance.armorStyle;
            armorSkin.ResetRenderer();
        }
    }

    // Method to update appearance based on equipped items
    public void UpdateCharacterAppearance()
    {
        if (inventory.currentArmor != null)
        {
            if (armorSkin != null)
            {
                armorSkin.newSprite = inventory.currentArmor.texture;
                armorSkin.ResetRenderer();
            }
            else
            {
                Debug.LogError("armorSkin is not assigned in CharacterAppearanceSetter.");
            }
        }
        else
        {
            Debug.LogWarning("No armor is currently equipped.");
            ApplyDefaultArmorTexture();
        }
    }

    private void ApplyDefaultArmorTexture()
    {
        if (armorSkin != null)
        {
            if (isMale)
            {
                if (defaultMaleArmorTexture != null)
                {
                    armorSkin.newSprite = defaultMaleArmorTexture;
                    armorSkin.ResetRenderer();
                }
                else
                {
                    Debug.LogWarning("Default male armor texture is missing. Please assign it in the Inspector.");
                }
            }
            else
            {
                if (defaultFemaleArmorTexture != null)
                {
                    armorSkin.newSprite = defaultFemaleArmorTexture;
                    armorSkin.ResetRenderer();
                }
                else
                {
                    Debug.LogWarning("Default female armor texture is missing. Please assign it in the Inspector.");
                }
            }
        }
        else
        {
            Debug.LogError("armorSkin is not assigned in CharacterAppearanceSetter.");
        }
    }
}
    // You can add similar blocks for other equipment types, like helmet, weapon, etc.
    // For example:
    // if (inventory.currentHelmet)
    // {
    //     helmetSkin.folderPath = inventory.currentHelmet.folderPath;
    //     helmetSkin.newSprite = inventory.currentHelmet.sprite;
    //     helmetSkin.ResetRenderer();
    // }
