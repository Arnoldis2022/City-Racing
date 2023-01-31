using System;
using TMPro;
using UnityEngine;

public class SpeedDetector : MonoBehaviour
{
    public Action<SpeedDetector> TargetSpeedPassed;
    public Action TargetSpeedNotPassed;

    [SerializeField] private Player _player;
    [SerializeField] private float _minTargetSpeed;
    [SerializeField] private float _maxTargetSpeed;
    [SerializeField] private float _maxCreditsReward;
    [SerializeField] private float _maxExperienceReward;
    [SerializeField] private TextMeshProUGUI _creditsRewardPreview;
    [SerializeField] private TextMeshProUGUI _experienceRewardPreview;
    [SerializeField] private TextMeshProUGUI _targetSpeedPreview;
    private float _targetSpeed;
    private int _creditsReward;
    private int _experienceReward;

    public int CreditsReward => _creditsReward;
    public int ExperienceReward => _experienceReward;

    private void Awake()
    {
        _targetSpeed = UnityEngine.Random.Range(_minTargetSpeed, _maxTargetSpeed);
        _targetSpeed = (int)_targetSpeed;
        _creditsReward = Mathf.FloorToInt(_maxCreditsReward * (_targetSpeed / _maxTargetSpeed));
        _creditsRewardPreview.text = _creditsReward.ToString();

        _experienceReward = Mathf.FloorToInt(_maxExperienceReward * (_targetSpeed / _maxTargetSpeed));
        _creditsRewardPreview.text = _experienceReward.ToString();

        _targetSpeedPreview.text = _targetSpeed.ToString() + "km/h";

        if (_player == null)
            _player = FindObjectOfType<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_player.tag))
        {
            if(_player.Car.Speed >= _targetSpeed)
            {
                _player.IncreaseCredits(_creditsReward);
                _player.AddExperience(_experienceReward);
                Game.Instance.UpdatePlayerData(_player);
                TargetSpeedPassed?.Invoke(this);
                gameObject.SetActive(false);
            }
            else
            {
                TargetSpeedNotPassed?.Invoke();
            }
        }
    }
}
