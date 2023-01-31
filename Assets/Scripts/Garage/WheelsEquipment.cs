using UnityEngine;

public class WheelsEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.WheelsTuningIsActive;
    }
}