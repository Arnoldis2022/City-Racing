using System;
using System.Collections.Generic;

[Serializable]
public class GameData : IData
{
    public Dictionary<string, CarData> CarsData;
    public PlayerData PlayerData;
    public GarageData GarageData;

    public GameData() { }

    public GameData(Dictionary<string, CarData> carsData, PlayerData playerData, GarageData garageData)
    {
        CarsData = carsData;
        PlayerData = playerData;
        GarageData = garageData;
    }
}
