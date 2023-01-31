using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Race config")]
public class RaceConfig : ScriptableObject
{
    [SerializeField] private string _raceName;
    [SerializeField] private string _leaderboardName;
    [SerializeField] private int _numberRaceLaps;
    [SerializeField] private List<RaceReward> _rewards;

    public string RaceName => _raceName;
    public string LeaderboardName => _leaderboardName;
    public int NumberRaceLaps => _numberRaceLaps;
    public List<RaceReward> Rewards => _rewards;
}