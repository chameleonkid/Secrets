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

    private void Awake() => SetAppearance();

    private void SetAppearance()
    {
        if (characterAppearance.eyeColor)
        {
            eyesSkin.newSprite = characterAppearance.eyeColor;
            eyesSkin.ResetRenderer();
        }

        if (characterAppearance.hairStyle)
        {
            hairStyle.newSprite = characterAppearance.hairStyle;
            hairRenderer.color = characterAppearance.hairColor;
            hairStyle.ResetRenderer();
        }

        if (characterAppearance.bodyStyle)
        {
            bodySkin.newSprite = characterAppearance.bodyStyle;
            bodySkin.ResetRenderer();
        }

        if (characterAppearance.armorStyle)
        {
            armorSkin.newSprite = characterAppearance.armorStyle;
            armorSkin.ResetRenderer();
        }
    }
}
