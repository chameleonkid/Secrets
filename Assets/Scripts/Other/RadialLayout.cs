using UnityEngine;

public static class RadialLayout
{
    public static Vector2[] GetOffsets(int amountOfPositions, float facingDirection, float spreadAngle)
    {
        var offsets = new Vector2[amountOfPositions];

        float originAngle = facingDirection + (spreadAngle / 2f);
        float increment = (amountOfPositions != 1) ? (spreadAngle / (amountOfPositions - 1)) : 0;

        for (int i = 0; i < amountOfPositions; i++)
        {
            float angle = originAngle - (increment * i);
            var vector = MathfEx.AngleToVector2(angle);

            offsets[i] = vector;
        }
        return offsets;
    }
}
