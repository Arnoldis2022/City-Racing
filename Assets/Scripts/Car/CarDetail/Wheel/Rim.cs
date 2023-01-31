using UnityEngine;

public class Rim : PaintedDetail
{
    [SerializeField] private string _rimName;
    [SerializeField] private Color _baseColor = Color.gray;

    public override string DetailName => _rimName;
    public Color BaseColor => _baseColor;
}