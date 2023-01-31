using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    public Action AllCheckpointPassed;

    [SerializeField] private RaceConfig _config;
    [SerializeField] private RCC_AIWaypointsContainer _intersectionPoints;
    [SerializeField] private List<Transform> _startPoints;
    [SerializeField] private CheckPointManager _checkPointManager;

    public RaceConfig Config => _config; 
    public string RaceName => _config.RaceName;
    public List<Transform> StartPoints => _startPoints;
    public List<RaceReward> Rewards => _config.Rewards;
    public CheckPointManager CheckPointManager => _checkPointManager;
    public RCC_AIWaypointsContainer IntersectionPoints => _intersectionPoints;
    public string LeaderboardName => _config.LeaderboardName;
    public int NumberRaceLaps => _config.NumberRaceLaps;
    public int TotalPositions => _startPoints.Count;
}
