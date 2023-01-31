using UnityEngine;

public class BrakeEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.BrakesImproverIsActive;
    }
}