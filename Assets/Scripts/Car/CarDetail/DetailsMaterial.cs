using System;
using UnityEngine;

[Serializable]
public struct DetailsMaterial
{
    public Color BodyColor;
    public float BodySmoothness;
    public Color RimColor;
    public float RimSmoothness;

    public DetailsMaterial(Color bodyColor, Color rimColor, float bodySmoothness = 1f, float rimSmoothness = 1f)
    {
        BodyColor = bodyColor;
        BodySmoothness = bodySmoothness;
        RimColor = rimColor;
        RimSmoothness = rimSmoothness;
    }
}