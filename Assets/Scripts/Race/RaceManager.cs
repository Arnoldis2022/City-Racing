using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public Action InitCompleted;
    public Action<int> ÑountdownPerSeconds;
    public Action Started;
    public Action RaceIsOver;
    public Action RaceStoped;
    public Action UpdateRaceData;

    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private List<RaceTrigger> _raceTriggers;
    [SerializeField] private float _botsViewDistance = 10f;
    [SerializeField] private int _botsNextWaypointPassDistance = 10;
    [SerializeField] private float _botsRaycastAngle = 15f;
    [SerializeField] private CarMarker _carMarkerPrefab;
    private List<CarMarker> _carMarkers;
    private int _playerPosition;
    private float _playerTime;
    private RaceTrack _raceTrack;
    private bool _raceIsActive;
    private RaceSession _raceSession;

    public RaceTrack RaceTrack => _raceTrack;
    public int PlayerPosition => _playerPosition;
    public float PlayerTime => _playerTime;
    public float PlayerTimePerMilliseconds => _playerTime * 1000f;
    public bool RaceIsActive => _raceIsActive;

    public void CreateRaceSession(RaceTrack raceTrack)
    {
        _raceSession = new RaceSession(raceTrack);
        _carMarkers = new List<CarMarker>();
        _raceTrack = raceTrack;
        CreateBots();
        _player.Car.StopCar();
        _raceSession.AddCarToStart(_player.Car);
        GleyTrafficSystem.Manager.SetTrafficDensity(0);
        GleyTrafficSystem.Manager.ClearTrafficOnArea(_player.transform.position, 800f);
        InitCompleted?.Invoke();
    }

    private void CreateBots()
    {
        var cars = Game.Instance.GameDataBase.Cars;
        var requiredClass = _player.Car.Class;
        var availableCars = cars.Where(car => car.Class >= requiredClass - 1 && car.Class <= requiredClass + 1).ToList();
        var carsQuantity = availableCars.Count;
        for (int i = 0; i < _raceTrack.TotalPositions - 1; i++)
        {
            var carIndex = UnityEngine.Random.Range(0, carsQuantity);
            var car = CreateBotCar(availableCars[carIndex]);
            var bot = CreateBotInfo(car);
            var carMarker = Instantiate(_carMarkerPrefab);
            carMarker.Init(car);
            _carMarkers.Add(carMarker);
            _raceSession.AddBotToStart(bot);
        }
    }

    private Car CreateBotCar(CarConfig carConfig)
    {
        var car = Instantiate(carConfig.CarPrefab);
        CarData carData = CarDataRandomizer.GenerateCarData(car.Config);
        car.ConfigureCar(carData);
        return car;
    }

    private BotInfo CreateBotInfo(Car car)
    {
        BotInfo bot = new BotInfo();
        bot.Car = car;
        bot.AI = car.gameObject.AddComponent<RCC_AICarController>();
        bot.AI.waypointsContainer = _raceTrack.IntersectionPoints;
        bot.AI.raycastLength = _botsViewDistance;
        bot.AI.nextWaypointPassDistance = _botsNextWaypointPassDistance;
        bot.AI.raycastAngle = _botsRaycastAngle;
        bot.AI.obstacleLayers = _obstacleLayers;
        bot.ResetAI();
        return bot;
    }

    public void StartRace()
    {
        _raceTriggers.ForEach(trigger => trigger.gameObject.SetActive(false));
        _raceIsActive = true;
        StartCoroutine(ÑountdownCoroutine());
    }

    private IEnumerator ÑountdownCoroutine()
    {
        float timer = 0f;
        while(timer % 60 <= 3f)
        {
            timer += Time.deltaTime;
            yield return null;
            ÑountdownPerSeconds?.Invoke((int)timer % 60);
        }
        Started?.Invoke();
        StartCoroutine(RaceCoroutine());
        _raceSession.StartRace();
    }

    private IEnumerator RaceCoroutine()
    {
        int lapsQuantity = 0;
        while (lapsQuantity < _raceTrack.NumberRaceLaps && _raceIsActive)
        {
            lapsQuantity = GetNumberLapsPlayer();
            _playerTime += Time.deltaTime;
            yield return null;
            UpdateRaceData?.Invoke();
        }
        EndRace();
    }

    public void EndRace()
    {
        RaceIsOver.Invoke();
        ResetRace();
    }

    public void StopRace()
    {
        StopAllCoroutines();
        ResetRace();
        RaceStoped?.Invoke();
    }

    public void ResetRace()
    {
        _raceIsActive = false;
        GleyTrafficSystem.Manager.SetTrafficDensity(Game.Instance.GraphicsSettings.TrafficDensity);
        _playerTime = 0f;
        _playerPosition = 0;
        _carMarkers.ForEach(carMarker => Destroy(carMarker.gameObject));
        _carMarkers.Clear();
        DeleteBots();
        _raceTrack.gameObject.SetActive(false);
        _raceTriggers.ForEach(trigger => trigger.gameObject.SetActive(true));
    }

    public int GetNumberLapsPlayer()
    {
        return _raceSession.GetNumberCarRacingLaps(_player.Car);
    }

    public void DeleteBots()
    {
        foreach (var bot in _raceSession.Bots)
        {
            var car = bot.Value.Car;
            Destroy(car.gameObject);
        }
    }

    public int GetPlayerPosition()
    {
        _playerPosition = _raceSession.GetCarPosition(_player.Car);
        return _playerPosition;
    }
}
