using UnityEngine;

public class GameUI : CoreUI
{
    [SerializeField] private CarFuelCheckerUI _fuelCheckerUI;
    [SerializeField] private RaceUI _raceUI;
    [SerializeField] private DriftUI _driftUI;
    [SerializeField] private PopupInfo _popupInfo;

    public override void Init()
    {
        base.Init();
        _fuelCheckerUI.Init();
        _raceUI.Init(this);
        _driftUI.Init();
        _popupInfo.Init();
    }

    public override void OpenWindow(Window window)
    {
        WindowStack.Push(window);
        ShowWindow();
        AdChecker();
    }

    public override void CloseWindow()
    {
        if (WindowStack.Count > 0)
        {
            var window = WindowStack.Pop();
            window.gameObject.SetActive(false);
        }
        else
        {
            OpenWindow(MainWindow);
        }
    }
}
