using System.Collections.Generic;
using UnityEngine;

public static class CarDataRandomizer
{
    public static CarData GenerateCarData(CarConfig config)
    {
        var spoilerConfig = RandomDetailID(config.SupportedSpoilers);
        var rimConfig = RandomDetailID(config.SupportedRims);
        var tireConfig = RandomDetailID(config.SupportedTires);
        CarData carData = new CarData(config);
        carData.Color = new DetailColor(RandomColor());
        carData.SpoilerID = spoilerConfig.name;
        var spoilerData = new SpoilerData(spoilerConfig);
        carData.AvailableSpoilers = new List<SpoilerData>();
        carData.AvailableSpoilers.Add(spoilerData);
        carData.RimID = rimConfig.name;
        var rimData = new RimData(rimConfig, RandomColor());
        carData.AvailableRims = new List<RimData>();
        carData.AvailableRims.Add(rimData);
        carData.TireID = tireConfig.name;
        var tireData = new TireData(tireConfig);
        carData.AvailableTires = new List<TireData>();
        carData.AvailableTires.Add(tireData);
        return carData;
    }

    private static TDetailConfig RandomDetailID<TDetailConfig>(List<TDetailConfig> detailsConfig) where TDetailConfig : DetailConfig
    {
        var detailsCount = detailsConfig.Count;
        var detailIndex = UnityEngine.Random.Range(0, detailsCount);
        return detailsConfig[detailIndex];
    }

    private static Color RandomColor()
    {
        var r = Random.Range(0f, 1f);
        var g = Random.Range(0f, 1f);
        var b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }
}