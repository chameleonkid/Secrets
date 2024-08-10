using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
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

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI characterName = default;

    [Header("Persistence")]
    [SerializeField] private CharacterAppearance characterAppearance = default;

    [Header("Database")]
    [SerializeField] private int texturesCounter;
    [SerializeField] private ScriptableObjectPersistence so;
    [SerializeField] private SkinTexturesDatabase activeTextures => so.skinTexturesDatabases[texturesCounter];

    private void Start()
    {
        so = SaveManager.Instance.GetComponent<ScriptableObjectPersistence>();
        MusicManager.RequestMusic(creatorMusic);
        ResetTextures();
    }

    public void NextBody()
    {
        IncrementCounter(ref bodyCounter, activeTextures.bodySkins);
        bodyChanger.newSprite = activeTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void PreviousBody()
    {
        DecrementCounter(ref bodyCounter, activeTextures.bodySkins);
        bodyChanger.newSprite = activeTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void NextHair()
    {
        IncrementCounter(ref hairCounter, activeTextures.hairStyles);
        hairChanger.newSprite = activeTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHair()
    {
        DecrementCounter(ref hairCounter, activeTextures.hairStyles);
        hairChanger.newSprite = activeTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void NextArmor()
    {
        IncrementCounter(ref armorCounter, activeTextures.armorSkins);
        armorChanger.newSprite = activeTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void PreviousArmor()
    {
        DecrementCounter(ref armorCounter, activeTextures.armorSkins);
        armorChanger.newSprite = activeTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void NextEyes()
    {
        IncrementCounter(ref eyeCounter, activeTextures.eyeSkins);
        eyeChanger.newSprite = activeTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void PreviousEyes()
    {
        DecrementCounter(ref eyeCounter, activeTextures.eyeSkins);
        eyeChanger.newSprite = activeTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void NextHairColor()
    {
        IncrementCounter(ref hairColorCounter, hairColor);
        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHairColor()
    {
        DecrementCounter(ref hairColorCounter, hairColor);
        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void ToMale()
    {
        characterAppearance.isMale = true;
        texturesCounter = 1;
        RefreshTextures();
    }

    public void ToFemale()
    {
        characterAppearance.isMale = false;
        texturesCounter = 0;
        RefreshTextures();
    }

    // public void NextTextureSet()
    // {
    //     IncrementCounter(ref texturesCounter, so.skinTexturesDatabases);
    //     RefreshTextures();
    // }

    // public void PreviousTextureSet()
    // {
    //     DecrementCounter(ref texturesCounter, so.skinTexturesDatabases);
    //     RefreshTextures();
    // }

    public void SaveAppearance()
    {
        characterAppearance.skinIndex = texturesCounter;

        characterAppearance.armorStyle = activeTextures.armorSkins[armorCounter];
        characterAppearance.eyeColor = activeTextures.eyeSkins[eyeCounter];
        characterAppearance.hairStyle = activeTextures.hairStyles[hairCounter];
        characterAppearance.hairColor = hairColor[hairColorCounter];
        characterAppearance.bodyStyle = activeTextures.bodySkins[bodyCounter];
        characterAppearance.playerName = characterName.text;

        characterAppearance.bodyIndex = bodyCounter;
        characterAppearance.hairIndex = hairCounter;
        characterAppearance.armorIndex = armorCounter;
        characterAppearance.eyeIndex = eyeCounter;

        characterAppearance.armorFolderPath = activeTextures.armorFolderPath;
        characterAppearance.hairFolderPath = activeTextures.hairFolderPath;
        characterAppearance.eyeFolderPath = activeTextures.eyeFolderPath;
        characterAppearance.bodyFolderPath = activeTextures.bodyFolderPath;

        SceneManager.LoadScene("Intro");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ResetTextures()
    {
        bodyChanger.newSprite = activeTextures.bodySkins[0];
        bodyChanger.ResetRenderer();

        hairChanger.newSprite = activeTextures.hairStyles[0];
        hairChanger.ResetRenderer();

        armorChanger.newSprite = activeTextures.armorSkins[0];
        armorChanger.ResetRenderer();

        eyeChanger.newSprite = activeTextures.eyeSkins[0];
        eyeChanger.ResetRenderer();
    }

    private void RefreshTextures()
    {
        bodyChanger.folderPath = activeTextures.bodyFolderPath;
        bodyChanger.newSprite = activeTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();

        hairChanger.folderPath = activeTextures.hairFolderPath;
        hairChanger.newSprite = activeTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();

        armorChanger.folderPath = activeTextures.armorFolderPath;
        armorChanger.newSprite = activeTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();

        eyeChanger.folderPath = activeTextures.eyeFolderPath;
        eyeChanger.newSprite = activeTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    private void IncrementCounter<T>(ref int counter, T[] array) 
    {
        counter++;
        if (counter >= array.Length)
        {
            counter = 0;
        }
    }

    private void DecrementCounter<T>(ref int counter, T[] array) {
        counter--;
        if (counter < 0)
        {
            counter = array.Length - 1;
        }
    }
}
