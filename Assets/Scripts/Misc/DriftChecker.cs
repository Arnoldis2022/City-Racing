using System;
using System.Collections;
using UnityEngine;

public class DriftChecker : BaseChecker
{
    public Action Cancelled;
    public Action Completed;
    public Action Updated;

    [SerializeField] private float _driftAcceptanceTime;
    [SerializeField] private float _experiencePerDriftPerUnitOfTime;
    [SerializeField] private float _creditsPerDriftPerUnitOfTime;
    [SerializeField] private float _timeoutAfterCanceled = 0.5f;
    private bool _isCancelled = false;
    private Car _targetCar;
    private Player _player;
    private int _driftPoint;
    private IEnumerator _acceptanceCoroutine;

    public int DriftPoint => _driftPoint;
    public int ExperienceFromDrifting => Mathf.FloorToInt(_driftPoint * _experiencePerDriftPerUnitOfTime);
    public int CreditsFromDrifting => Mathf.FloorToInt(_driftPoint * _creditsPerDriftPerUnitOfTime);

    public override void Init(Player player)
    {
        _player = player;
        _targetCar = player.Car;
        _targetCar.Collision += CancelledDrift;
        _driftPoint = 0;
    }

    public override void Check()
    {
        if(_targetCar.IsDrifting && !_isCancelled)
        {
            StopAllCoroutines();
            _driftPoint += 1;
            Updated?.Invoke();
        }
        else if(!_targetCar.IsDrifting && _driftPoint != 0)
        {
            _acceptanceCoroutine = AcceptanceOfDrift();
            StartCoroutine(_acceptanceCoroutine);
        }
    }

    private IEnumerator AcceptanceOfDrift()
    {
        yield return new WaitForSeconds(_driftAcceptanceTime);
        _player.IncreaseCredits(CreditsFromDrifting);
        _player.AddExperience(ExperienceFromDrifting);
        Completed?.Invoke();
        _driftPoint = 0;
    }

    private void CancelledDrift(Collision collision)
    {
        if(_driftPoint > 0)
        {
            _isCancelled = true;
            _driftPoint = 0;
            if(_acceptanceCoroutine != null)
                StopCoroutine(_acceptanceCoroutine);
            StartCoroutine(Timeout());
            Cancelled?.Invoke();
        }
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(_timeoutAfterCanceled);
        _isCancelled = false;
    }
}
