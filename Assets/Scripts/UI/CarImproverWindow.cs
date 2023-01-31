using UnityEngine;

public class CarImproverWindow : MoveableWindow
{
    [SerializeField] private Garage _garage;
    [SerializeField] private Player _player;
    [SerializeField] private ImproveCarPanel _engine;
    [SerializeField] private ImproveCarPanel _controllability;
    [SerializeField] private ImproveCarPanel _brakes;
    [SerializeField] private ImproveCarPanel _fuelTank;
    [SerializeField] private ImproveCarPanel _nos;

    private void OnEnable()
    {
        var car = _player.Car;
        _engine.Init(_player, car.Engine);
        _controllability.Init(_player, car.Controllability);
        _brakes.Init(_player, car.Brakes);
        _fuelTank.Init(_player, car.FuelTank);
        _nos.Init(_player, car.NOS);

        EnableButton(_engine.ImproveButton, _garage.EngineImproverIsActive);
        EnableButton(_controllability.ImproveButton, _garage.ControllabilityImproverIsActive);
        EnableButton(_brakes.ImproveButton, _garage.BrakesImproverIsActive);
        EnableButton(_fuelTank.ImproveButton, _garage.FuelTankImproverIsActive);
        EnableButton(_nos.ImproveButton, _garage.NOSImproverIsActive);
    }

    private void EnableButton(LockableButton button, bool isOpen)
    {
        if (isOpen)
            button.UnlockButton();
        else
            button.LockButton();
    }
}
