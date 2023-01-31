using UnityEngine;

public class GasStationUI : MonoBehaviour
{
    [SerializeField] private GasStation[] _gasStations;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private string _completeMessageKey;

    private void Awake()
    {
        _gasStations = FindObjectsOfType<GasStation>();

        foreach(var gasStation in _gasStations)
        {
            gasStation.RefuelComplete += RefuelComplete;
            gasStation.RefuelError += RefuelError;
        }
    }

    private void RefuelComplete()
    {
        var message = Game.Instance.Localization.GetText(_completeMessageKey);
        _infoWindow.OpenInfoWindow(message);
    }

    private void RefuelError(string message)
    {
        _infoWindow.OpenInfoWindow(message);
    }
}
