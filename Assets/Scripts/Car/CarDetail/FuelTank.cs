using System;

public class FuelTank : Upgradeable
{
    public Action FuelTankIsEmpty;

    private float _fuelQuantity;
    private float _fuelTankCapacity;
    private float _fuelTankRatio;
    private float _baseFuelConsumptionRatio;
    private float _maxFuelConsumptionRatio;
    private float _baseFuelTankCapacity;
    private float _maxFuelTankCapacity;
    private int _tankLevel;
    private int _maxTankLevel;

    public float FuelQuantity => _fuelQuantity;
    public float FuelTankCapacity => _fuelTankCapacity;
    public float FuelTankRatio => _fuelTankRatio;
    public override int Level => _tankLevel;
    public override int MaxLevel => _maxTankLevel;

    public FuelTank(CarConfig config, float fuelQuantity, int tankLevel)
    {
        _tankLevel = tankLevel;
        _fuelQuantity = fuelQuantity;
        _baseFuelConsumptionRatio = config.BaseFuelConsumptionRatio;
        _maxFuelConsumptionRatio = config.MaxFuelConsumptionRatio;
        _baseFuelTankCapacity = config.BaseFuelTankCapacity;
        _maxFuelTankCapacity = config.MaxFuelTankCapacity;
        _maxTankLevel = config.MaxFuelTankLevel;
        _fuelTankCapacity = CalculateValue(_baseFuelTankCapacity, _maxFuelTankCapacity);
        _fuelTankRatio = CalculateValue(_baseFuelConsumptionRatio, _maxFuelConsumptionRatio);
    }
     
    public override void LevelUp()
    {
        if(_tankLevel < _maxTankLevel)
        {
            _tankLevel++;
            _fuelTankCapacity = CalculateValue(_baseFuelTankCapacity, _maxFuelTankCapacity);
            _fuelTankRatio = CalculateValue(_baseFuelConsumptionRatio, _maxFuelConsumptionRatio);
        }
    }

    public void UpdateFuelQuantity(float fuel)
    {
        _fuelQuantity += fuel;
        if (_fuelQuantity > _fuelTankCapacity)
        {
            _fuelQuantity = _fuelTankCapacity;
        }
        else if(_fuelQuantity <= 0f)
        {
            _fuelQuantity = 0f;
            FuelTankIsEmpty?.Invoke();
        }
    }
}