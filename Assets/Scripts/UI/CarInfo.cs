using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _carNameLabel;
    [SerializeField] private CarCharacteristicUI _speed;
    [SerializeField] private CarCharacteristicUI _fuelConsumption;
    [SerializeField] private CarCharacteristicUI _controllability;
    [SerializeField] private CarCharacteristicUI _brakes;
    private float _maxFuelConsumption;
    private float _maxSpeed;
    private float _maxBrakeForce;
    private float _maxSteerSpeed;


    private void Awake()
    {
        var cars = Game.Instance.GameDataBase.Cars;
        foreach(var car in cars)
        {
            if(car.BaseSpeed > _maxSpeed)
                _maxSpeed = car.BaseSpeed;
            if(car.BaseFuelConsumptionRatio > _maxFuelConsumption)
                _maxFuelConsumption = car.BaseFuelConsumptionRatio;
            if(car.BaseBrakeForce > _maxBrakeForce)
                _maxBrakeForce = car.BaseBrakeForce;
            if(car.MaxSteerSpeed > _maxSteerSpeed)
                _maxSteerSpeed = car.MaxSteerSpeed;
        }
    }

    public void Init(CarConfig carConfig)
    {
        _carNameLabel.text = carConfig.CarName;
        _speed.Init(carConfig.BaseSpeed, _maxSpeed);
        _fuelConsumption.Init(carConfig.BaseFuelConsumptionRatio, _maxFuelConsumption);
        _brakes.Init(carConfig.BaseBrakeForce, _maxBrakeForce);
        _controllability.Init(carConfig.MaxSteerSpeed, _maxSteerSpeed);
    }
}
