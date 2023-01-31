using UnityEngine;

public class GarageUI : CoreUI
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Player _player;
    [SerializeField] private InfoTable _infoTable;
    [SerializeField] private RefuelButton _refuelButton;

    public override void Init()
    {
        base.Init();
        WindowStack.Push(MainWindow);
        _cameraMovement.MoveEnd.AddListener(ShowWindow);
        _infoTable.Init();
        _refuelButton.Init();
    }

    public override void OpenWindow(Window window)
    {
        var currentWindow = (MoveableWindow)WindowStack.Peek();
        currentWindow.gameObject.SetActive(false);
        WindowStack.Push(window);
        TryMoveCamera((MoveableWindow)window);
        AdChecker();
    }

    public override void CloseWindow()
    {
        if(WindowStack.Peek() != MainWindow)
        {
            var window = (MoveableWindow)WindowStack.Pop();
            window.gameObject.SetActive(false);
            window = (MoveableWindow)WindowStack.Peek();
            if (window == MainWindow)
            {
                Game.Instance.UpdatePlayerData(_player);
                Game.Instance.SaveData();
            }
            TryMoveCamera(window);
        }
    }

    private void TryMoveCamera(MoveableWindow window)
    {
        if (window.ViewTransform != default)
            _cameraMovement.MoveTo(window.ViewTransform);
        else
            ShowWindow();
    }
}
