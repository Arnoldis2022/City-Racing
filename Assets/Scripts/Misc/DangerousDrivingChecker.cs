using System;
using System.Collections;
using UnityEngine;

public class DangerousDrivingChecker : MonoBehaviour
{
    public Action<int> Rewarded;

    [SerializeField] private Player _player;
    [SerializeField] private int _maxExperienceReward = 200;
    [SerializeField] private float _minSpeed = 20f;
    [SerializeField] private float _targetSpeedForMaxReward = 250f;
    [SerializeField] private float _timeToReward = 0.5f;
    [SerializeField] private float _timeout = 0.5f;
    [SerializeField] private string _targetTag = "Player";
    private IEnumerator _timeoutCoroutine;
    private float _speedForReward;
    private bool _isActive;

    private void Awake()
    {
        if(_player == null)   
            _player = FindObjectOfType<Player>();
        _isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive && other.CompareTag(_targetTag))
        {
            var car = _player.Car;
            if(car.Speed >= _minSpeed)
            {
                _speedForReward = Mathf.Clamp(car.Speed, _minSpeed, _targetSpeedForMaxReward);
                _isActive = false;
                _timeoutCoroutine = TimeToReward();
                StartCoroutine(_timeoutCoroutine);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isActive)
        {
            StopCoroutine(_timeoutCoroutine);
            StartCoroutine(Timeout());
        }
    }

    private IEnumerator TimeToReward()
    {
        yield return new WaitForSeconds(_timeToReward);
        var reward = _maxExperienceReward * (_speedForReward / _targetSpeedForMaxReward);
        if (reward > 0)
        {
            _player.AddExperience((int)reward);
        }
        StartCoroutine(Timeout());
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(_timeout);
        _isActive = true;
    }
}
