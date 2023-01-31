using UnityEngine;

public class Spoiler : PaintedDetail
{
    [SerializeField] private string _spoilerName;
    
    public override string DetailName => _spoilerName;
}
