using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarFuelCheckerUI : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private FuelChecker _fuelChecker;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _endGameWindow;
    [SerializeField] private GameObject _teleportPriceContainer;
    [SerializeField] private Text _teleportPriceText;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _gasStationTeleportButton;
    [SerializeField] private GasStation _gasStation;
    [SerializeField] private GameLoadScreen _loadScreen;
    [SerializeField] private RaceManager _raceManager;
    private Car _car;
    private bool _isActive;

    public void Init()
    {
        _car = _player.Car;
        _isActive = false;
        _fuelChecker.FuelIsOut += OpenWindow;
    }

    public void OpenWindow()
    {
        if(!_isActive)
        {
            _isActive = true;
            _parentObject.SetActive(true);
            _endGameWindow.gameObject.SetActive(true);
            OnOrOffTeleport();
        }
    }

    private void OnOrOffTeleport()
    {
        var teleportPrice = _gasStation.CalculateTeleportPrice(_player);
        if (_player.Credits < teleportPrice)
        {
            _gasStationTeleportButton.gameObject.SetActive(false);
            _teleportPriceContainer.SetActive(false);
        }
        else
        {
            _gasStationTeleportButton.gameObject.SetActive(true);
            _teleportPriceContainer.SetActive(true);
            _teleportPriceText.text = teleportPrice.ToString();
        }
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartTeleportToGasStation()
    {
        _endGameWindow.SetActive(false);
        _loadScreen.Open();
        _loadScreen.LoadScreenOpened += TeleportToGasStation;
        if (_raceManager.RaceIsActive)
        {
            _raceManager.StopRace();
        }
    }

    private void TeleportToGasStation()
    {
        _gasStation.TryTeleportCar(_player);
        _car.StartEngine();
        _loadScreen.LoadScreenOpened -= TeleportToGasStation;
        _loadScreen.LoadScreenClosed += HideWindow;
        _loadScreen.Close();
    }

    private void HideWindow()
    {
        _isActive = false;
        _loadScreen.LoadScreenClosed -= HideWindow;
        _endGameWindow.gameObject.SetActive(false);
        _parentObject.SetActive(false);
    }
}
