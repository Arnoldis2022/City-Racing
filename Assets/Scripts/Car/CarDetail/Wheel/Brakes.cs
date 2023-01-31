using UnityEngine;

public class Brakes : PaintedDetail
{
    [SerializeField] private string _brakesName;

    public override string DetailName => _brakesName;
}