using UnityEngine;

public class CarBody : PaintedDetail
{
    [SerializeField] private string _bodyName;

    public override string DetailName => _bodyName;
}
