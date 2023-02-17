using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class InventoryWeapon : EquippableItem
{
    public enum WeaponType
    {
        Sword,
        Axe,
        Boomerang,
        Bow,
        Hammer
    }

    public int minDamage;
    public int maxDamage;
    public GameObject projectile;
    public Texture2D weaponSkin = default;
    public WeaponType weaponType = default;
    public float swingTime = 0.0f;
    [ColorUsageAttribute(true, true)] public Color glowColor;

    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage + ("\n\n Level: ") + level + ("\n\n Attackspeed: ") + swingTime + (" Seconds");

    public Vector2[] leftHitboxPolygon {
        get {
            switch (weaponType) {
                default:
                case WeaponType.Sword:
                    return swordLeft;
                case WeaponType.Axe:
                    return axeLeft;
                case WeaponType.Boomerang:
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
                case WeaponType.Boomerang:
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
                case WeaponType.Boomerang:
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
                case WeaponType.Boomerang:
                    return spearDown;
                case WeaponType.Bow:
                    return bowDown;
            }
        }
    }
    
    #region Sword Hitboxes
    private static Vector2[] swordLeft = {
        new Vector2(-0.4069519f, -0.3820038f),
        new Vector2(-0.1808929f, -0.390152f),
        new Vector2(0.3363366f, -0.1696549f),
        new Vector2(0.3117828f, 0.01402283f),
        new Vector2(-0.157196f, 0.1885376f),
        new Vector2(0.07695651f, 0.6883322f),
        new Vector2(-0.678606f, 0.6328795f),
        new Vector2(-0.9410026f, 0.3514785f),
        new Vector2(-0.8930984f, 0.1099698f),
        new Vector2(-0.7081349f, -0.1537272f),
        new Vector2(-0.54772f, -0.3385902f),
    };

    private static Vector2[] swordRight = {
        new Vector2(0.5872498f, 0.6769409f),
        new Vector2(-0.1358874f, 0.6984687f),
        new Vector2(-0.128545f, 0.5766439f),
        new Vector2(0.125824f, 0.2002258f),
        new Vector2(-0.3581474f, -0.01712887f),
        new Vector2(-0.3356709f, -0.1835297f),
        new Vector2(0.1340637f, -0.3865051f),
        new Vector2(0.495224f, -0.379425f),
        new Vector2(0.6174347f, -0.1728446f),
        new Vector2(0.8308065f, 0.05306007f),
        new Vector2(0.8740206f, 0.4863611f),
    };

    private static Vector2[] swordUp = {
        new Vector2(0.7838745f, 0.263092f),
        new Vector2(0.8469129f, 0.555679f),
        new Vector2(0.640686f, 0.8614197f),
        new Vector2(0.4284437f, 0.9774902f),
        new Vector2(0.1268232f, 1.088135f),
        new Vector2(-0.0275116f, 0.8649139f),
        new Vector2(-0.3745728f, 0.8208923f),
        new Vector2(-0.6811445f, 0.3949863f),
        new Vector2(-0.2655188f, 0.1351249f),
        new Vector2(0.3759766f, 0.09655762f),
        new Vector2(0.6164382f, 0.08582008f)
    };

    private static Vector2[] swordDown =  {
        new Vector2(-0.2358f, 0.2629f),
        new Vector2(-0.1795859f, 0.06671722f),
        new Vector2(-0.3346993f, -0.01477894f),
        new Vector2(-0.4056413f, -0.1583749f),
        new Vector2(-0.5530407f, -0.1325015f),
        new Vector2(-0.5242767f, -0.3492174f),
        new Vector2(-0.4158325f, -0.4849291f),
        new Vector2(-0.2136993f, -0.5695391f),
        new Vector2(0.07230663f, -0.5696025f),
        new Vector2(0.3982315f,  -0.3832312f),
        new Vector2(0.4726486f,  -0.2432566f),
        new Vector2(0.5810461f, -0.1280579f),
        new Vector2(0.6830292f, 0.3905234f),
        new Vector2(0.5639355f, 0.5707344f)
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

    #region Hammer Hitboxes
    private static Vector2[] hammerLeft = {
        new Vector2(0, 0),
        new Vector2(5, 2),
        new Vector2(3, 3),
        new Vector2(0.1f, 0.1f),
        new Vector2(0.1f, 0.1f),
        new Vector2(0.1f, 0.1f)
    };

    private static Vector2[] hammerRight = {
        new Vector2(0, 0),
        new Vector2(2, 2),
        new Vector2(3, 3),
        new Vector2(-0.5f, 0.8f),
        new Vector2(-0.25f, 0.5f),
        new Vector2(0.2f, 0.5f)
    };

    private static Vector2[] hammerUp = {
        new Vector2(0, 0),
        new Vector2(1, 1),
        new Vector2(1, 1),
        new Vector2(-0.5f, 0.8f),
        new Vector2(-0.25f, 0.5f),
        new Vector2(0.2f, 0.5f)
    };

    private static Vector2[] hammerDown = {
        new Vector2(0, 0),
        new Vector2(1, 1),
        new Vector2(0.6f, 1.1f),
        new Vector2(-0.5f, 0.8f),
        new Vector2(-0.25f, 0.5f),
        new Vector2(0.2f, 0.5f)
    };
    #endregion
    /*
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
            case WeaponType.Boomerang:
                swingTime = 0.75f;
                break;
            case WeaponType.Bow:
                swingTime = 1.25f;     //Maybe make a field for swingTime so every weapon can have a specific CD
                break;
            case WeaponType.Hammer:
                swingTime = 1.25f;     //Maybe make a field for swingTime so every weapon can have a specific CD
                break;
        }
    }
    */
}
