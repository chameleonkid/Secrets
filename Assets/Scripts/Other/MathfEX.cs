using System.Collections;
using UnityEngine;
using System;


public static class MathfEx
{
    public static Vector2 AngleToVector2(float angle)
    {
        var radAngle = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
    }

    public static float Vector2ToAngle(Vector2 vector) => Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
}