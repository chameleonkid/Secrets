using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class InventoryWeapon : EquippableItem
{
    public enum WeaponType
    {
        Sword,
        Axe,
        Spear
    }

    public int minDamage;
    public int maxDamage;
    public float glowIntensity;
    public Texture2D weaponSkin = default;
    public WeaponType weaponType = default;
    public float swingTime = 0.3f;
    [ColorUsageAttribute(true, true)] public Color glowColor;

    public Vector2[] leftBox;
    public Vector2[] rightBox;
    public Vector2[] upBox;
    public Vector2[] downBox;

    private Vector2[] _leftHitboxPolygon = new Vector2[6];
    private Vector2[] _rightHitboxPolygon = new Vector2[6];
    private Vector2[] _upHitboxPolygon = new Vector2[6];
    private Vector2[] _downHitboxPolygon = new Vector2[6];


    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage;

    public void OnValidate()
    {
        if (weaponType == WeaponType.Sword)
        {

            _leftHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(5, 2),
                new Vector2(3, 3),
                new Vector2(0.1f, 0.1f),
                new Vector2(0.1f, 0.1f),
                new Vector2(0.1f, 0.1f)
                };


            _rightHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(2, 2),
                new Vector2(3, 3),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            };

            _upHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(1, 1),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            };

            _downHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(0.6f, 1.1f),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            }
        }
        else if (weaponType == WeaponType.Axe)
        {

            swingTime = 0.5f;

            _leftHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(5, 2),
                new Vector2(3, 3),
                new Vector2(0.1f, 0.1f),
                new Vector2(0.1f, 0.1f),
                new Vector2(0.1f, 0.1f)
                };


            _rightHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(2, 2),
                new Vector2(3, 3),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            };

            _upHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(1, 1),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            };

            _downHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(0.6f, 1.1f),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            }
        }
        else if (weaponType == WeaponType.Spear)
        {

            swingTime = 0.6f;

            _leftHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(5, 2),
                new Vector2(3, 3),
                new Vector2(0.1f, 0.1f),
                new Vector2(0.1f, 0.1f),
                new Vector2(0.1f, 0.1f)
                };


            _rightHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(2, 2),
                new Vector2(3, 3),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            };

            _upHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(1, 1),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            };

            _downHitboxPolygon = new[] {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(0.6f, 1.1f),
                new Vector2(-0.5f, 0.8f),
                new Vector2(-0.25f, 0.5f),
                new Vector2(0.2f, 0.5f)
            }
        }

        leftBox = new[] { _leftHitboxPolygon[0], _leftHitboxPolygon[1], _leftHitboxPolygon[2], _leftHitboxPolygon[3], _leftHitboxPolygon[4], _leftHitboxPolygon[5] };
        rightBox = new[] { _rightHitboxPolygon[0], _rightHitboxPolygon[1], _rightHitboxPolygon[2], _rightHitboxPolygon[3], _rightHitboxPolygon[4], _rightHitboxPolygon[5] };
        upBox = new[] { _upHitboxPolygon[0], _upHitboxPolygon[1], _upHitboxPolygon[2], _upHitboxPolygon[3], _upHitboxPolygon[4], _upHitboxPolygon[5] };
        downBox = new[] { _downHitboxPolygon[0], _downHitboxPolygon[1], _downHitboxPolygon[2], _downHitboxPolygon[3], _downHitboxPolygon[4], _downHitboxPolygon[5] };
    }

}
