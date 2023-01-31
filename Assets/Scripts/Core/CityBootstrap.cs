using GleyTrafficSystem;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using YG;

public class CityBootstrap : SceneBootstrap
{
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private CarCamera _carCamera;
    [SerializeField] private CityBackground _cityBacgkround;
    [SerializeField] private MoneySpawner _moneySpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private MiniMapCamera _minimapCamera;
    [SerializeField] private CarChecker _carChecker;
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private Color _playerCarMarkerColor;
    [SerializeField] private CarMarker _playerMarkerPrefab;
    [SerializeField] private Light _light;
    [SerializeField] private TrafficComponent _trafficComponent;

    public override void Init(SavesYG savedData)
    {
        var graphicsSettings = Game.Instance.GraphicsSettings;
        graphicsSettings.SetGraphicsSettings(Camera.main, _light, _postProcessVolume);
        _trafficComponent.nrOfVehicles = graphicsSettings.TrafficDensity;

        base.Init(savedData);
        Player.Car.StartEngine();

        if (_playerSpawner != null)
            _playerSpawner.SetPlayerPosition(Player.Car);
        if (_carCamera != null)
            _carCamera.Init();
        if (_moneySpawner != null)
            _moneySpawner.Init();
        if (_minimapCamera != null)
            _minimapCamera.Init();
        if (_cityBacgkround != null)
            _cityBacgkround.Init();
        if (_carChecker != null)
            _carChecker.Init(Player);
        _gameUI?.Init();
        var carMarker = Instantiate(_playerMarkerPrefab);
        carMarker.Init(Player.Car);
        carMarker.SetColor(_playerCarMarkerColor);
        YandexGame.StickyAdActivity(false);
    }
}
