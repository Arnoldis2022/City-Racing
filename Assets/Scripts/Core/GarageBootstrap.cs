using UnityEngine;
using YG;

public class GarageBootstrap : SceneBootstrap
{
    [SerializeField] private Garage _garage;
    [SerializeField] private GarageUI _garageUI;
    [SerializeField] private GameObject _tutorial;
    private GarageData _garageData;

    public override void Init(SavesYG savedData)
    {
        base.Init(savedData);
        Player.Car.KillEngine();
        if (_garageData != null)
            return;
        _garageData = savedData.GarageData;
        if(_garageData == null)
            _garageData = new GarageData();
        _garage?.Init(_garageData);
        _garageUI.Init();
        _tutorial.SetActive(Game.Instance.IsFirstSession);
        Player.Car.CarController.canControl = false;
        YandexGame.StickyAdActivity(true);
    }
}
