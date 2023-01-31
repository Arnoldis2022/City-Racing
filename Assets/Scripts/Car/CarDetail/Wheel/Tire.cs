using UnityEngine;

public class Tire : Detail
{
    [SerializeField] private string _tireName = "Tire";

    public override string DetailName => _tireName;
    [Range(0f, 1f)]
    [SerializeField] private float _discRadius = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _discWidth = 1f;
    public float DiscRadius => _discRadius;
    public float DiscWidth => _discWidth;
}