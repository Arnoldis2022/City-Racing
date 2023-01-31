using UnityEngine;

[CreateAssetMenu(menuName = "Car/Rim Asset")]
public class RimConfig : DetailConfig
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private float _defaultRimSmoothness;

    public Color DefaultColor => _defaultColor;
    public float DefaultRimSmoothness => _defaultRimSmoothness;
}