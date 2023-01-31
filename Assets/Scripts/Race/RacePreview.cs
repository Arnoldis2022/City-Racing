using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RacePreview : Window
{
    [SerializeField] private Player _player;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private GameLoadScreen _loadScreen;
    [SerializeField] private LeaderboardYG _leaderboard;
    [SerializeField] private RaceManager _raceManager;
    [SerializeField] private RewardPreview _rewardPreviewPrefab;
    [SerializeField] private Transform _rewardPreviewContainer;
    private RaceTrack _currentRaceTrack;
    private List<RewardPreview> _rewardPreviewPool;

    public void Init(RaceTrack raceTrack)
    {
        _currentRaceTrack = raceTrack;
        _leaderboard.nameLB = raceTrack.LeaderboardName;
        _leaderboard.UpdateLB();
        InitRewardPreview(raceTrack.Rewards);
    }

    private void InitRewardPreview(List<RaceReward> rewards)
    {
        if(_rewardPreviewPool == null)
        {
            _rewardPreviewPool = new List<RewardPreview>();
            for (int i = 0; i < rewards.Count; i++)
            {
                var rewardPreview = Instantiate(_rewardPreviewPrefab, _rewardPreviewContainer);
                rewardPreview.Init(i + 1, rewards[i].Experience, rewards[i].Credits);
                _rewardPreviewPool.Add(rewardPreview);
            }
        }
        else
        {
            for (int i = 0; i < _rewardPreviewPool.Count; i++)
            {
                _rewardPreviewPool[i].Init(i + 1, rewards[i].Experience, rewards[i].Credits);
            }
        }
        //try
        //{
        //    for (int i = 0; i < _rewardPreviewPool.Count; i++)
        //    {
        //        _rewardPreviewPool[i].Init(i + 1, rewards[i].Experience, rewards[i].Credits);
        //    }
        //}
        //catch
        //{
        //    _rewardPreviewPool = new List<RewardPreview>();
        //    for (int i = 0; i < rewards.Count; i++)
        //    {
        //        var rewardPreview = Instantiate(_rewardPreviewPrefab, _rewardPreviewContainer);
        //        rewardPreview.Init(i + 1, rewards[i].Experience, rewards[i].Credits);
        //        _rewardPreviewPool.Add(rewardPreview);
        //    }
        //}
    }

    //Старая реализация начала гонки учитывающая класс автомобиля и гонки
    //public void PlayRace()
    //{
    //    var playerCarClass = _player.Car.Class;
    //    var requiredCarClass = _currentRaceTrack.RequiredCarClass;
    //    if (playerCarClass >= requiredCarClass)
    //    {
    //        _loadScreen.LoadScreenOpened += InitRace;
    //        _loadScreen.Open();
    //    }
    //    else
    //    {
    //        var message = Game.Instance.Localization.GetText("{ui_text_class_error}");
    //        _infoWindow.OpenInfoWindow(message);
    //    }
    //}

    public void PlayRace()
    {
        _loadScreen.LoadScreenOpened += InitRace;
        _loadScreen.Open();
    }

    private void InitRace()
    {
        _loadScreen.LoadScreenOpened -= InitRace;
        _currentRaceTrack.gameObject.SetActive(true);
        _raceManager.InitCompleted += _loadScreen.Close;
        _loadScreen.LoadScreenClosed += StartRace;
        _raceManager.CreateRaceSession(_currentRaceTrack);
    }

    private void StartRace()
    {
        _raceManager.InitCompleted -= _loadScreen.Close;
        _loadScreen.LoadScreenClosed -= StartRace;
        _raceManager.StartRace();
    }
}
