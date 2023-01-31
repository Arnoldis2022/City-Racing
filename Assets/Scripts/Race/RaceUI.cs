using UnityEngine;
using YG;

public class RaceUI : MonoBehaviour
{
    [SerializeField] private RaceManager _raceManager;
    [SerializeField] private RaceFinishUI _raceFinishUI;
    [SerializeField] private RaceHUD _raceHUD;
    [SerializeField] private GameObject _racePreviewButton;
    [SerializeField] private RacePreview _racePreview;
    private GameUI _gameUI;
    private UserInput _userInput;
    private bool _carIsReadyToRace;
    private RaceTrack _raceTrack;

    public void Init(GameUI gameUI)
    {
        _userInput = new UserInput();
        _raceManager.ÑountdownPerSeconds += _raceHUD.Countdown;
        _raceManager.InitCompleted += _raceHUD.Init;
        _raceManager.Started += _raceHUD.StartCountdown;
        _raceManager.UpdateRaceData += UpdateRaceData;
        _raceManager.RaceStoped += _raceHUD.HideHUD;
        _raceManager.RaceIsOver += FinishUI;
        _gameUI = gameUI;
        var triggers = FindObjectsOfType<RaceTrigger>();
        foreach (var trigger in triggers)
        {
            trigger.Entered += EnableRacePreviewButton;
            trigger.Exit += DisableRacePreviewButton;
        }
    }

    private void UpdateRaceData()
    {
        var currentCircle = _raceManager.GetNumberLapsPlayer() + 1;
        var totalCircle = _raceManager.RaceTrack.NumberRaceLaps;
        _raceHUD.UpdateCircle(currentCircle, totalCircle);

        var playerPosition = _raceManager.GetPlayerPosition() + 1;
        var totalPositions = _raceManager.RaceTrack.TotalPositions;
        _raceHUD.UpdatePlayerPosition(playerPosition, totalPositions);

        float milliseconds = _raceManager.PlayerTime * 1000;
        RaceTime time = new RaceTime(milliseconds);
        _raceHUD.UpdateTime(time.ToString());
    }

    private void EnableRacePreviewButton(RaceTrack raceTrack)
    {
        _userInput.Enable();
        _userInput.Car.Use.performed += context => OpenRacePreview();
        _carIsReadyToRace = true;
        _raceTrack = raceTrack;
        _racePreviewButton.SetActive(true);
    }

    private void DisableRacePreviewButton()
    {
        _userInput.Car.Use.performed -= context => OpenRacePreview();
        _userInput.Disable();
        _carIsReadyToRace = false;
        _racePreviewButton.SetActive(false);
        TryClosePreview();
    }

    public void OpenRacePreview()
    {
        bool previewIsOpened = _gameUI.IsLastWindow(_racePreview);
        if(!previewIsOpened)
        {
            _userInput.Car.Tab.performed += context => ClosePreview();
            _userInput.Car.Use.performed -= context => OpenRacePreview();
            _gameUI.OpenWindow(_racePreview);
            _racePreview.Init(_raceTrack);
            _racePreviewButton.SetActive(false);
        }
    }

    private void ClosePreview()
    {
        _userInput.Car.Tab.performed -= context => ClosePreview();
        _userInput.Car.Use.performed += context => OpenRacePreview();
        TryClosePreview();
        if (_carIsReadyToRace)
            _racePreviewButton.SetActive(true);
        else
            _racePreviewButton.SetActive(false);
    }

    public void TryClosePreview()
    {
        bool previewIsOpened = _gameUI.IsLastWindow(_racePreview);
        if (previewIsOpened)
            _gameUI.CloseWindow();
    }

    public void FinishUI()
    {
        WriteResultInLeaderboard();
        _raceHUD.HideHUD();
        _gameUI.OpenWindow(_raceFinishUI);
        _raceFinishUI.Init(_raceManager);
    }

    private void WriteResultInLeaderboard()
    {
        var raceName = _raceManager.RaceTrack.RaceName;
        RaceData raceData = new RaceData(raceName, (int)_raceManager.PlayerTimePerMilliseconds);
        var isUpdated = Game.Instance.TryUpdateRaceData(raceData);
        if (isUpdated)
        {
            YandexGame.NewLeaderboardScores(_raceManager.RaceTrack.LeaderboardName, raceData.PlayerTimeRecord);
        }
    }
}
