Thank you very much for purchasing the Retro Pixel Characters!
Here's just a few notes to the package and its contents.

Update 1.51: Component offset changed to use Z-axis instead of sort order, for compatibility with future pack. Your project needs the following settings to work:
Project Settings > Graphics Settings > Transparency Sort Mode: Custom Axis
Transparency Sort Axis: X:0 Y:1 Z:-0.49

====SPRITESKIN SCRIPT INFORMATION====
SCRIPT INFO
The SpriteSkin script included in the pack is used for a few things.
It 'skins' the sprites during run time to allow all spritesheets to use the same set of animations. Otherwise, there'd have to be a set of animations for each separate sheet.
It is also used by child objects, like hairstyles, outifts, etc, to dynamically skin them to use the same animation frame as the character base. As such, all prefabs used for eye colors, hairstyles, outfits and weapons should be child objects of the character base. See the included demo scene for reference.

RESOURCES FOLDER
The spritesheets need to be in the "Resources\Retro Pixel Characters\Spritesheets" folder.
This is what makes the SpriteSkin script work, since it uses Resources.LoadAll().
The rest of the assets may be moved outside of the Resources folder.

OTHER USES
You could use the script for skinning your own sprites too.
It might need a bit of modification to work for your project.

====PLAYERCONTROLLER2D SCRIPT INFORMATION====
This script is a very simple, sample character controller script. It is set up to work with the sample animator tree to enable movement in all 4 directions and display the appropriate animation.

The script is attached to all the character base prefabs. Study those prefabs to see how to set up an object to use the script.

It is highly recommended that you use your own controller script, or build upon the included one, to have it work exactly how you want it.

====PREFABS & ANIMATIONS====
The characters have a complete animation set and organized animator controller tree.
The animator controller is currently set to work with the PlayerController2D script, which can make the characters stand, walk and run in all 4 directions. Other animations are organized in the animator tree as well, ready for setup with your project.

The prefabs are made to demonstrate how to set up the sprites. The SpriteSkin script
is attached to all prefabs. There are no AI scripts included or attached to prefabs.