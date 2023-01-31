using UnityEngine;

public class SpeedDetectorUI : MonoBehaviour
{
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private string _notPassedMessageKey = "{ui_text_speed_checkecr_false}";

    private void Start()
    {
        var speedCheckers = FindObjectsOfType<SpeedDetector>();
        foreach(var speedChecker in speedCheckers)
        {
            speedChecker.TargetSpeedNotPassed += TargetSpeedNotPassedMessage;
        }
    }

    private void TargetSpeedNotPassedMessage()
    {
        var message = Game.Instance.Localization.GetText(_notPassedMessageKey);
        _infoWindow.OpenInfoWindow(message);
    }
}
