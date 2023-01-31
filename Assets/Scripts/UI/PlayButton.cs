using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _cityLevelIndex;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private Button _button;

    public void Play()
    {
        var fuelInPercent = _player.Car.FuelQuantity / _player.Car.FuelTankCapacity;
        if(fuelInPercent < 0.1f)
        {
            var message = Game.Instance.Localization.GetText("{ui_error_fuel}");
            _infoWindow.OpenInfoWindow(message);
        }
        else
        {
            _button.interactable = false;
            SceneLoader.SwitchToScene(_cityLevelIndex);
        }
    }
}
