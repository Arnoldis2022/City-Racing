using System.Collections.Generic;
using UnityEngine;

public class RaceSession
{
    private RaceTrack _raceTrack;
    private Car[] _cars;
    private Dictionary<Car, BotInfo> _bots;
    private Dictionary<Car, RaceCarStatistic> _raceCarStatistics;

    public Dictionary<Car, BotInfo> Bots => _bots;
    public Dictionary<Car, RaceCarStatistic> RaceCarStatistics => _raceCarStatistics;

    public RaceSession(RaceTrack raceTrack)
    {
        _raceTrack = raceTrack;
        _cars = new Car[_raceTrack.TotalPositions];
        _raceCarStatistics = new Dictionary<Car, RaceCarStatistic>();
        _bots = new Dictionary<Car, BotInfo>();
        var checkPointManager = _raceTrack.CheckPointManager;
        foreach (var checkPoint in checkPointManager.CheckPoints)
        {
            checkPoint.CheckpointPassed += CheckpointPassed;
        }
    }

    public void AddBotToStart(BotInfo botInfo)
    {
        _bots.Add(botInfo.Car, botInfo);
        botInfo.AI.enabled = false;
        AddCarToStart(botInfo.Car);
    }

    public void AddCarToStart(Car car)
    {
        var positionIndex = GetFreePositionIndex();
        var checkPointManager = _raceTrack.CheckPointManager;
        var startPoints = _raceTrack.StartPoints;
        var startCheckPoint = checkPointManager.StartCheckPoint;
        var checkpointBeforeStart = checkPointManager.CheckpointBeforeStart;
        RaceCarStatistic carStatistic = new RaceCarStatistic(startCheckPoint, checkpointBeforeStart);
        _raceCarStatistics.Add(car, carStatistic);
        _cars[positionIndex] = car;
        _cars[positionIndex].transform.position = startPoints[positionIndex].position;
        _cars[positionIndex].transform.rotation = startPoints[positionIndex].rotation;
        _cars[positionIndex].CarController.enabled = false;
    }

    public int GetCarPosition(Car car)
    {
        var position = 0;
        foreach (var otherCar in _cars)
        {
            var targetCarDistance = _raceCarStatistics[car].GetDistanceTraveled(car.transform.position);
            var otherCarDistance = _raceCarStatistics[otherCar].GetDistanceTraveled(otherCar.transform.position);
            if (targetCarDistance < otherCarDistance)
                position++;
        }
        return position;
    }

    private int GetFreePositionIndex()
    {
        int index;
        for (index = 0; index < _cars.Length; index++)
        {
            if (_cars[index] == null)
                break;
        }
        return index;
    }

    public int GetNumberCarRacingLaps(Car car)
    {
        return _raceCarStatistics[car].NumberLapsCompletedInRace;
    }

    private void CheckpointPassed(RaceCheckPoint checkpoint, Car car)
    {
        if (checkpoint == _raceCarStatistics[car].NextCheckPoint)
        {
            var checkPointManager = _raceTrack.CheckPointManager;
            var nextCheckPoint = checkPointManager.GetNextCheckPointAfter(checkpoint);
            _raceCarStatistics[car].SetNextCheckPoint(nextCheckPoint);
        }
    }

    public void StartRace()
    {
        foreach (var car in _cars)
        {
            car.CarController.enabled = true;
            if (_bots.ContainsKey(car))
                _bots[car].ResetAI();
        }
    }
}