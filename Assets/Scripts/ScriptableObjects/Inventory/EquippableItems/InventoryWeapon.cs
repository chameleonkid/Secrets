using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class InventoryWeapon : EquippableItem
{
    private enum WeaponType
    {
        Sword,
        Axe,
        spear
    }

    public int minDamage;
    public int maxDamage;
    public float glowIntensity;
    [SerializeField] private WeaponType weaponType = default;
    [ColorUsageAttribute(true, true)] public Color glowColor;



    Vector2 leftPoint0 = new Vector2(0.5f, 0.7f);
    Vector2 leftPoint1 = new Vector2(0.4f, 0.9f);
    Vector2 leftPoint2 = new Vector2(0.6f, 1.1f);
    Vector2 leftPoint3 = new Vector2(-0.5f, 0.8f);
    Vector2 leftPoint4 = new Vector2(-0.25f, 0.5f);
    Vector2 leftPoint5 = new Vector2(0.2f, 0.5f);
    Vector2 leftPoint6 = new Vector2(0.2f, 0.5f);

    Vector2 rightPoint0 = new Vector2(0.5f, 0.7f);
    Vector2 rightPoint1 = new Vector2(0.4f, 0.9f);
    Vector2 rightPoint2 = new Vector2(0.6f, 1.1f);
    Vector2 rightPoint3 = new Vector2(-0.5f, 0.8f);
    Vector2 rightPoint4 = new Vector2(-0.25f, 0.5f);
    Vector2 rightPoint5 = new Vector2(0.2f, 0.5f);
    Vector2 rightPoint6 = new Vector2(0.2f, 0.5f);

    Vector2 upPoint0 = new Vector2(0.5f, 0.7f);
    Vector2 upPoint1 = new Vector2(0.4f, 0.9f);
    Vector2 upPoint2 = new Vector2(0.6f, 1.1f);
    Vector2 upPoint3 = new Vector2(-0.5f, 0.8f);
    Vector2 upPoint4 = new Vector2(-0.25f, 0.5f);
    Vector2 upPoint5 = new Vector2(0.2f, 0.5f);
    Vector2 upPoint6 = new Vector2(0.2f, 0.5f);

    Vector2 downPoint0 = new Vector2(0.5f, 0.7f);
    Vector2 downPoint1 = new Vector2(0.4f, 0.9f);
    Vector2 downPoint2 = new Vector2(0.6f, 1.1f);
    Vector2 downPoint3 = new Vector2(-0.5f, 0.8f);
    Vector2 downPoint4 = new Vector2(-0.25f, 0.5f);
    Vector2 downPoint5 = new Vector2(0.2f, 0.5f);
    Vector2 downPoint6 = new Vector2(0.2f, 0.5f);

    public Vector2[] leftBox;
    public Vector2[] rightBox;
    public Vector2[] upBox;
    public Vector2[] downBox;


    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage;

    public void OnValidate()
    {
        if (weaponType == WeaponType.Sword)
        {
            leftPoint0 = new Vector2(0,0);
            leftPoint1 = new Vector2(2, 2);
            leftPoint2 = new Vector2(3, 3);
            leftPoint3 = new Vector2(0.1f, 0.1f);
            leftPoint4 = new Vector2(0.1f, 0.1f);
            leftPoint5 = new Vector2(0.1f, 0.1f);
            leftPoint6 = new Vector2(0,0);

             rightPoint0 = new Vector2(0,0);
             rightPoint1 = new Vector2(2, 2);
             rightPoint2 = new Vector2(3, 3);
             rightPoint3 = new Vector2(-0.5f, 0.8f);
             rightPoint4 = new Vector2(-0.25f, 0.5f);
             rightPoint5 = new Vector2(0.2f, 0.5f);
             rightPoint6 = new Vector2(0, 0);

            upPoint0 = new Vector2(0,0);
             upPoint1 = new Vector2(1, 1);
             upPoint2 = new Vector2(1, 1);
             upPoint3 = new Vector2(-0.5f, 0.8f);
             upPoint4 = new Vector2(-0.25f, 0.5f);
             upPoint5 = new Vector2(0.2f, 0.5f);
             upPoint6 = new Vector2(0, 0);

            downPoint0 = new Vector2(0,0);
             downPoint1 = new Vector2(1, 1);
             downPoint2 = new Vector2(0.6f, 1.1f);
             downPoint3 = new Vector2(-0.5f, 0.8f);
             downPoint4 = new Vector2(-0.25f, 0.5f);
             downPoint5 = new Vector2(0.2f, 0.5f);
             downPoint6 = new Vector2(0, 0);
        }
        else if (weaponType == WeaponType.Axe)
        {
            leftPoint0 = new Vector2(0, 0);
            leftPoint1 = new Vector2(2, 2);
            leftPoint2 = new Vector2(3, 3);
            leftPoint3 = new Vector2(0.1f, 0.1f);
            leftPoint4 = new Vector2(0.1f, 0.1f);
            leftPoint5 = new Vector2(0.1f, 0.1f);
            leftPoint6 = new Vector2(0, 0);

            rightPoint0 = new Vector2(1, 1);
            rightPoint1 = new Vector2(2, 2);
            rightPoint2 = new Vector2(3, 3);
            rightPoint3 = new Vector2(-0.5f, 0.8f);
            rightPoint4 = new Vector2(-0.25f, 0.5f);
            rightPoint5 = new Vector2(0.2f, 0.5f);
            rightPoint6 = new Vector2(0.2f, 0.5f);

            upPoint0 = new Vector2(0, 0);
            upPoint1 = new Vector2(4f, 15f);
            upPoint2 = new Vector2(7f, 20f);
            upPoint3 = new Vector2(0f, 25f);
            upPoint4 = new Vector2(-7f, 20f);
            upPoint5 = new Vector2(-4f, 15f);
            upPoint6 = new Vector2(0, 0);

            downPoint0 = new Vector2(0, 0);
            downPoint1 = new Vector2(1, 1);
            downPoint2 = new Vector2(0.6f, 1.1f);
            downPoint3 = new Vector2(-0.5f, 0.8f);
            downPoint4 = new Vector2(-0.25f, 0.5f);
            downPoint5 = new Vector2(0.2f, 0.5f);
            downPoint6 = new Vector2(0, 0);
        }
        else if (weaponType == WeaponType.spear)
        {
            leftPoint0 = new Vector2(0, 0);
            leftPoint1 = new Vector2(2, 2);
            leftPoint2 = new Vector2(3, 3);
            leftPoint3 = new Vector2(0.1f, 0.1f);
            leftPoint4 = new Vector2(0.1f, 0.1f);
            leftPoint5 = new Vector2(0.1f, 0.1f);
            leftPoint6 = new Vector2(0, 0);

            rightPoint0 = new Vector2(1, 1);
            rightPoint1 = new Vector2(2, 2);
            rightPoint2 = new Vector2(3, 3);
            rightPoint3 = new Vector2(-0.5f, 0.8f);
            rightPoint4 = new Vector2(-0.25f, 0.5f);
            rightPoint5 = new Vector2(0.2f, 0.5f);
            rightPoint6 = new Vector2(0.2f, 0.5f);

            upPoint0 = new Vector2(0, 0);
            upPoint1 = new Vector2(4f, 15f);
            upPoint2 = new Vector2(7f, 20f);
            upPoint3 = new Vector2(0f, 25f);
            upPoint4 = new Vector2(-7f, 20f);
            upPoint5 = new Vector2(-4f, 15f);
            upPoint6 = new Vector2(0, 0);

            downPoint0 = new Vector2(0, 0);
            downPoint1 = new Vector2(1, 1);
            downPoint2 = new Vector2(0.6f, 1.1f);
            downPoint3 = new Vector2(-0.5f, 0.8f);
            downPoint4 = new Vector2(-0.25f, 0.5f);
            downPoint5 = new Vector2(0.2f, 0.5f);
            downPoint6 = new Vector2(0, 0);
        }

        leftBox = new[] { leftPoint0, leftPoint1, leftPoint2, leftPoint3, leftPoint4, leftPoint5, leftPoint6 };
        rightBox = new[] { rightPoint0, rightPoint1, rightPoint2, rightPoint3, rightPoint4, rightPoint5, rightPoint6 };
        upBox = new[] { upPoint0, upPoint1, upPoint2, upPoint3, upPoint4, upPoint5, upPoint6 };
        downBox = new[] { downPoint0, downPoint1, downPoint2, downPoint3, downPoint4, downPoint5, downPoint6 };
    }

}
