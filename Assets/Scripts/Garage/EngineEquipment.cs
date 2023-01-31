using UnityEngine;

public class EngineEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.EngineImproverIsActive;
    }
}