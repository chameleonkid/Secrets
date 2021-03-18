using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class InventoryWeapon : EquippableItem
{
    public enum WeaponType
    {
        Sword,
        Axe,
        Spear,
        Bow
    }

    public int minDamage;
    public int maxDamage;
    public Texture2D weaponSkin = default;
    public WeaponType weaponType = default;
    public float swingTime = 0.0f;
    [ColorUsageAttribute(true, true)] public Color glowColor;

    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage + ("\n\n Level: ") + level;

    public Vector2[] leftHitboxPolygon {
        get {
            switch (weaponType) {
                default:
                case WeaponType.Sword:
                    return swordLeft;
                case WeaponType.Axe:
                    return axeLeft;
                case WeaponType.Spear:
                    return spearLeft;
                case WeaponType.Bow:
                    return bowLeft;
            }
        }
    }
    
    public Vector2[] rightHitboxPolygon {
        get {
            switch (weaponType) {
                default:
                case WeaponType.Sword:
                    return swordRight;
                case WeaponType.Axe:
                    return axeRight;
                case WeaponType.Spear:
                    return spearRight;
                case WeaponType.Bow:
                    return bowRight;
            }
        }
    }

    public Vector2[] upHitboxPolygon {
        get {
            switch (weaponType) {
                default:
                case WeaponType.Sword:
                    return swordUp;
                case WeaponType.Axe:
                    return axeUp;
                case WeaponType.Spear:
                    return spearUp;
                case WeaponType.Bow:
                    return bowUp;
            }
        }
    }

    public Vector2[] downHitboxPolygon {
        get {
            switch (weaponType) {
                default:
                case WeaponType.Sword:
                    return swordDown;
                case WeaponType.Axe:
                    return axeDown;
                case WeaponType.Spear:
                    return spearDown;
                case WeaponType.Bow:
                    return bowDown;
            }
        }
    }
    
    #region Sword Hitboxes
    private static Vector2[] swordLeft = {
        new Vector2(0f, 0.5f),
        new Vector2(0, 0.6f),
        new Vector2(-0.55f, 0.7f),
        new Vector2(-0.55f, 0.7f),
        new Vector2(-0.55f, 0f),
        new Vector2(0f, 0f)
    };

    private static Vector2[] swordRight = {
        new Vector2(0f, 0.5f),
        new Vector2(0, 0.6f),
        new Vector2(0.55f, 0.7f),
        new Vector2(0.55f, 0.7f),
        new Vector2(0.55f, 0f),
        new Vector2(0f, 0f)
    };

    private static Vector2[] swordUp = {
        new Vector2(0f, 0.5f),
        new Vector2(0.4f, 0.75f),
        new Vector2(0.3f, 1.1f),
        new Vector2(-0.3f, 1.1f),
        new Vector2(-0.4f, 0.75f),
        new Vector2(0f, 0.5f)
    };

    private static Vector2[] swordDown =  {
        new Vector2(0f, 0.25f),
        new Vector2(0.5f, 0.15f),
        new Vector2(0.35f, -0.5f),
        new Vector2(0f, -0.5f),
        new Vector2(-0.35f, -0.5f),
        new Vector2(-0.5f, 0.15f)
    };
    #endregion

    #region Axe Hitboxes
    private static Vector2[] axeLeft = {
        new Vector2(0f, 0.5f),
        new Vector2(0, 0.6f),
        new Vector2(-0.7f, 0.6f),
        new Vector2(-0.7f, 0.6f),
        new Vector2(-0.7f, 0f),
        new Vector2(0f, 0f)
    };

    private static Vector2[] axeRight = {
        new Vector2(0f, 0.5f),
        new Vector2(0, 0.6f),
        new Vector2(0.7f, 0.6f),
        new Vector2(0.7f, 0.6f),
        new Vector2(0.7f, 0f),
        new Vector2(0f, 0f)
    };

    private static Vector2[] axeUp = {
        new Vector2(0f, 0.5f),
        new Vector2(0.4f, 0.9f),
        new Vector2(0.3f, 1.3f),
        new Vector2(-0.3f, 1.3f),
        new Vector2(-0.4f, 0.9f),
        new Vector2(0f, 0.5f)
    };

    private static Vector2[] axeDown = {
        new Vector2(0f, 0.25f),
        new Vector2(0.5f, 0.15f),
        new Vector2(0.35f, -0.5f),
        new Vector2(0f, -0.5f),
        new Vector2(-0.35f, -0.5f),
        new Vector2(-0.5f, 0.15f)
    };
    #endregion

    #region Spear Hitboxes
    private static Vector2[] spearLeft = {
        new Vector2(0, 0),
        new Vector2(5, 2),
        new Vector2(3, 3),
        new Vector2(0.1f, 0.1f),
        new Vector2(0.1f, 0.1f),
        new Vector2(0.1f, 0.1f)
    };

    private static Vector2[] spearRight = {
        new Vector2(0, 0),
        new Vector2(2, 2),
        new Vector2(3, 3),
        new Vector2(-0.5f, 0.8f),
        new Vector2(-0.25f, 0.5f),
        new Vector2(0.2f, 0.5f)
    };

    private static Vector2[] spearUp = {
        new Vector2(0, 0),
        new Vector2(1, 1),
        new Vector2(1, 1),
        new Vector2(-0.5f, 0.8f),
        new Vector2(-0.25f, 0.5f),
        new Vector2(0.2f, 0.5f)
    };

    private static Vector2[] spearDown = {
        new Vector2(0, 0),
        new Vector2(1, 1),
        new Vector2(0.6f, 1.1f),
        new Vector2(-0.5f, 0.8f),
        new Vector2(-0.25f, 0.5f),
        new Vector2(0.2f, 0.5f)
    };
    #endregion

    #region Bow Hitboxes
    private static Vector2[] bowLeft = {
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
    };

    private static Vector2[] bowRight = {
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
    };

    private static Vector2[] bowUp = {
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
    };

    private static Vector2[] bowDown = {
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
    };
    #endregion

    private void OnValidate()
    {
        switch (weaponType)
        {
            default:
            case WeaponType.Sword:
                swingTime = 0.3f;
                break;
            case WeaponType.Axe:
                swingTime = 1;
                break;
            case WeaponType.Spear:
                swingTime = 0.6f;
                break;
            case WeaponType.Bow:
                swingTime = 1.25f;     //Maybe make a field for swingTime so every weapon can have a specific CD
                break;
        }
    }
}
