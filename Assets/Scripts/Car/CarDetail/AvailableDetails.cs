using System.Collections.Generic;

public class AvailableDetails
{
    private Dictionary<string, RimData> _rims;
    private Dictionary<string, TireData> _tires;
    private Dictionary<string, SpoilerData> _spoilers;

    public Dictionary<string, RimData> Rims => _rims;
    public Dictionary<string, TireData> Tires => _tires;
    public Dictionary<string, SpoilerData> Spoilers => _spoilers;

    public AvailableDetails(Dictionary<string, RimData> rims, Dictionary<string, TireData> tires, Dictionary<string, SpoilerData> spoilers)
    {
        _rims = rims;
        _tires = tires;
        _spoilers = spoilers;
    }

    public void AddRim(RimConfig rimConfig)
    {
        DetailColor rimColor = new DetailColor(rimConfig.DefaultColor);
        RimData rimData = new RimData(rimConfig, rimColor);
        AddDetails(_rims, rimData);
    }

    public void AddTire(TireConfig tireConfig)
    {
        TireData tireData = new TireData(tireConfig);
        AddDetails(_tires, tireData);
    }

    public void AddSpoiler(SpoilerConfig spoilerConfig)
    {
        SpoilerData spoilerData = new SpoilerData(spoilerConfig);
        AddDetails(_spoilers, spoilerData);
    }

    private bool AddDetails<TDetailData>(Dictionary<string, TDetailData> availableDetails, TDetailData newDetail) where TDetailData : DetailData
    {
        if (!availableDetails.ContainsKey(newDetail.Id))
        {
            availableDetails.Add(newDetail.Id, newDetail);
            return true;
        }

        return false;
    }
}