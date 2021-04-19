using UnityEngine;

public class CharacterAppearanceSetter : MonoBehaviour
{
    [SerializeField] private CharacterAppearance characterAppearance = default;

    [SerializeField] private SpriteRenderer hairRenderer = default;
    [SerializeField] private SpriteSkinRPC hairStyle = default;
    
    [SerializeField] private SpriteSkinRPC eyesSkin = default;
    [SerializeField] private SpriteSkinRPC bodySkin = default;
    [SerializeField] private SpriteSkinRPC armorSkin = default;

    private Color hairColor;

    private void Start() => SetAppearance();

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
}
