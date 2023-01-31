using UnityEngine;
using YG;

public abstract class SceneBootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SoundManager _soundManager;
    private PlayerData _playerData;

    public Player Player => _player;

    //private void Start()
    //{
    //    Game.Instance.ConfigureScene(this);
    //}

    public virtual void Init(SavesYG savedData)
    {
        if (_playerData != null)
            return;

        _playerData = savedData.PlayerData;

        if (_playerData == null)
            _playerData = new PlayerData(Player.Config);

        var carConfig = Game.Instance.GetCarConfig(_playerData.CurrentCarID);
        CarData carData = Game.Instance.GetCarData(carConfig);

        if (carData == null)
            carData = new CarData(carConfig);

        var car = CreateCar(carData, carConfig);
        car.EngineSoundVolume(Game.Instance.SoundEffectsVolume);
        Game.Instance.UpdateCarData(car);
        Player.Init(_playerData, car);
        if(_soundManager != null)
            _soundManager.Init();
    }

    private Car CreateCar(CarData data, CarConfig config)
    {
        var playerTransform = _player.transform;
        var car = Instantiate(config.CarPrefab, playerTransform.position, playerTransform.rotation, playerTransform);
        car.ConfigureCar(data);
        return car;
    }
}
