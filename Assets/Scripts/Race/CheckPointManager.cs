using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<RaceCheckPoint> _checkPoints;
    [SerializeField] private RaceCheckPoint _startCheckPoint;

    public List<RaceCheckPoint> CheckPoints => _checkPoints;
    public RaceCheckPoint StartCheckPoint => _startCheckPoint;
    public RaceCheckPoint CheckpointBeforeStart => GetPreviousCheckPointBefore(_startCheckPoint);
    public int NumberCheckPoints => _checkPoints.Count;

    private void Awake()
    {
        if(_startCheckPoint == null && _checkPoints.Count > 0)
        {
            _startCheckPoint = _checkPoints[0];
        }
    }

    public RaceCheckPoint GetNextCheckPointAfter(RaceCheckPoint checkPoint)
    {
        var currentCheckPointIndex = _checkPoints.IndexOf(checkPoint);
        currentCheckPointIndex++;
        if(currentCheckPointIndex >= _checkPoints.Count)
        {
            currentCheckPointIndex = 0;
        }

        return _checkPoints[currentCheckPointIndex];
    }

    public RaceCheckPoint GetPreviousCheckPointBefore(RaceCheckPoint checkPoint)
    {
        var currentCheckPointIndex = _checkPoints.IndexOf(checkPoint);
        currentCheckPointIndex--;
        if(currentCheckPointIndex < 0)
        {
            currentCheckPointIndex = _checkPoints.Count - 1;
        }

        return _checkPoints[currentCheckPointIndex];
    }
}
