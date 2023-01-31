using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _fpsText;
    [SerializeField] private Text _playerExperienceText;
    [SerializeField] private Text _playerLevelText;
    [SerializeField] private Text _playerCreditsText;
    private float _updateInterval = 0.5F;
    private double _lastInterval;
    private int _frames;
    private float _fps;

    void Update()
    {
        ++_frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > _lastInterval + _updateInterval)
        {
            _fps = (float)(_frames / (timeNow - _lastInterval));
            _fpsText.text = _fps.ToString();
            _frames = 0;
            _lastInterval = timeNow;
        }
        _playerExperienceText.text = _player.Experience.ToString();
        _playerLevelText.text = _player.Level.ToString();
        _playerCreditsText.text = _player.Credits.ToString();
    }
}
