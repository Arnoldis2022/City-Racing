using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Car Asset")]
public class CarConfig : ScriptableObject
{
    [SerializeField] private string _carName;

    [Header("Core of the car")]
    [SerializeField] private Car _carPrefab;
    [Range(0, 5)]
    [SerializeField] private int _carClass;
    [SerializeField] private Color _defaultBodyColor;
    [SerializeField] private float _defaultBodySmoothness;
    [SerializeField] private RimConfig _defaultRim;
    [SerializeField] private TireConfig _defaultTire;
    [SerializeField] private SpoilerConfig _defaultSpoiler;
    [SerializeField] private List<RimConfig> _supportedRims;
    [SerializeField] private List<TireConfig> _supportedTires;
    [SerializeField] private List<SpoilerConfig> _supportedSpoilers;
    [SerializeField] private Vector3 _wheelSize = Vector3.one;
    [SerializeField] private Vector3 _spoilerSize = Vector3.one;
    [SerializeField] private float _price;

    [Header("Fuel tank parameters")]
    [SerializeField] private float _baseFuelConsumptionRatio;
    [SerializeField] private float _maxFuelConsumptionRatio;
    [SerializeField] private float _baseFuelTankCapacity;
    [SerializeField] private float _fuelPrice;
    [SerializeField] private float _maxFuelTankCapacity;
    [SerializeField] private int _maxFuelTankLevel;

    [Header("Engine parameters")]
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _baseEngineTorque;
    [SerializeField] private float _maxEngineTorque;
    [SerializeField] private int _maxEngineLevel;

    [Header("Brakes parameters")]
    [SerializeField] private float _baseBrakeForce;
    [SerializeField] private float _maxBrakeForce;
    [SerializeField] private int _maxBrakesLevel;

    [Header("Controllability parameters")]
    [SerializeField] private float _baseSteerAngleAtSpeed;
    [SerializeField] private float _maxSteerAngleAtSpeed;
    [SerializeField] private float _baseSteerSpeed;
    [SerializeField] private float _maxSteerSpeed;
    [SerializeField] private int _maxControllabilityLevel;

    [Header("NOS parameters")]
    [SerializeField] private float _baseNOSCapacity;
    [SerializeField] private float _maxNOSCapacity;
    [SerializeField] private int _maxNOSLevel;

    public string CarName => _carName; 
    public Car CarPrefab => _carPrefab;
    public int Class => _carClass;
    public Color DefaultBodyColor => _defaultBodyColor;
    public float DefaultBodySmoothness => _defaultBodySmoothness;
    public RimConfig DefaultRim => _defaultRim;
    public TireConfig DefaultTire => _defaultTire;
    public SpoilerConfig DefaultSpoiler => _defaultSpoiler;
    public List<RimConfig> SupportedRims => _supportedRims;
    public List<TireConfig> SupportedTires => _supportedTires;
    public List<SpoilerConfig> SupportedSpoilers => _supportedSpoilers;
    public Vector3 WheelSize => _wheelSize;
    public Vector3 SpoilerSize => _spoilerSize;

    public float BaseFuelConsumptionRatio => _baseFuelConsumptionRatio;
    public float MaxFuelConsumptionRatio => _maxFuelConsumptionRatio;
    public float BaseFuelTankCapacity => _baseFuelTankCapacity;
    public float FuelPrice => _fuelPrice;
    public float MaxFuelTankCapacity => _maxFuelTankCapacity;
    public int MaxFuelTankLevel => _maxFuelTankLevel;

    public float BaseSpeed => _baseSpeed;
    public float MaxSpeed => _maxSpeed;
    public float BaseEngineTorque => _baseEngineTorque;
    public float MaxEngineTorque => _maxEngineTorque;
    public int MaxEngineLevel => _maxEngineLevel;

    public float BaseBrakeForce => _baseBrakeForce;
    public float MaxBrakeForce => _maxBrakeForce;
    public int MaxBrakesLevel => _maxBrakesLevel;

    public float BaseSteerAngleAtSpeed => _baseSteerAngleAtSpeed;
    public float MaxSteerAngleAtSpeed => _maxSteerAngleAtSpeed;
    public float BaseSteerSpeed => _baseSteerSpeed;
    public float MaxSteerSpeed => _maxSteerSpeed;
    public int MaxControllabilityLevel => _maxControllabilityLevel;


    public float BaseNOSCapacity => _baseNOSCapacity;
    public float MaxNOSCapacity => _maxNOSCapacity;
    public int MaxNOSLevel => _maxNOSLevel;

    public float Price => _price;


}