using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private SpriteSkinRPC bodyChanger = default;
    public Texture2D[] bodySkins = default;
    private int bodyCounter = 0;

    [Header("Hair")]
    [SerializeField] private SpriteSkinRPC hairChanger = default;
    public Texture2D[] hairStyles = default;
    private int hairCounter = 0;
    public SpriteRenderer hairColorRenderer;
    Color[] hairColor = { new Color(0, 1, 0, 1), new Color(1, 0, 0, 1), new Color(1, 1, 1, 1), new Color(0, 0, 1, 1), new Color(1, 1, 0, 1) };
    private int hairColorCounter = 0;

    [Header("Armor")]
    [SerializeField] private SpriteSkinRPC armorChanger = default;
    public Texture2D[] armorSkins = default;
    private int armorCounter = 0;

    [Header("Eyes")]
    [SerializeField] private SpriteSkinRPC eyeChanger = default;
    public Texture2D[] eyeSkins = default;
    private int eyeCounter = 0;

    [SerializeField] private CharacterAppearance characterAppearance = default;

    private void Awake()
    {
        bodyChanger.newSprite = bodySkins[0];
        bodyChanger.ResetRenderer();

        hairChanger.newSprite = hairStyles[0];
        hairChanger.ResetRenderer();

        armorChanger.newSprite = armorSkins[0];
        armorChanger.ResetRenderer();

        eyeChanger.newSprite = eyeSkins[0];
        eyeChanger.ResetRenderer();
    }

    public void NextBody()
    {
        bodyCounter++;
        if (bodyCounter >= bodySkins.Length)
        {
            bodyCounter = 0;
        }

        bodyChanger.newSprite = bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void PreviousBody()
    {
        bodyCounter--;
        if (bodyCounter <= 0)
        {
            bodyCounter = bodySkins.Length - 1;
        }

        bodyChanger.newSprite = bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void NextHair()
    {
        hairCounter++;
        if (hairCounter >= hairStyles.Length )
        {
            hairCounter = 0;
        }

        hairChanger.newSprite = hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHair()
    {
        hairCounter--;
        if (hairCounter <= 0)
        {
            hairCounter = hairStyles.Length -1;
        }

        hairChanger.newSprite = hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void NextArmor()
    {
        armorCounter++;
        if (armorCounter >= armorSkins.Length)
        {
            armorCounter = 0;
        }

        armorChanger.newSprite = armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void PreviousArmor()
    {
        armorCounter--;
        if (armorCounter <= 0)
        {
            armorCounter = armorSkins.Length -1;
        }

        armorChanger.newSprite = armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }


    public void NextEyes()
    {
        eyeCounter++;
        if (eyeCounter >= eyeSkins.Length)
        {
            eyeCounter = 0;
        }

        eyeChanger.newSprite = eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void PreviousEyes()
    {
        eyeCounter--;
        if (eyeCounter <= 0)
        {
            eyeCounter = eyeSkins.Length -1;
        }

        eyeChanger.newSprite = eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void NextHairColor()
    {
        hairColorCounter++;
        if (hairColorCounter >= hairColor.Length)
        {
            hairColorCounter = 0;
        }

        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHairColor()
    {
        hairColorCounter--;
        if (hairColorCounter <= 0)
        {
            hairColorCounter = hairColor.Length -1;
        }

        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void SaveAppearance()
    {
        characterAppearance.armorStyle = armorSkins[armorCounter];
        characterAppearance.eyeColor = eyeSkins[eyeCounter];
        characterAppearance.hairStyle = hairStyles[hairCounter];
        characterAppearance.hairColor = hairColor[hairColorCounter];
        characterAppearance.bodyStyle = bodySkins[bodyCounter];
    }



}
