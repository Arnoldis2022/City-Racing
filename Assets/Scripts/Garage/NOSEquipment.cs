public class NOSEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.NOSImproverIsActive;
    }
}
