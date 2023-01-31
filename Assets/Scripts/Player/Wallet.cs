using System;
using UnityEngine;

public class Wallet
{
    public Action<float> ChangingNumberCredits;

    private float _credits;
    private float _totalCredits;
    private float _maxCredits;

    public float Credits => _credits;
    public float TotalCredits => _totalCredits;

    public Wallet(float credits, float totalCredits,float maxCredits)
    {
        _credits = credits;
        _totalCredits = totalCredits;
        _maxCredits = maxCredits;
    }

    public void IncreaseCredits(float credits)
    {
        credits = Mathf.Abs(credits);
        var currentCredits = _credits + credits;

        if (currentCredits > _maxCredits)
        {
            currentCredits = _maxCredits;
        }

        _credits = currentCredits;
        _totalCredits += credits;
        ChangingNumberCredits?.Invoke(_credits);
    }

    public bool TryDecreaseCredits(float credits)
    {
        credits = Mathf.Abs(credits);
        var currentCredits = _credits - credits;

        if(currentCredits < 0)
        {
            return false;
        }

        _credits = currentCredits;
        ChangingNumberCredits?.Invoke(_credits);
        return true;
    }
}