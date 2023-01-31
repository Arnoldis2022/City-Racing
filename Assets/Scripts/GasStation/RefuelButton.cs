using UnityEngine;
using UnityEngine.UI;

public class RefuelButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _refuelButton;
    [SerializeField] private Color _emptyColor;
    [SerializeField] private Color _normalFuelColor;
    [SerializeField] private Color _fullFuelColor;
    [SerializeField] private float _baseRefuelPricePerUnit;
    [SerializeField] private InfoWindow _infoWindow;

    public void Init()
    {
        var car = _player.Car;
        RepaintButton(car.FuelQuantity, car.FuelTankCapacity);
    }

    public void TryRefuelCar()
    {
        var car = _player.Car;
        try
        {
            car.TryRefuel(_player);
            RepaintButton(car.FuelQuantity, car.FuelTankCapacity);
            Game.Instance.UpdatePlayerData(_player);
        }
        catch (PriceException exception)
        {
            _infoWindow.OpenInfoWindow(exception.Message);
        }
    }

    private void RepaintButton(float fuelQuantity, float fuelTankCapacity)
    {
        if (fuelQuantity > fuelTankCapacity * 0.5f)
        {
            _refuelButton.image.color = _fullFuelColor;
        }
        else if (fuelQuantity > fuelTankCapacity * 0.25f)
        {
            _refuelButton.image.color = _normalFuelColor;
        }
        else
        {
            _refuelButton.image.color = _emptyColor;
        }
    }
}