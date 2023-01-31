using System;
using UnityEngine;

public class GasStation : MonoBehaviour
{
    public Action<Player> PlayerCarEnter;
    public Action RefuelComplete;
    public Action<string> RefuelError;
    public Action PlayerCarExit;
    public Action PlayerCarTeleported;

    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerTeleportTransform;
    [SerializeField] private float _teleportPrice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.Car.gameObject)
        {
            PlayerCarEnter?.Invoke(_player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player.Car.gameObject)
        {
            PlayerCarExit?.Invoke();
            TryRefuelCar();
        }
    }

    public bool TryRefuelCar()
    {
        try
        {
            var car = _player.Car;
            var isRefuel = car.TryRefuel(_player);
            if (isRefuel)
                RefuelComplete?.Invoke();
            return isRefuel;
        }
        catch(PriceException exception)
        {
            RefuelError?.Invoke(exception.Message);
            return false;
        }
    }

    public float CalculateTeleportPrice(Player player)
    {
        var car = player.Car;
        var valueIncrease = _teleportPrice * (car.Class / 2f);
        var refuelPrice = car.CalculatePriceForRefueling(player);
        var requiredCredits = _teleportPrice + valueIncrease + refuelPrice;
        return requiredCredits;
    }

    public void TryTeleportCar(Player player)
    {
        var requiredPrice = CalculateTeleportPrice(player);
        if(player.Credits >= requiredPrice)
        {
            var car = player.Car;
            var carTransform = car.transform;
            TryRefuelCar();
            player.TryDecreaseCredits(_teleportPrice);
            player.Car.StopCar();
            carTransform.position = _playerTeleportTransform.position;
            carTransform.rotation = _playerTeleportTransform.rotation;
            PlayerCarTeleported?.Invoke();
        }
        else
        {
            throw new PriceException();
        }
    }
}
