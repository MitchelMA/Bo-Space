using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Tools
{
    public static float Map(float x, float xMin, float xMax, float newXMin, float newXMax)
    {
        float oldDiff = xMax - xMin;
        float percentage = (x - xMin) / oldDiff;

        float newDiff = newXMax - newXMin;
        float newValue = percentage * newDiff + newXMin;
        return newValue;
    }
}
