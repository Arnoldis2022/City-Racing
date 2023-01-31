using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    public Action<int> ExperienceAdded;
    public Action<float> CreditsAdded;
    public Action<float> CreditsChanged;
    public Action CarChanged;

    [SerializeField] private PlayerConfig _config;
    [SerializeField] private string _moneyEarnedLeaderboardID;
    private Car _car;
    private PlayerLevel _level;
    private Wallet _wallet;
    private string _carID;
    private List<string> _purchasedCarsID;

    public string CarID => _carID;
    public Car Car => _car;
    public Wallet Wallet => _wallet;
    public PlayerConfig Config => _config;
    public List<string> PurchasedCarsID => _purchasedCarsID;
    public float Credits => _wallet.Credits;
    public PlayerLevel PlayerLevel => _level;
    public int Level => _level.Level;
    public int Experience => _level.Experience;

    public void Init(PlayerData playerData, Car car)
    {
        _wallet = new Wallet(playerData.Credits, playerData.TotalCredits, _config.MaxCredits);
        _level = new PlayerLevel(playerData, _config);
        _purchasedCarsID = playerData.PurchasedCarsID;
        SetCar(car);
    }

    public void SetCar(Car car)
    {
        _car = car;
        _car.gameObject.tag = gameObject.tag;
        _carID = car.Config.name;
        var purchased = _purchasedCarsID.IndexOf(_carID);
        if(purchased == -1)
        {
            _purchasedCarsID.Add(_carID);
        }
        CarChanged?.Invoke();
    }

    public void AddExperience(int experience)
    {
        if(experience > 0)
        {
            _level.AddExperience(experience);
            ExperienceAdded?.Invoke(experience);
        }
    }

    public void IncreaseCredits(float credits)
    {
        if(credits > 0)
        {
            _wallet.IncreaseCredits(credits);
            YandexGame.NewLeaderboardScores(_moneyEarnedLeaderboardID, (int)_wallet.TotalCredits);
            CreditsAdded?.Invoke(credits);
            CreditsChanged?.Invoke(_wallet.Credits);
        }
    }

    public bool TryDecreaseCredits(float credits)
    {
        var isDecreased = _wallet.TryDecreaseCredits(credits);
        CreditsChanged?.Invoke(_wallet.Credits);
        return isDecreased;
    }
}
