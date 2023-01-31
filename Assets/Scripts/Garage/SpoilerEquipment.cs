using UnityEngine;

public class SpoilerEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.SpoilerTuningIsActive;
    }
}