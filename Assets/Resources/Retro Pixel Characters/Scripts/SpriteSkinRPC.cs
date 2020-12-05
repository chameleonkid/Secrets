using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class SpriteSkinRPC : MonoBehaviour {
	[Tooltip("The new spritesheet texture to use.")]
	public Texture2D newSprite;
	[Tooltip("The path to the new spritesheet. Note that the sheets must be in the Resources folder.")]
	public string folderPath = "Retro Pixel Characters/Spritesheets/";
	[Tooltip("Set to true if this is a child sprite, like an outfit, eye color, etc.")]
	public bool isChildSprite = false;

	private Sprite[] newSpritesheet; //Spritesheet array. Populated with all sprites within a texture sheet.
	private SpriteRenderer spriteRenderer; //The object's sprite renderer.
	private string currentFrameName; //Name of the current frame. Used to find the current frame's number.
	private int frameIndex = 0; //The index of the current frame. Used to set index of new frame.
	private SpriteSkinRPC parentSkin; //The parent object's SpriteSkin component. Used only by child objects.

	//During start, find the SpriteRenderer and load all spritesheet frames into the array.
	//Again, spritesheets need to be in the Resources folder for the Resources.LoadAll to work.
	void Start () {
		if (spriteRenderer == null)
		{
			spriteRenderer = GetComponentInParent<SpriteRenderer>(); //Set spriteRenderer to current sprite's renderer.
		}
		if (newSprite != null)
		{
			newSpritesheet = Resources.LoadAll<Sprite>(folderPath + newSprite.name); //Load all sprites within newSpritesheet.
		}
		if (isChildSprite == true)
		{
			parentSkin = transform.parent.GetComponent<SpriteSkinRPC>(); //Get the base sprite's SpriteSkin component if this is a child sprite.
		}
	}

	//Using LateUpdate, since trying to replace sprites in Update will just be overridden by the animation.
	void LateUpdate ()
	{
		if (isChildSprite == true && parentSkin == null)
		{
			Debug.LogError("Couldn't find SpriteSkin in parent. Either make object child of character base or untick Is Child Sprite.");
			this.enabled = false;
			return;
		}

		//If we've got our spritesheet, our renderer and the spritesheet is actually a sheet, let's fire up the magic!
		if (newSprite != null && spriteRenderer != null && newSpritesheet.Length > 0)
		{
			//Get the currently rendered sprite's name.
			currentFrameName = spriteRenderer.sprite.name;
			//First, remove the "r2c" prefix, as it will offset frame number if left in sprite name.
			currentFrameName = Regex.Replace(currentFrameName, "r2c", "");
			//Parse out the frame number from the frame name and use as index for the new frame to render.
			//Get the parent object sprite's frame index if this is a child object.
			if (isChildSprite == false) 
			{
				int.TryParse(Regex.Replace(currentFrameName, "[^0-9]", ""), out frameIndex);
			}
			else 
			{				
				frameIndex = parentSkin.GetParentFrameIndex();
			}
			//Finally, set the new sprite to render.
			spriteRenderer.sprite = newSpritesheet[frameIndex];
		}
		else if (newSprite == null)
		{
			//If the New Sprite has not been set, log a warning and then disable this script (to prevent a new warning every frame.)
			Debug.LogWarning("New Sprite has not been set. Drag and drop your spritesheet texture to the New Sprite field.");
			this.enabled = false;
		}
		else if (spriteRenderer == null)
		{
			//If the object contains no Sprite Renderer, log an error and disable script.
			Debug.LogError("Parent does not contain a Sprite Renderer.");
			this.enabled = false;
		}
		else if (newSpritesheet.Length <= 0)
		{
			//If the new sprite sheet fails loading any sprites, it might not be a proper spritesheet. Could also be that the sprite wasn't found in the folder path.
			Debug.LogWarning("It seems there were too few sprites in the New Sprite. Was it all a ruse?! Actually, it might be the wrong Folder Path, too.");
			this.enabled = false;
		}
	}

	//This feature is used by child objects to get the frameIndex from the parent base sprite's SpriteSkin component.
	public int GetParentFrameIndex()
	{
		if (isChildSprite == false)
		{
			return frameIndex; //If this object is indeed the parent base sprite, return the frameIndex.
			//The child object will use this number to set the current frame in its SpriteSkin component.
		}
		else
		{
			Debug.LogError("Trying to access GetParentFrameIndex in child sprite object."); //If this were somehow accessed in a child object.
			return 0;
		}
	}
}
