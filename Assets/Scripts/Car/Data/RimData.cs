using UnityEngine;

[System.Serializable]
public class RimData : DetailData
{
    public DetailColor Color;

    public RimData(RimConfig rimConfig, DetailColor color) : base(rimConfig)
    {
        Color = color;
    }

    public RimData(RimConfig rimConfig, Color color) : base(rimConfig)
    {
        Color = new DetailColor(color);
    }
}