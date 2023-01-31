using UnityEngine;

public class ControllabilityEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.ControllabilityImproverIsActive;
    }
}