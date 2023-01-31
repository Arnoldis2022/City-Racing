using System.Collections.Generic;
using System.Linq;

public class GarageUpper
{
    private PaintSprayEquipment _paintSprayEquipment;
    private FuelTankEquipment _fuelTankEquipment;
    private WheelsEquipment _wheelsEquipment;
    private SpoilerEquipment _spoilerEquipment;
    private EngineEquipment _engineEquipment;
    private ControllabilityEquipment _controllabilityEquipment;
    private BrakeEquipment _brakeEquipment;
    private NOSEquipment _nosEquipment;

    public bool PaintingIsActive => _paintSprayEquipment.IsActive;
    public bool WheelsTuningIsActive => _wheelsEquipment.IsActive;
    public bool SpoilerTuningIsActive => _spoilerEquipment.IsActive;
    public bool FuelTankImproverIsActive => _fuelTankEquipment.IsActive;
    public bool EngineImproverIsActive => _engineEquipment.IsActive;
    public bool ControllabilityImproverIsActive => _controllabilityEquipment.IsActive;
    public bool BrakesImproverIsActive => _brakeEquipment.IsActive;
    public bool NOSImproverIsActive => _nosEquipment.IsActive;

    public GarageUpper(List<GarageEquipment> garageEquipments, GarageData garageData)
    {    
        _paintSprayEquipment = garageEquipments.FirstOrDefault(equipment => equipment is PaintSprayEquipment) as PaintSprayEquipment;
        _fuelTankEquipment = garageEquipments.FirstOrDefault(equipment => equipment is FuelTankEquipment) as FuelTankEquipment;
        _wheelsEquipment = garageEquipments.FirstOrDefault(equipment => equipment is WheelsEquipment) as WheelsEquipment;
        _spoilerEquipment = garageEquipments.FirstOrDefault(equipment => equipment is SpoilerEquipment) as SpoilerEquipment;
        _engineEquipment = garageEquipments.FirstOrDefault(equipment => equipment is EngineEquipment) as EngineEquipment;
        _controllabilityEquipment = garageEquipments.FirstOrDefault(equipment => equipment is ControllabilityEquipment) as ControllabilityEquipment;
        _brakeEquipment = garageEquipments.FirstOrDefault(equipment => equipment is BrakeEquipment) as BrakeEquipment;
        _nosEquipment = garageEquipments.FirstOrDefault(equipment => equipment is NOSEquipment) as NOSEquipment;

        InitEquipment(_paintSprayEquipment, garageData);
        InitEquipment(_fuelTankEquipment, garageData);
        InitEquipment(_wheelsEquipment, garageData);
        InitEquipment(_spoilerEquipment, garageData);
        InitEquipment(_engineEquipment, garageData);
        InitEquipment(_controllabilityEquipment, garageData);
        InitEquipment(_brakeEquipment, garageData);
        InitEquipment(_nosEquipment, garageData);
    }

    private void InitEquipment(GarageEquipment garageEquipment, GarageData garageData)
    {
        garageEquipment.Init(garageData);
        if(garageEquipment.IsActive)
        {
            garageEquipment.gameObject.SetActive(true);
        }
    }

    public void TryOpenPainting(Player player)
    {
        _paintSprayEquipment.TryOpenEquipment(player);
    }

    public void TryOpenFuelTankImprover(Player player)
    {
        _fuelTankEquipment.TryOpenEquipment(player);
    }

    public void TryOpenWheelsTuning(Player player)
    {
        _wheelsEquipment.TryOpenEquipment(player);
    }

    public void TryOpenSpoilerTuning(Player player)
    {
        _spoilerEquipment.TryOpenEquipment(player);
    }

    public void TryOpenEngineImprover(Player player)
    {
        _engineEquipment.TryOpenEquipment(player);
    }

    public void TryOpenControllabilityImprover(Player player)
    {
        _controllabilityEquipment.TryOpenEquipment(player);
    }

    public void TryOpenNOSImprover(Player player)
    {
        _nosEquipment.TryOpenEquipment(player);
    }

    public void TryOpenBrakesImprover(Player player)
    {
        _brakeEquipment.TryOpenEquipment(player);
    }
}