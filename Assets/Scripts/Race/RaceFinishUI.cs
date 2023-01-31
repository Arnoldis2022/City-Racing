using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RaceFinishUI : Window
{
    [SerializeField] private LeaderboardYG _leaderboard;
    [SerializeField] private GameObject _rewardContainer;
    [SerializeField] private Text _placeText;
    [SerializeField] private Text _creditsText;
    [SerializeField] private Text _experienceText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Player _player;
    [SerializeField] private Button _doubleRewardButton;
    private float _currentCreditsReward;
    private int _currentExperienceReward;

    public void Init(RaceManager raceManager)
    {
        _player.Car.CarController.driftingNow = false;
        YandexGame.RewardVideoEvent += RewardAdComplete;
        var raceTrack = raceManager.RaceTrack;
        var playerPosition = raceManager.PlayerPosition;
        var playerTimePerSeconds = raceManager.PlayerTime;
        var rewards = raceTrack.Rewards;
        var leaderboardName = raceTrack.LeaderboardName;
        SetPosition(playerPosition);
        SetTime(playerTimePerSeconds);
        SetRewards(rewards, playerPosition);
        UpdateLeaderboard(leaderboardName);
        YandexGame.FullscreenShow();
    }

    private void SetPosition(int playerPosition)
    {
        _placeText.text = (playerPosition + 1).ToString();
    }

    private void SetTime(float playerTimePerSeconds)
    {
        RaceTime raceTime = new RaceTime(playerTimePerSeconds * 1000);
        _timeText.text = raceTime.ToString();
    }

    private void SetRewards(List<RaceReward> rewards, int playerPosition)
    {
        try
        {
            RaceReward reward = rewards[playerPosition];
            _player.IncreaseCredits(reward.Credits);
            _player.AddExperience(reward.Experience);
            _currentCreditsReward = reward.Credits;
            _currentExperienceReward = reward.Experience;
            _creditsText.text = _currentCreditsReward.ToString();
            _experienceText.text = _currentExperienceReward.ToString();
            _rewardContainer.SetActive(true);
            _doubleRewardButton.gameObject.SetActive(true);
        }
        catch
        {
            _rewardContainer.SetActive(false);
        }
    }

    private void UpdateLeaderboard(string leaderboardID)
    {
        _leaderboard.nameLB = leaderboardID;
        _leaderboard.UpdateLB();
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RewardAdComplete;
        Game.Instance.UpdatePlayerData(_player);
        Game.Instance.SaveData();
    }

    public void RewardAd(int id)
    {
        _doubleRewardButton.gameObject.SetActive(false);
        YandexGame.RewVideoShow(id);
    }

    private void RewardAdComplete(int id)
    {
        var doubleCredits = _currentCreditsReward * 2;
        var doubleExperience = _currentExperienceReward * 2;
        _player.IncreaseCredits(_currentCreditsReward);
        _player.AddExperience(_currentExperienceReward);
        _creditsText.text = doubleCredits.ToString();
        _experienceText.text = doubleExperience.ToString();
    }

}