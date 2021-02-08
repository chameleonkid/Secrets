using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    [Header("Gender")]
    [SerializeField] private bool isFemale = false;

    [Header("Skins")]
    [SerializeField] private SkinTexturesDatabase skinTextures = default;
    [SerializeField] private SkinTexturesDatabase skinTexturesFemale = default;
    [SerializeField] private SkinTexturesDatabase skinTexturesMale = default;

    [Header("Body")]
    [SerializeField] private SpriteSkinRPC bodyChanger = default;
    private int bodyCounter = 0;

    [Header("Hair")]
    [SerializeField] private SpriteSkinRPC hairChanger = default;
    private int hairCounter = 0;
    public SpriteRenderer hairColorRenderer;
    Color[] hairColor = { new Color(0.3f, 0.3f, 0.3f, 1), new Color(0, 1, 0, 1), new Color(1, 0, 0, 1), new Color(1, 1, 1, 1), new Color(0, 0, 1, 1), new Color(1, 1, 0, 1), new Color(1, 0, 1, 1) };
    private int hairColorCounter = 0;

    [Header("Armor")]
    [SerializeField] private SpriteSkinRPC armorChanger = default;
    private int armorCounter = 0;

    [Header("Eyes")]
    [SerializeField] private SpriteSkinRPC eyeChanger = default;
    private int eyeCounter = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip[] creatorMusic = default;
    [SerializeField] private AudioClip buttonClick = default;

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI characterName = default;

    [Header("Persistence")]
    [SerializeField] private CharacterAppearance characterAppearance = default;


    private void Start()
    {
        MusicManager.RequestMusic(creatorMusic);
    }
    private void Awake()
    {
        ResetTextures();
    }

    public void NextBody()
    {
        bodyCounter++;
        if (bodyCounter >= skinTextures.bodySkins.Length)
        {
            bodyCounter = 0;
        }

        SoundManager.RequestSound(buttonClick);
        bodyChanger.newSprite = skinTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void PreviousBody()
    {
        bodyCounter--;
        if (bodyCounter < 0)
        {
            bodyCounter = skinTextures.bodySkins.Length - 1;
        }
        SoundManager.RequestSound(buttonClick);
        bodyChanger.newSprite = skinTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void NextHair()
    {
        hairCounter++;
        if (hairCounter >= skinTextures.hairStyles.Length )
        {
            hairCounter = 0;
        }
        SoundManager.RequestSound(buttonClick);
        hairChanger.newSprite = skinTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHair()
    {
        hairCounter--;
        if (hairCounter < 0)
        {
            hairCounter = skinTextures.hairStyles.Length -1;
        }
        SoundManager.RequestSound(buttonClick);
        hairChanger.newSprite = skinTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void NextArmor()
    {
        armorCounter++;
        if (armorCounter >= skinTextures.armorSkins.Length)
        {
            armorCounter = 0;
        }
        SoundManager.RequestSound(buttonClick);
        armorChanger.newSprite = skinTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void PreviousArmor()
    {
        armorCounter--;
        if (armorCounter < 0)
        {
            armorCounter = skinTextures.armorSkins.Length -1;
        }
        SoundManager.RequestSound(buttonClick);
        armorChanger.newSprite = skinTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void NextEyes()
    {
        eyeCounter++;
        if (eyeCounter >= skinTextures.eyeSkins.Length)
        {
            eyeCounter = 0;
        }
        SoundManager.RequestSound(buttonClick);
        eyeChanger.newSprite = skinTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void PreviousEyes()
    {
        eyeCounter--;
        if (eyeCounter < 0)
        {
            eyeCounter = skinTextures.eyeSkins.Length -1;
        }
        SoundManager.RequestSound(buttonClick);
        eyeChanger.newSprite = skinTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void NextHairColor()
    {
        hairColorCounter++;
        if (hairColorCounter >= hairColor.Length)
        {
            hairColorCounter = 0;
        }
        SoundManager.RequestSound(buttonClick);
        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHairColor()
    {
        hairColorCounter--;
        if (hairColorCounter < 0)
        {
            hairColorCounter = hairColor.Length -1;
        }
        SoundManager.RequestSound(buttonClick);
        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void SaveAppearance()
    {
        characterAppearance.armorStyle = skinTextures.armorSkins[armorCounter];
        characterAppearance.eyeColor = skinTextures.eyeSkins[eyeCounter];
        characterAppearance.hairStyle = skinTextures.hairStyles[hairCounter];
        characterAppearance.hairColor = hairColor[hairColorCounter];
        characterAppearance.bodyStyle = skinTextures.bodySkins[bodyCounter];
        characterAppearance.playerName = characterName.text;

        characterAppearance.bodyIndex = bodyCounter;
        characterAppearance.hairIndex = hairCounter;
        characterAppearance.armorIndex = armorCounter;
        characterAppearance.eyeIndex = eyeCounter;

        characterAppearance.isFemale = isFemale;

        SoundManager.RequestSound(buttonClick);
        SceneManager.LoadScene("Mavens_Inn_Cutscene");
    }

    public void ResetTextures()
    {
        bodyChanger.newSprite = skinTextures.bodySkins[0];
        bodyChanger.ResetRenderer();

        hairChanger.newSprite = skinTextures.hairStyles[0];
        hairChanger.ResetRenderer();

        armorChanger.newSprite = skinTextures.armorSkins[0];
        armorChanger.ResetRenderer();

        eyeChanger.newSprite = skinTextures.eyeSkins[0];
        eyeChanger.ResetRenderer();
    }

    public void GenderToMale()
    {
        isFemale = false;
        skinTextures = skinTexturesMale;
        bodyChanger.folderPath = "Retro Pixel Characters/Spritesheets/Male/1 - Base/";
        hairChanger.folderPath = "Retro Pixel Characters/Spritesheets/Male/5 - Hairstyles/";
        armorChanger.folderPath = "Retro Pixel Characters/Spritesheets/Male/3 - Outfits/";
        eyeChanger.folderPath = "Retro Pixel Characters/Spritesheets/Male/2 - Eye Colors/";      
        ResetTextures();
    }

    public void GenderToFemale()
    {
        isFemale = true;
        skinTextures = skinTexturesFemale;
        bodyChanger.folderPath = "Retro Pixel Characters/Spritesheets/Female/1 - Base/";
        hairChanger.folderPath = "Retro Pixel Characters/Spritesheets/Female/5 - Hairstyles/";
        armorChanger.folderPath = "Retro Pixel Characters/Spritesheets/Female/3 - Outfits/";
        eyeChanger.folderPath = "Retro Pixel Characters/Spritesheets/Female/2 - Eye Colors/";
        ResetTextures();
    }
}
