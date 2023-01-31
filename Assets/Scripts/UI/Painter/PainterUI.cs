using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainterUI : MonoBehaviour
{
    public Action<Color> ColorIsBought;

    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Toggle _matteToggle;
    [SerializeField] private Toggle _glossyToggle;
    [SerializeField] private Text _priceText;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private Slider _redSpectrumSlider;
    [SerializeField] private Slider _greenSpectrumSlider;
    [SerializeField] private Slider _blueSpectrumSlider;
    [SerializeField] private InputField _redSpectrumInputField;
    [SerializeField] private InputField _greenSpectrumInputField;
    [SerializeField] private InputField _blueSpectrumInputField;
    private PaintShop _painter;

    public void Init(List<PaintedDetail> paintedDetails, float paintingPrice)
    {
        _painter = new PaintShop(paintedDetails, paintingPrice);
        _painter.PriceUpdated += TryEnableBuyButton;
        _priceText.text = _painter.TotalPrice.ToString();
        _glossyToggle.isOn = paintedDetails[0].IsGlossy;
        _matteToggle.isOn = paintedDetails[0].IsMatte;
        UpdateUI();
    }

    public void Init(PaintedDetail paintedDetail, float paintingPrice)
    {
        List<PaintedDetail> paintedDetails = new List<PaintedDetail>() { paintedDetail };
        Init(paintedDetails, paintingPrice);
    }

    public void BuyColor()
    {
        try
        {
            _painter.TryBuyColor(_player);
            ColorIsBought?.Invoke(_painter.PaidColor);
            Game.Instance.UpdatePlayerData(_player);
        }
        catch (PriceException exception)
        {
            var localization = Game.Instance.Localization;
            var message = localization.GetText(exception.Message);
            _infoWindow.OpenInfoWindow(message);
        }
    }

    public void SetColor(Color color)
    {
        _painter.SetColor(color);
        UpdateUI();
    }

    public void SetMatteType(bool value)
    {
        _painter.SetMatteType();
        UpdateUI();
    }

    public void SetGlossyType(bool value)
    {
        _painter.SetGlossyType();
        UpdateUI();
    }

    public void SetRedSpectrumSlider()
    {
        var value = _redSpectrumSlider.value;
        _painter.SetRedSpectrum(value);
        UpdateUI();
    }

    public void SetRedSpectrumIputField(string value)
    {
        float redSpectrum = SpectrumValidate(float.Parse(value));
        _painter.SetRedSpectrum(redSpectrum);
        UpdateUI();
    }

    public void SetGreenSpectrumSlider()
    {
        var value = _greenSpectrumSlider.value;
        _painter.SetGreenSpectrum(value);
        UpdateUI();
    }

    public void SetGreenSpectrumIputField(string value)
    {
        float greenSpectrum = SpectrumValidate(float.Parse(value));
        _painter.SetGreenSpectrum(greenSpectrum);
        UpdateUI();
    }

    public void SetBlueSpectrumSlider()
    {
        var value = _blueSpectrumSlider.value;
        _painter.SetBlueSpectrum(value);
        UpdateUI();
    }

    public void SetBlueSpectrumIputField(string value)
    {
        float blueSpectrum = SpectrumValidate(float.Parse(value));
        _painter.SetBlueSpectrum(blueSpectrum);
        UpdateUI();
    }

    private float SpectrumValidate(float value)
    {
        float spectrum = value;
        if (spectrum < 0f)
            spectrum = 0f;
        else if (spectrum > 255f)
            spectrum = 1f;
        else
            spectrum = spectrum / 255f;
        return spectrum;
    }

    private void UpdateUI()
    {
        var color = _painter.CurrentColor;
        var red255 = (int)(color.r * 255f);
        var green255 = (int)(color.g * 255f);
        var blue255 = (int)(color.b * 255f);
        _redSpectrumSlider.value = color.r;
        _greenSpectrumSlider.value = color.g;
        _blueSpectrumSlider.value = color.b;
        _redSpectrumInputField.text = red255.ToString();
        _greenSpectrumInputField.text = green255.ToString();
        _blueSpectrumInputField.text = blue255.ToString();
        TryEnableBuyButton();
    }

    private void TryEnableBuyButton()
    {
        var totalPrice = _painter.TotalPrice;
        if (totalPrice != 0)
        {
            _priceText.text = _painter.TotalPrice.ToString();
            _buyButton.gameObject.SetActive(true);
        }
        else
        {
            _buyButton.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        _painter.MaterialValidation();
    }
}
