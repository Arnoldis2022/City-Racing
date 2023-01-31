public class FuelTankEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.FuelTankImproverIsActive;
    }
}