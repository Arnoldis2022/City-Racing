using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private TirePlace _tirePlace;
    [SerializeField] private RimPlace _rimPlace;
    [SerializeField] private Brakes _brakes;
    private float _rimWidth = 1f;
    private float _rimRadius = 1f;

    public Transform WheelTransform => _wheelTransform;
    public TirePlace TirePlace => _tirePlace;
    public RimPlace RimPlace => _rimPlace;
    public Brakes Brakes => _brakes;

    public void CreateWheel(TireConfig tireAsset, RimConfig rimAsset)
    {
        SetTire(tireAsset);
        SetRim(rimAsset);
    }

    public void SetTire(TireConfig tireAsset)
    {
        _rimWidth = tireAsset.Prefab.transform.localScale.x * tireAsset.DiscWidth;
        _rimRadius = tireAsset.Prefab.transform.localScale.y * tireAsset.DiscRadius;
        _tirePlace.CreateTire(tireAsset.Prefab as Tire);
    }

    public void SetRim(RimConfig rimAsset)
    {
        _rimPlace.CreateRim(rimAsset.Prefab as Rim, _rimWidth, _rimRadius);
    }

    public void ResetRimSize()
    {
        _rimPlace.SetRimSize(_rimWidth, _rimRadius);
    }
}