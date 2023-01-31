using UnityEngine;

public class TuningMenuWindow : MoveableWindow
{
    [SerializeField] private Garage _garage;
    [SerializeField] private LockableButton _wheelTuningButton;
    [SerializeField] private LockableButton _spoilerTuningButton;
    [SerializeField] private LockableButton _colorChangerButton;

    private void OnEnable()
    {
        EnableButton(_colorChangerButton, _garage.PaintingIsActive);
        EnableButton(_wheelTuningButton, _garage.WheelsTuningIsActive);
        EnableButton(_spoilerTuningButton, _garage.SpoilerTuningIsActive);
    }

    private void EnableButton(LockableButton button, bool isOpen)
    {
        if (isOpen)
            button.UnlockButton();
        else
            button.LockButton();
    }
}
