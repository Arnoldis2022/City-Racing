using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoTable : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _creditsText;
    [SerializeField] private Text _fuelText;
    [SerializeField] private Text _maxFuelText;

    public void Init()
    {
        _player.CreditsChanged += UpdateCredits;
        _player.Car.CarRefueld += UpdateFuelQuantity;
        _player.CarChanged += UpdateFuelQuantity;
        _creditsText.text = _player.Credits.ToString("f0");
        _levelText.text = _player.Level.ToString("f0");
        UpdateFuelQuantity();
    }

    private void OnEnable()
    {
        try
        { 
            Init();
        }
        catch{}
    }

    private void UpdateCredits(float credits)
    {
        _creditsText.text = credits.ToString("f0");
    }

    private void UpdateFuelQuantity()
    {
        _fuelText.text = _player.Car.FuelQuantity.ToString("f0");
        _maxFuelText.text = _player.Car.FuelTankCapacity.ToString("f0");
    }
}
