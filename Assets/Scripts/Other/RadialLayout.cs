using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public static class RadialLayout
{
    public static Vector2[] GetPositions(int amountOfPositions, float facingDirection, float spreadAngle, float radius)
    {
        var offsets = new Vector2[amountOfPositions];

        float originAngle = facingDirection + (spreadAngle / 2f);
        float increment = spreadAngle / (amountOfPositions - 1);

        for (int i = 0; i < amountOfPositions; i++)
        {
            float angle = originAngle - (increment * i);
            var vector = MathfEx.AngleToVector2(angle);
            vector *= radius;

            offsets[i] = vector;
        }
        return offsets;
    }
}

