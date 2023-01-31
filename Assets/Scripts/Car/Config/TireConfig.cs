using UnityEngine;

[CreateAssetMenu(menuName = "Car/Tire Data")]
public class TireConfig : DetailConfig
{
    [Range(0f, 1f)]
    [SerializeField] private float _discRadiusInPercent = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _discWidthInPercent = 1f;

    public float DiscRadius => _discRadiusInPercent;
    public float DiscWidth => _discWidthInPercent;
}