using System.Collections.Generic;

public class CarLoader
{
    public static CarData LoadCarData(CarConfig carConfig)
    {
        CarData carData = new CarData();
        try
        {
            DataSaver<CarData> dataSaver = new DataSaver<CarData>(carConfig.name);
            carData = dataSaver.LoadData();
        }
        catch
        {
            carData = LoadDefaultData(carConfig);
        }
        return carData;
    }

    private static CarData LoadDefaultData(CarConfig carConfig)
    {
        CarData defaultCarData = new CarData();
        defaultCarData.ControllabilityLevel = 0;
        defaultCarData.BrakesLevel = 0;
        defaultCarData.EngineLevel = 0;
        defaultCarData.FuelTankLevel = 0;
        defaultCarData.FuelQuantity = carConfig.BaseFuelTankCapacity;
        LoadDefaultRim(ref defaultCarData, carConfig.DefaultRim);
        LoadDefaultTire(ref defaultCarData, carConfig.DefaultTire);
        LoadDefaultSpoiler(ref defaultCarData, carConfig.DefaultSpoiler);
        return defaultCarData;
    }

    private static void LoadDefaultRim(ref CarData carData, RimConfig rimConfig)
    {
        carData.RimID = rimConfig.name;
        carData.AvailableRims = new List<RimData>();
        RimData rimData = new RimData(rimConfig, new DetailColor(rimConfig.DefaultColor));
        carData.AvailableRims.Add(rimData);
    }

    private static void LoadDefaultTire(ref CarData carData, TireConfig tireConfig)
    {
        carData.TireID = tireConfig.name;
        carData.AvailableTires = new List<TireData>();
        TireData tireData = new TireData(tireConfig);
        carData.AvailableTires.Add(tireData);
    }

    private static void LoadDefaultSpoiler(ref CarData carData, SpoilerConfig spoilerConfig)
    {
        carData.AvailableSpoilers = new List<SpoilerData>();
        if (spoilerConfig != null)
        {
            carData.SpoilerID = spoilerConfig.name;
            SpoilerData spoilerData = new SpoilerData(spoilerConfig);
            carData.AvailableSpoilers.Add(spoilerData);
        }
    }
}
