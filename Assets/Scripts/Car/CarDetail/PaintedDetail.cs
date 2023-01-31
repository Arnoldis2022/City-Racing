
using UnityEngine;

public abstract class PaintedDetail : Detail
{
    public const float MatteSmoothness = 0.5f;
    public const float GlossySmoothness = 1f;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private int _materialIndex;
    private string _smoothnessChannelName = "_Glossiness";
    private bool _isMatte;
    private bool _isGlossy;

    public Material Material => _meshRenderer.materials[_materialIndex];
    public Color Color => Material.color;
    public float MaterialSmoothness => Material.GetFloat(_smoothnessChannelName);
    public bool IsMatte => _isMatte;
    public bool IsGlossy => _isGlossy;


    public void SetColor(Color color, float smoothness)
    {
        Material.color = color;
        if(smoothness == 0.5f)
            SetMatte();
        else
            SetGlossy();
    }

    public void SetMatte()
    {
        Material.SetFloat(_smoothnessChannelName, MatteSmoothness);
        _isGlossy = false;
        _isMatte = true;
    }

    public void SetGlossy()
    {
        Material.SetFloat(_smoothnessChannelName, GlossySmoothness);
        _isGlossy = true;
        _isMatte = false;
    }
}