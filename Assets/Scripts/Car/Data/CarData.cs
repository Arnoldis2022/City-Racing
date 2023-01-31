using System;
using System.Collections.Generic;

[Serializable]
public class CarData : IData
{
    public string Id;
    public int ControllabilityLevel;
    public int NOSLevel;
    public int BrakesLevel;
    public int EngineLevel;
    public int FuelTankLevel;
    public float FuelQuantity;
    public DetailColor Color;
    public string RimID;
    public string TireID;
    public string SpoilerID;
    public List<RimData> AvailableRims;
    public List<TireData> AvailableTires;
    public List<SpoilerData> AvailableSpoilers;

    public CarData() { }

    public CarData(Car car)
    {
        Id = car.Config.name;
        ControllabilityLevel = car.Controllability.Level;
        BrakesLevel = car.Brakes.Level;
        EngineLevel = car.Engine.Level;
        FuelTankLevel = car.FuelTankLevel;
        FuelQuantity = car.FuelQuantity;
        NOSLevel = car.NOS.Level;
        Color = new DetailColor(car.Body.Color, smoothness: car.Body.MaterialSmoothness);
        RimID = car.Wheelbase.RimID;
        TireID = car.Wheelbase.TireID;
        SpoilerID = car.SpoilerPlace.SpoilerID;
        AvailableRims = DetailsDataToList(car.Wheelbase.AvailableRims);
        AvailableTires = DetailsDataToList(car.Wheelbase.AvailableTires);
        AvailableSpoilers = DetailsDataToList(car.SpoilerPlace.AvailableSpoilers);
    }

    private List<T> DetailsDataToList<T>(Dictionary<string, T> detailsData) where T : DetailData
    {
        var detailsList = new List<T>();
        foreach(var detailData in detailsData)
        {
            detailsList.Add(detailData.Value);
        }
        return detailsList;
    }

    public CarData(CarConfig carConfig)
    {
        Id = carConfig.name;
        ControllabilityLevel = 0;
        BrakesLevel = 0;
        EngineLevel = 0;
        FuelTankLevel = 0;
        NOSLevel = 0;
        FuelQuantity = carConfig.BaseFuelTankCapacity;
        Color = new DetailColor(carConfig.DefaultBodyColor, smoothness: carConfig.DefaultBodySmoothness);
        LoadDefaultRim(carConfig.DefaultRim);
        LoadDefaultTire(carConfig.DefaultTire);
        LoadDefaultSpoiler(carConfig.DefaultSpoiler);
    }

    private void LoadDefaultRim(RimConfig rimConfig)
    {
        RimID = rimConfig.name;
        AvailableRims = new List<RimData>();
        RimData rimData = new RimData(rimConfig, new DetailColor(rimConfig.DefaultColor, smoothness: rimConfig.DefaultRimSmoothness));
        AvailableRims.Add(rimData);
    }

    private void LoadDefaultTire(TireConfig tireConfig)
    {
        TireID = tireConfig.name;
        AvailableTires = new List<TireData>();
        TireData tireData = new TireData(tireConfig);
        AvailableTires.Add(tireData);
    }

    private void LoadDefaultSpoiler(SpoilerConfig spoilerConfig)
    {
        AvailableSpoilers = new List<SpoilerData>();
        if (spoilerConfig != null)
        {
            SpoilerID = spoilerConfig.name;
            SpoilerData spoilerData = new SpoilerData(spoilerConfig);
            AvailableSpoilers.Add(spoilerData);
        }
    }
}