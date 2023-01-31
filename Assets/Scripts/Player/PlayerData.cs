using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData : IData
{
    public string CurrentCarID;
    public List<string> PurchasedCarsID;
    public float Credits;
    public float TotalCredits;
    public int Experience;
    public int Level;

    public PlayerData() { }

    public PlayerData(Player player)
    {
        CurrentCarID = player.CarID;
        PurchasedCarsID = player.PurchasedCarsID;
        Credits = player.Credits;
        TotalCredits = player.Wallet.TotalCredits;
        Experience = player.Experience;
        Level = player.Level;
    }

    public PlayerData(PlayerConfig playerConfig)
    {
        CurrentCarID = playerConfig.DefaultCar.name;
        PurchasedCarsID = new List<string>() { CurrentCarID };
        Credits = playerConfig.DefaultCredits;
        TotalCredits = playerConfig.DefaultCredits;
        Level = playerConfig.DefaultLevel;
        Experience = 0;
    }
}