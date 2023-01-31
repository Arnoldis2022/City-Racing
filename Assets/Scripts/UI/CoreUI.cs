using System.Collections.Generic;
using UnityEngine;
using YG;

public abstract class CoreUI : MonoBehaviour
{
    [SerializeField] private Window _mainWindow;
    [SerializeField] private int _quantityOpenedWindowBeforeAd = 5;
    private int _windowOpenCount;
    private Stack<Window> _windowStack;
    private UserInput _userInput;

    public Stack<Window> WindowStack => _windowStack;
    public Window MainWindow => _mainWindow;
    public UserInput Input => _userInput;
    public abstract void OpenWindow(Window window);
    public abstract void CloseWindow();


    public virtual void Init()
    {
        _windowStack = new Stack<Window>();
        _userInput = new UserInput();
        _userInput.Car.Tab.performed += context => CloseWindow();
        _userInput.Enable();
    }

    public void AdChecker()
    {
        _windowOpenCount++;
        if (_windowOpenCount >= _quantityOpenedWindowBeforeAd)
        {
            _windowOpenCount = 0;
            YandexGame.FullscreenShow();
        }
    }

    public void CloseAllWindow()
    {
        while (WindowStack.Count > 0)
        {
            CloseWindow();
        }
    }

    public void ShowWindow()
    {
        var window = WindowStack.Peek();
        window.gameObject.SetActive(true);
    }


    public bool IsLastWindow(Window window)
    {
        if (WindowStack.Count == 0)
            return false;

        if (WindowStack.Peek() == window)
            return true;

        return false;
    }

    private void OnDisable()
    {
        _userInput.Disable();
    }
}