using System;
using UnityEngine;

public class RaceCarStatistic
{
    public Action<int> RaceLapIsCompleted;
    
    private RaceCheckPoint _previousCheckPoint;
    private RaceCheckPoint _nextCheckPoint;
    private RaceCheckPoint _startCheckPoint;
    private float _distanceTraveled;
    private int _checkpointCounter;
    private int _numberLapsCompletedInRace;

    public RaceCheckPoint PreviousCheckPoint => _previousCheckPoint;
    public RaceCheckPoint NextCheckPoint => _nextCheckPoint;
    public RaceCheckPoint StartCheckPoint => _startCheckPoint;
    public float DistanceTraveled => _distanceTraveled;
    public int CheckpointCounter => _checkpointCounter;
    public int NumberLapsCompletedInRace => _numberLapsCompletedInRace;

    public RaceCarStatistic(RaceCheckPoint startCheckPoint, RaceCheckPoint checkpointBeforeStart)
    {
        _previousCheckPoint = checkpointBeforeStart;
        _startCheckPoint = startCheckPoint;
        _nextCheckPoint = startCheckPoint;
        _distanceTraveled = 0;
        _checkpointCounter = 0;
        _numberLapsCompletedInRace = 0;
    }

    public void SetNextCheckPoint(RaceCheckPoint nextCheckPoint)
    {
        if(nextCheckPoint != _nextCheckPoint && nextCheckPoint != _previousCheckPoint)
        {
            var previousCheckPointPostion = _previousCheckPoint.transform.position;
            var nextCheckPointPostion = _nextCheckPoint.transform.position;
            if(_nextCheckPoint == _startCheckPoint && _checkpointCounter > 0)
            { 
                _numberLapsCompletedInRace++;
                RaceLapIsCompleted?.Invoke(_numberLapsCompletedInRace);
            }
            _distanceTraveled += Vector3.Distance(previousCheckPointPostion, nextCheckPointPostion);
            _previousCheckPoint = _nextCheckPoint;
            _nextCheckPoint = nextCheckPoint;
            _checkpointCounter++;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public float GetDistanceToNextCheckPoint(Vector3 position)
    {
        var nextCheckPointPostion = _nextCheckPoint.transform.position;
        return Vector3.Distance(position, nextCheckPointPostion);
    }

    public float GetDistanceToPreviousCheckPoint(Vector3 position)
    {
        var previousCheckPointPostion = _previousCheckPoint.transform.position;
        return Vector3.Distance(previousCheckPointPostion, position);
    }

    public float GetDistanceTraveled(Vector3 position)
    {
        return _distanceTraveled + GetDistanceToPreviousCheckPoint(position);
    }
}