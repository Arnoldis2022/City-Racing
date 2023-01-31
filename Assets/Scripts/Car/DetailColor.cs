using UnityEngine;

[System.Serializable]
public struct DetailColor
{
    public float R;
    public float G;
    public float B;
    public float Metalic;
    public float Smoothness;

    public DetailColor(Color color, float metalic = 1f, float smoothness = 1f)
    {
        R = color.r;
        G = color.g;
        B = color.b;
        Metalic = metalic;
        Smoothness = smoothness;
    }

    public Color ToColor()
    {
        return new Color(R, G, B);
    }
}