
[System.Serializable]
public class GarageData : IData
{
    public bool PaintingIsActive;
    public bool FuelTankImproverIsActive;
    public bool WheelsTuningIsActive;
    public bool SpoilerTuningIsActive;
    public bool EngineImproverIsActive;
    public bool ControllabilityImproverIsActive;
    public bool BrakesImproverIsActive;
    public bool NOSImproverIsActive;

    public GarageData() { }

    public GarageData(Garage garage)
    {
        PaintingIsActive = garage.PaintingIsActive;
        FuelTankImproverIsActive = garage.FuelTankImproverIsActive;
        WheelsTuningIsActive = garage.WheelsTuningIsActive;
        SpoilerTuningIsActive = garage.SpoilerTuningIsActive;
        EngineImproverIsActive = garage.EngineImproverIsActive;
        ControllabilityImproverIsActive = garage.ControllabilityImproverIsActive;
        BrakesImproverIsActive = garage.BrakesImproverIsActive;
        NOSImproverIsActive = garage.NOSImproverIsActive;
    }
}