using System;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Action<Car> CarDestroing;
    public Action CarRefueld;
    public Action<Collision> Collision;

    [SerializeField] private CarBody _body;
    [SerializeField] private Wheelbase _wheelbase;
    [SerializeField] private SpoilerPlace _spoilerPlace;
    [SerializeField] private CarConfig _config;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private RCC_CarControllerV3 _carController;
    private CarBrakes _brakes;
    private CarControllability _controllability;
    private CarEngine _engine;
    private FuelTank _fuelTank;
    private NitrousOxideSystem _nos;
    private float _fuelPrice;

    public string CarName => _config.CarName;
    public Rigidbody Rigidbody => _rigidbody;
    public RCC_CarControllerV3 CarController => _carController;
    public CarBody Body => _body;
    public CarBrakes Brakes => _brakes;
    public CarControllability Controllability => _controllability;
    public NitrousOxideSystem NOS => _nos;
    public CarConfig Config => _config;
    public CarEngine Engine => _engine;
    public FuelTank FuelTank => _fuelTank;
    public Wheelbase Wheelbase => _wheelbase;
    public float Speed => _carController.speed;
    public int Class => _config.Class;
    public SpoilerPlace SpoilerPlace => _spoilerPlace;
    public float FuelQuantity => _carController.fuelTank;
    public bool IsDrifting => _carController.driftingNow;
    public float DriftAngle => _carController.driftAngle;
    public float FuelTankCapacity => _fuelTank.FuelTankCapacity;
    public int FuelTankLevel => _fuelTank.Level;
    public void KillEngine() => _carController.KillEngine();
    public void StartEngine() => _carController.StartEngine();

    public void ConfigureCar(CarData carData)
    {
        _fuelTank = new FuelTank(_config, carData.FuelQuantity, carData.FuelTankLevel);
        _engine = new CarEngine(_config, carData.EngineLevel);
        _brakes = new CarBrakes(_config, carData.BrakesLevel);
        _controllability = new CarControllability(_config, carData.ControllabilityLevel);
        _nos = new NitrousOxideSystem(_config, carData.NOSLevel);
        _fuelPrice = _config.FuelPrice;
        _carController.highspeedsteerAngle = _controllability.SteerAngleAtSpeed;
        _carController.highspeedsteerAngleAtspeed = _controllability.SteerSpeed;
        _carController.maxspeed = _engine.Speed;
        _carController.maxEngineTorque = _engine.EngineTorque;
        _carController.fuelConsumptionRate = _fuelTank.FuelTankRatio;
        _carController.fuelTankCapacity= _fuelTank.FuelTankCapacity;
        _carController.brakeTorque = _brakes.BrakeForce;
        _carController.useNOS = _nos.Level > 0;
        _carController.NoS = _nos.NOSCapacity;
        _carController.fuelTank = _fuelTank.FuelQuantity;
        _carController.InitGears();
        ConfigureBody(carData);
        ConfigureWheels(carData);
        ConfigureSpoiler(carData);
    }

    private void ConfigureBody(CarData carData)
    {
        var bodyColor = carData.Color.ToColor();
        var bodySmoothness = carData.Color.Smoothness;
        _body.SetColor(bodyColor, bodySmoothness);
    }

    private void ConfigureWheels(CarData carData)
    {
        var rimAsset = _config.SupportedRims.FirstOrDefault(suitableRim => suitableRim.name == carData.RimID);
        var tireAsset = _config.SupportedTires.FirstOrDefault(suitableTire => suitableTire.name == carData.TireID);
        _wheelbase.Init(carData.AvailableRims, carData.AvailableTires);
        var rimColor = _wheelbase.AvailableRims[rimAsset.name];
        _wheelbase.CreateWheels(tireAsset, rimAsset, _config.WheelSize, rimColor);
    }

    private void ConfigureSpoiler(CarData carData)
    {
        var spoiler = _config.SupportedSpoilers.FirstOrDefault(suitableSpoiler => suitableSpoiler.name == carData.SpoilerID);
        _spoilerPlace.Init(carData.AvailableSpoilers);
        _spoilerPlace.CreateSpoiler(spoiler, _config.SpoilerSize, _body.Color, _body.MaterialSmoothness);
    }

    public void EngineSoundVolume(float volume)
    {
        _carController.minEngineSoundVolume = _carController.baseMinEngineSoundVolume * volume;
        _carController.maxEngineSoundVolume = _carController.baseMaxEngineSoundVolume * volume;
        _carController.idleEngineSoundVolume = _carController.baseIdleEngineSoundVolume * volume;
    }

    public float CalculatePriceForRefueling(Player player)
    {
        var requiredFuel = FuelTankCapacity - FuelQuantity;
        return requiredFuel * _fuelPrice;
    }

    public void StopCar()
    {
        Rigidbody.velocity = Vector3.zero;
    }

    public bool TryRefuel(Player player)
    {
        if (player.Credits < _fuelPrice)
            throw new PriceException();
        var requiredFuel = FuelTankCapacity - FuelQuantity;
        var amountFuelPlayerCanBuy = Mathf.Floor(player.Credits / _fuelPrice);
        var isRefuel = false;

        if (amountFuelPlayerCanBuy >= requiredFuel)
        {
            var requiredCredits = requiredFuel * _fuelPrice;
            player.TryDecreaseCredits(requiredCredits);
            _carController.fuelTank = FuelTankCapacity;
            isRefuel = true;
        }
        else if (amountFuelPlayerCanBuy < requiredFuel)
        {
            var requiredCredits = amountFuelPlayerCanBuy * _fuelPrice;
            player.TryDecreaseCredits(requiredCredits);
            _carController.fuelTank += amountFuelPlayerCanBuy;
            isRefuel = true;
        }
        _fuelTank.UpdateFuelQuantity(_carController.fuelTank);
        CarRefueld?.Invoke();
        return isRefuel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collision?.Invoke(collision);
    }
}



