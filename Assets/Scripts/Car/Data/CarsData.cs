using System;
using System.Collections.Generic;

[Serializable]
public class CarsData : IData
{
    public Dictionary<string, CarData> AllCarsData;

    public CarsData() { }

    public CarsData(Dictionary<string, CarData> allCarsData)
    {
        AllCarsData = allCarsData;
    }
}