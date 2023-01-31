using System.Collections.Generic;
using UnityEngine;

public class Garage : MonoBehaviour
{
    [SerializeField] private List<GarageEquipment> _garageEquipments;
    [SerializeField] private Transform _carPlace;
    private GarageUpper _garageUpper;

    public GarageUpper Upper => _garageUpper;
    public bool PaintingIsActive => _garageUpper.PaintingIsActive;
    public bool FuelTankImproverIsActive => _garageUpper.FuelTankImproverIsActive;
    public bool WheelsTuningIsActive => _garageUpper.WheelsTuningIsActive;
    public bool SpoilerTuningIsActive => _garageUpper.SpoilerTuningIsActive;
    public bool EngineImproverIsActive => _garageUpper.EngineImproverIsActive;
    public bool ControllabilityImproverIsActive => _garageUpper.ControllabilityImproverIsActive;
    public bool BrakesImproverIsActive => _garageUpper.BrakesImproverIsActive;
    public bool NOSImproverIsActive => _garageUpper.NOSImproverIsActive;
    public Transform CarPlace => _carPlace;

    public void Init(GarageData garageData)
    {
        _garageUpper = new GarageUpper(_garageEquipments, garageData);
    }
}
