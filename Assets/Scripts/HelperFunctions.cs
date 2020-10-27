using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions
{
    public static float GetAngle(Vector3 v1, Vector3 v2)
    {
        float lengthV1 = Vector3.Magnitude(v1);
        float lengthV2 = Vector3.Magnitude(v2);
        float dotV1V2 = Vector3.Dot(v1, v2);
        float cosAngle = dotV1V2 / (lengthV1 * lengthV2);
        return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }

    public static float Squared(float number)
    {
        return number * number;
    }

    public static float DistanceBetweenTwoPoints(float angleBetween, Vector3 p)
    {
        return Mathf.Sin(angleBetween * Mathf.Deg2Rad) * Vector3.Magnitude(p);
    }
}
