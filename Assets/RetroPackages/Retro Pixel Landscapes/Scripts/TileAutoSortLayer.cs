using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileAutoSortLayer : MonoBehaviour
{
    /// <summary>
    /// This script automatically sets the Sorting Layer and Order in Layer of GameObjects associated with the rule tiles.
    /// </summary>
    private SpriteRenderer[] sprites;
    private TilemapRenderer tilemap;

    private void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        if (transform.parent == null || transform.parent.GetComponentInParent<TilemapRenderer>() == null)
        {
            Debug.LogError("Cannot retrieve parent TilemapRenderer for auto sorting. Do not place tile prefabs directly in scene.",this);
            return;
        }
        else
        {
            tilemap = transform.parent.GetComponentInParent<TilemapRenderer>();
        }

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingLayerID = tilemap.sortingLayerID;
            sprite.sortingOrder = 1;
        }
    }
}
