using UnityEngine;

public class PaintSprayEquipment : GarageEquipment
{
    public override void Init(GarageData garageData)
    {
        _isActive = garageData.PaintingIsActive;
    }
}