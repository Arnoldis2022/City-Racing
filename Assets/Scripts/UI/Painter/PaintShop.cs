using UnityEngine;
using System.Collections.Generic;
using System;

public class PaintShop
{
    public Action PriceUpdated;

    private List<PaintedDetail> _paintedDetails;
    private Color _paidColor;
    private float _paidSmoothness;
    private Color _currentColor;
    private float _currentSmoothness;
    private float _paintingPrice;
    private float _materialTypePrice;
    private float _totalPrice;
    private float _matteSmotthness;
    private float _glossySmotthness;

    public float PaintingPrice => _paintingPrice;
    public float MaterialTypePrice => _materialTypePrice;
    public float TotalPrice => _totalPrice;
    public Color PaidColor => _paidColor;
    public Color CurrentColor => _currentColor;

    public PaintShop(List<PaintedDetail> paintedDetails, float paintingPrice)
    {
        var detail = paintedDetails[0];
        _paintedDetails = paintedDetails;
        _totalPrice = paintingPrice;
        _paintingPrice = paintingPrice;
        _materialTypePrice = _paintingPrice * 0.4f;
        _currentColor = detail.Color;
        _paidColor = detail.Color;
        _paidSmoothness = detail.MaterialSmoothness;
        _currentSmoothness = detail.MaterialSmoothness;
        _matteSmotthness = PaintedDetail.MatteSmoothness;
        _glossySmotthness = PaintedDetail.GlossySmoothness;
        UpdateTotalPrice();
    }

    public void TryBuyColor(Player player)
    {
        var isPurchased = player.TryDecreaseCredits(_totalPrice);
        if(isPurchased)
        {
            _paidColor = _currentColor;
            _paidSmoothness = _currentSmoothness;
            UpdateTotalPrice();
        }
        else
        {
            throw new PriceException();
        }
    }

    public void SetMatteType()
    {
        foreach (PaintedDetail detail in _paintedDetails)
        {
            detail.SetMatte();
        }
        _currentSmoothness = _paintedDetails[0].MaterialSmoothness;
        UpdateTotalPrice();
    }

    public void SetGlossyType()
    {
        foreach (PaintedDetail detail in _paintedDetails)
        {
            detail.SetGlossy();
        }
        _currentSmoothness = _paintedDetails[0].MaterialSmoothness;
        UpdateTotalPrice();
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
        foreach (PaintedDetail detail in _paintedDetails)
        {
            detail.SetColor(color, _currentSmoothness);
        }
        UpdateTotalPrice();
    }

    public void SetRedSpectrum(float value)
    {
        foreach (PaintedDetail detail in _paintedDetails)
        {
            _currentColor.r = Mathf.Clamp(value, 0f, 1f);
            detail.Material.color = _currentColor;
        }
        UpdateTotalPrice();
    }

    public void SetGreenSpectrum(float value)
    {
        foreach (PaintedDetail detail in _paintedDetails)
        {
            _currentColor.g = Mathf.Clamp(value, 0f, 1f);
            detail.Material.color = _currentColor;
        }
        UpdateTotalPrice();
    }

    public void SetBlueSpectrum(float value)
    {
        foreach (PaintedDetail detail in _paintedDetails)
        {
            _currentColor.b = Mathf.Clamp(value, 0f, 1f);
            detail.Material.color = _currentColor;
        }
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        var colorIsCurrent = ColorIsCurrent();
        var smoothnessIsCurrent = SmoothnessIsCurrent();
        _totalPrice = 0;
        if (!colorIsCurrent)
            _totalPrice += _paintingPrice;

        if (!smoothnessIsCurrent)
            _totalPrice += MaterialTypePrice;

        PriceUpdated?.Invoke();
    }

    public void MaterialValidation()
    {
        var colorIsCurrent = ColorIsCurrent();
        var smoothnessIsCurrent = SmoothnessIsCurrent();
        if (!colorIsCurrent)
            ResetColor();
        if (!smoothnessIsCurrent)
            ResetSmoothness();
    }

    public bool ColorIsCurrent()
    {
        if (_currentColor != _paidColor)
            return false;

        return true;
    }

    public bool SmoothnessIsCurrent()
    {
        if (_currentSmoothness != _paidSmoothness)
            return false;

        return true;
    }

    public void ResetColor()
    {
        SetColor(_paidColor);
    }

    public void ResetSmoothness()
    {
        if (_paidSmoothness == _matteSmotthness)
            SetMatteType();
        else
            SetGlossyType();
    }
}
