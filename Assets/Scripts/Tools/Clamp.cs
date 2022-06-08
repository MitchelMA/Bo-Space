using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Tools
{
    public static float Clamp(float x, float min, float max)
    {
        float tmp = Mathf.Max(min, x);
        return Mathf.Min(max, tmp);
    }
}
