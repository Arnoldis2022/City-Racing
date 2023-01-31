using UnityEngine;

public class GasStationConfig : MonoBehaviour
{
    [SerializeField] private float _basePricePerUnitFuel;

    public float CalculateRefuelPrice(Car car)
    {
        var carClass = car.Class;
        var requiredFuel = car.FuelTankCapacity - car.FuelQuantity;
        var basePrice = requiredFuel * _basePricePerUnitFuel;
        var valueIncrease = basePrice * (carClass / 2);
        return basePrice + valueIncrease;
    }
}
