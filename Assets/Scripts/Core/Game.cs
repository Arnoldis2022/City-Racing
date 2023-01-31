using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    public UnityEvent GameDataIsLoaded;

    [SerializeField] private GameDataBase _gameDataBase;
    private Localization _localization;
    private SavesYG _savedData;
    private Dictionary<string, CarData> _carsData;
    private Dictionary<string, RaceData> _racesData;

    public LocalizationTexts Localization => _localization.CurrentLocalization;
    public float MusicVolume => _savedData.musicVolume;
    public float SoundEffectsVolume => _savedData.soundEffectsVolume;
    public int GraphicsQualityIndex => _savedData.graphicsQualityIndex;
    public GameDataBase GameDataBase => _gameDataBase;
    public GraphicsSettings GraphicsSettings => _gameDataBase.GraphicsSettings[GraphicsQualityIndex];
    public bool IsFirstSession => _savedData.isFirstSession;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            YandexGame.LoadProgress();
            YandexGame.GetDataEvent += InitGameData;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Instance.FindSceneBootstrap();
            GameDataIsLoaded?.Invoke();
        }
    }

    private void InitGameData()
    {
        _savedData = YandexGame.savesData;
        _localization = new Localization(_gameDataBase.LocalizationTexts);
        _localization.SwitchLanguage(_savedData.language);
        _carsData = new Dictionary<string, CarData>();
        _racesData = new Dictionary<string, RaceData>();
        if (_savedData.CarsData != null)
            CreateCarsData();
        if (_savedData.RacesData != null)
            CreateRacesData();
        QualitySettings.SetQualityLevel(GraphicsQualityIndex);
        FindSceneBootstrap();
        GameDataIsLoaded?.Invoke();
    }

    private void CreateCarsData()
    {
        var carsData = _savedData.CarsData;
        foreach (var carData in carsData)
        {
            _carsData.Add(carData.Id, carData);
        }
    }

    private void CreateRacesData()
    {
        var racesData = _savedData.RacesData;  
        foreach(var raceData in racesData)
        {
            _racesData.Add(raceData.RaceName, raceData);
        }
    }

    private void FindSceneBootstrap()
    {
        var bootstrap = FindObjectOfType<SceneBootstrap>();
        bootstrap.Init(_savedData);
    }

    public void SaveData()
    {
        _savedData.isFirstSession = false;
        _savedData.language = YandexGame.savesData.language;
        _savedData.feedbackDone = YandexGame.savesData.feedbackDone;
        _savedData.promptDone = YandexGame.savesData.promptDone;
        _savedData.CarsData = new List<CarData>();
        _savedData.RacesData = new List<RaceData>();
        foreach(var carData in _carsData)
        {
            _savedData.CarsData.Add(carData.Value);
        }
        foreach(var raceData in _racesData)
        {
            _savedData.RacesData.Add(raceData.Value);
        }
        YandexGame.savesData = _savedData;
        YandexGame.SaveProgress();
    }

    public void UpdatePlayerData(Player player)
    {
        _savedData.PlayerData = new PlayerData(player);
        UpdateCarData(player.Car);
    }

    public void UpdateCarData(Car car)
    {
        CarData carData = new CarData(car);
        CarConfig carConfig = car.Config;
        if (_carsData.ContainsKey(carConfig.name))
            _carsData[carConfig.name] = carData;
        else
            _carsData.Add(carConfig.name, carData);
    }

    public void UpdateGarageData(Garage garage)
    {
        _savedData.GarageData = new GarageData(garage);
    }

    public void SwitchLanguage(string language)
    {
        _localization.SwitchLanguage(language);
    }

    public void ConfigureScene(SceneBootstrap bootstrap)
    {
        bootstrap.Init(_savedData);
    }

    public CarConfig GetCarConfig(string carID)
    {
        var carsConfig = _gameDataBase.Cars;
        foreach (var config in carsConfig)
        {
            if (config.name == carID)
                return config;
        }
        throw new ArgumentException("The car does not exist by the specified ID!");
    }

    public void UpdateGraphicsSettingsData(int qualityIndex)
    {
        _savedData.graphicsQualityIndex = qualityIndex;
    }

    public void UpdateSoundEffectsVolumeData(float volume)
    {
        _savedData.soundEffectsVolume = volume;
    }

    public void UpdateMusicVolumeData(float volume)
    {
        _savedData.musicVolume = volume;
    }

    public bool TryUpdateRaceData(RaceData raceData)
    {
        if (_racesData.ContainsKey(raceData.RaceName))
        {
            var currentRaceData = _racesData[raceData.RaceName];
            if (currentRaceData.PlayerTimeRecord > raceData.PlayerTimeRecord)
            {
                _racesData[raceData.RaceName] = raceData;
                return true;
            }
            return false;
        }
        else
        {
            _racesData.Add(raceData.RaceName, raceData);
            return true;
        }
    }

    public CarData GetCarData(CarConfig carConfig)
    {
        if (!_carsData.ContainsKey(carConfig.name))
            return null;

        return _carsData[carConfig.name];
    }

    public RaceData GetRaceData(string raceName)
    {
        if (!_racesData.ContainsKey(raceName))
            return null;

        return _racesData[raceName];
    }
}