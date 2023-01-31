using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wheelbase : MonoBehaviour
{
    [SerializeField] private Wheel _frontRightWheel;
    [SerializeField] private Wheel _frontLeftWheel;
    [SerializeField] private Wheel _rearRightWheel;
    [SerializeField] private Wheel _rearLeftWheel;
    [SerializeField] private WheelCollider _frontRightWheelColliders;
    [SerializeField] private WheelCollider _frontLeftWheelColliders;
    [SerializeField] private WheelCollider _rearRightWheelColliders;
    [SerializeField] private WheelCollider _rearLeftWheelColliders;
    private List<Wheel> _wheels;
    private RimConfig _currentRim;
    private TireConfig _currentTire;
    private Color _rimsColor;
    private Vector3 _wheelsSize;
    private Dictionary<string, RimData> _availableRims;
    private Dictionary<string, TireData> _availableTires;

    public List<Wheel> Wheels => _wheels;
    public Wheel FrontRightWheel => _frontRightWheel;
    public Wheel FrontLeftWheel => _frontLeftWheel;
    public Wheel RearRightWheel => _rearRightWheel;
    public Wheel RearLeftWheel => _rearLeftWheel;
    public WheelCollider FrontRightWheelColliders => _frontRightWheelColliders;
    public WheelCollider FrontLeftWheelColliders => _frontLeftWheelColliders;
    public WheelCollider RearRightWheelColliders => _rearRightWheelColliders;
    public WheelCollider RearLeftWheelColliders => _rearLeftWheelColliders;
    public Dictionary<string, RimData> AvailableRims => _availableRims;
    public Dictionary<string, TireData> AvailableTires => _availableTires;
    public string RimID => _currentRim.name;
    public string TireID => _currentTire.name;
    public RimConfig CurrentRim => _currentRim;
    public TireConfig CurrentTire => _currentTire;
    public Vector3 WheelsSize => _wheelsSize;
    public Color RimsColor => _frontRightWheel.RimPlace.CurrentRim.Color;
    public float RimsMaterialSmoothness => _frontRightWheel.RimPlace.CurrentRim.MaterialSmoothness;

    public void Init(List<RimData> availableRims, List<TireData> availableTires)
    {
        _availableRims = DetailsDataToDictionary(availableRims);
        _availableTires = DetailsDataToDictionary(availableTires);
        _wheels = new List<Wheel>()
        {
            _frontRightWheel,
            _frontLeftWheel,
            _rearRightWheel,
            _rearLeftWheel
        };
    }

    private Dictionary<string, T> DetailsDataToDictionary<T>(List<T> detailsList) where T : DetailData
    {
        var detailsData = new Dictionary<string, T>();
        foreach(var detailData in detailsList)
        {
            detailsData.Add(detailData.Id, detailData);
        }
        return detailsData;
    }

    public void AddRim(RimConfig rimConfig)
    {
        RimData rimData = new RimData(rimConfig, rimConfig.DefaultColor);
        _availableRims.Add(rimData.Id, rimData);
    }

    public void AddTire(TireConfig tireConfig)
    {
        TireData tireData = new TireData(tireConfig);
        _availableTires.Add(tireData.Id, tireData);
    }

    public void CreateWheels(TireConfig tireConfig, RimConfig rimConfig, Vector3 wheelSize, RimData rimData)
    {
        _currentRim = rimConfig;
        _currentTire = tireConfig;
        _wheelsSize = wheelSize;
        _rimsColor = rimData.Color.ToColor();
        foreach(var wheel in _wheels)
        {
            wheel.CreateWheel(tireConfig, rimConfig);
            wheel.transform.localScale = wheelSize;
            wheel.RimPlace.CurrentRim.SetColor(_rimsColor, rimData.Color.Smoothness);
        }
    }

    public void CreateWheels(TireConfig tireConfig)
    {
        _currentTire = tireConfig;
        foreach(var wheel in _wheels)
        {
            wheel.SetTire(tireConfig);
            wheel.ResetRimSize();
        }
    }

    public void SetRimsColor(Color color)
    {
        _rimsColor = color;
        DetailColor currentColor = _availableRims[CurrentRim.name].Color;
        _availableRims[CurrentRim.name].Color = new DetailColor(color, currentColor.Smoothness);
        foreach (var wheel in _wheels)
        {
            wheel.RimPlace.CurrentRim.SetColor(color, currentColor.Smoothness);
        }
    }
}
