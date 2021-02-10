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
    [SerializeField] private AudioClip buttonClick = default;

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI characterName = default;

    [Header("Persistence")]
    [SerializeField] private CharacterAppearance characterAppearance = default;

    private int texturesCounter;
    private ScriptableObjectPersistence so => SaveManager.Instance.so;
    private SkinTexturesDatabase activeTextures => so.skinTexturesDatabases[texturesCounter];

    private void Awake()
    {
        ResetTextures();
    }

    private void Start()
    {
        MusicManager.RequestMusic(creatorMusic);
    }

    public void NextBody()
    {
        IncrementCounter(ref bodyCounter, activeTextures.bodySkins);
        SoundManager.RequestSound(buttonClick);
        bodyChanger.newSprite = activeTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void PreviousBody()
    {
        DecrementCounter(ref bodyCounter, activeTextures.bodySkins);
        SoundManager.RequestSound(buttonClick);
        bodyChanger.newSprite = activeTextures.bodySkins[bodyCounter];
        bodyChanger.ResetRenderer();
    }

    public void NextHair()
    {
        IncrementCounter(ref hairCounter, activeTextures.hairStyles);
        SoundManager.RequestSound(buttonClick);
        hairChanger.newSprite = activeTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHair()
    {
        DecrementCounter(ref hairCounter, activeTextures.hairStyles);
        SoundManager.RequestSound(buttonClick);
        hairChanger.newSprite = activeTextures.hairStyles[hairCounter];
        hairChanger.ResetRenderer();
    }

    public void NextArmor()
    {
        IncrementCounter(ref armorCounter, activeTextures.armorSkins);
        SoundManager.RequestSound(buttonClick);
        armorChanger.newSprite = activeTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void PreviousArmor()
    {
        DecrementCounter(ref armorCounter, activeTextures.armorSkins);
        SoundManager.RequestSound(buttonClick);
        armorChanger.newSprite = activeTextures.armorSkins[armorCounter];
        armorChanger.ResetRenderer();
    }

    public void NextEyes()
    {
        IncrementCounter(ref eyeCounter, activeTextures.eyeSkins);
        SoundManager.RequestSound(buttonClick);
        eyeChanger.newSprite = activeTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void PreviousEyes()
    {
        DecrementCounter(ref eyeCounter, activeTextures.eyeSkins);
        SoundManager.RequestSound(buttonClick);
        eyeChanger.newSprite = activeTextures.eyeSkins[eyeCounter];
        eyeChanger.ResetRenderer();
    }

    public void NextHairColor()
    {
        IncrementCounter(ref hairColorCounter, hairColor);
        SoundManager.RequestSound(buttonClick);
        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void PreviousHairColor()
    {
        DecrementCounter(ref hairColorCounter, hairColor);
        SoundManager.RequestSound(buttonClick);
        hairColorRenderer.color = hairColor[hairColorCounter];
        hairChanger.ResetRenderer();
    }

    public void NextTextureSet()
    {
        IncrementCounter(ref texturesCounter, so.skinTexturesDatabases);
        ResetRenderers();
    }

    public void PreviousTextureSet()
    {
        DecrementCounter(ref texturesCounter, so.skinTexturesDatabases);
        ResetRenderers();
    }

    public void SaveAppearance()
    {
        characterAppearance.index = texturesCounter;

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

        SoundManager.RequestSound(buttonClick);
        SceneManager.LoadScene("Mavens_Inn_Cutscene");
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

    private void ResetRenderers()
    {
        bodyChanger.ResetRenderer();
        hairChanger.ResetRenderer();
        armorChanger.ResetRenderer();
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
