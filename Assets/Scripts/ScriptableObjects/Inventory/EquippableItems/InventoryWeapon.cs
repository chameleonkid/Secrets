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

    public Vector2[] leftHitboxPolygon { get; private set; } = new Vector2[6];
    public Vector2[] rightHitboxPolygon { get; private set; } = new Vector2[6];
    public Vector2[] upHitboxPolygon { get; private set; } = new Vector2[6];
    public Vector2[] downHitboxPolygon { get; private set; } = new Vector2[6];

    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage;

    private void OnValidate()
    {
        switch (weaponType)
        {
            default:
            case WeaponType.Sword:
                swingTime = 0.3f;
                SetSwordHitboxes();
                break;
            case WeaponType.Axe:
                swingTime = 1;
                SetAxeHitboxes();
                break;
            case WeaponType.Spear:
                swingTime = 0.6f;
                SetSpearHitboxes();
                break;
            case WeaponType.Bow:
                swingTime = 1.25f;     //Maybe make a field for swingTime so every weapon can have a specific CD
                SetSpearHitboxes();
                break;
        }
    }

    private void SetSwordHitboxes()
    {
        leftHitboxPolygon = new[] {
            new Vector2(0f, 0.5f),
            new Vector2(0, 0.6f),
            new Vector2(-0.55f, 0.7f),
            new Vector2(-0.55f, 0.7f),
            new Vector2(-0.55f, 0f),
            new Vector2(0f, 0f)
        };

        rightHitboxPolygon = new[] {
            new Vector2(0f, 0.5f),
            new Vector2(0, 0.6f),
            new Vector2(0.55f, 0.7f),
            new Vector2(0.55f, 0.7f),
            new Vector2(0.55f, 0f),
            new Vector2(0f, 0f)
        };

        upHitboxPolygon = new[] {
            new Vector2(0f, 0.5f),
            new Vector2(0.4f, 0.75f),
            new Vector2(0.3f, 1.1f),
            new Vector2(-0.3f, 1.1f),
            new Vector2(-0.4f, 0.75f),
            new Vector2(0f, 0.5f)
        };

        downHitboxPolygon = new[] {
            new Vector2(0f, 0.25f),
            new Vector2(0.5f, 0.15f),
            new Vector2(0.35f, -0.25f),
            new Vector2(0f, -0.25f),
            new Vector2(-0.35f, -0.25f),
            new Vector2(-0.5f, 0.15f)
        };
    }

    private void SetAxeHitboxes()
    {
        leftHitboxPolygon = new[] {
            new Vector2(0f, 0.5f),
            new Vector2(0, 0.6f),
            new Vector2(-0.7f, 0.6f),
            new Vector2(-0.7f, 0.6f),
            new Vector2(-0.7f, 0f),
            new Vector2(0f, 0f)
        };

        rightHitboxPolygon = new[] {
            new Vector2(0f, 0.5f),
            new Vector2(0, 0.6f),
            new Vector2(0.7f, 0.6f),
            new Vector2(0.7f, 0.6f),
            new Vector2(0.7f, 0f),
            new Vector2(0f, 0f)
        };

        upHitboxPolygon = new[] {
            new Vector2(0f, 0.5f),
            new Vector2(0.4f, 0.9f),
            new Vector2(0.3f, 1.3f),
            new Vector2(-0.3f, 1.3f),
            new Vector2(-0.4f, 0.9f),
            new Vector2(0f, 0.5f)
        };

        downHitboxPolygon = new[] {
            new Vector2(0f, 0.25f),
            new Vector2(0.5f, 0.15f),
            new Vector2(0.35f, -0.5f),
            new Vector2(0f, -0.5f),
            new Vector2(-0.35f, -0.5f),
            new Vector2(-0.5f, 0.15f)
        };
    }

    private void SetSpearHitboxes()
    {
        leftHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(5, 2),
            new Vector2(3, 3),
            new Vector2(0.1f, 0.1f),
            new Vector2(0.1f, 0.1f),
            new Vector2(0.1f, 0.1f)
        };


        rightHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(2, 2),
            new Vector2(3, 3),
            new Vector2(-0.5f, 0.8f),
            new Vector2(-0.25f, 0.5f),
            new Vector2(0.2f, 0.5f)
        };

        upHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(1, 1),
            new Vector2(1, 1),
            new Vector2(-0.5f, 0.8f),
            new Vector2(-0.25f, 0.5f),
            new Vector2(0.2f, 0.5f)
        };

        downHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(1, 1),
            new Vector2(0.6f, 1.1f),
            new Vector2(-0.5f, 0.8f),
            new Vector2(-0.25f, 0.5f),
            new Vector2(0.2f, 0.5f)
        };
    }

    private void SetBowHitboxes()
    {
        leftHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
        };


        rightHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
        };

        upHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
        };

        downHitboxPolygon = new[] {
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
        };
    }
}
