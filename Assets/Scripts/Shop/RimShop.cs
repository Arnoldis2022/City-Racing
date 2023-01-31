using System.Collections.Generic;
using UnityEngine;

public class RimShop : DetailShop<RimConfig>
{
    [SerializeField] private PainterUI _painter;
    [SerializeField] private float _paintingPrice;
    private RimConfig _installedRim;

    public List<RimConfig> SupportedRims => Player.Car.Config.SupportedRims;

    public override void Init()
    {
        _installedRim = Car.Wheelbase.CurrentRim;
        PainterInit();
        _painter.ColorIsBought += SetColor;
        var currentRimID = _installedRim.name;
        var availableRims = Car.Wheelbase.AvailableRims;
        SetDetail(availableRims, SupportedRims, currentRimID);
    }

    public override void NextDetail()
    {
        var rimConfig = NextDetail(SupportedRims);
        var currentRimID = _installedRim.name;
        var availableRims = Car.Wheelbase.AvailableRims;
        var tire = Car.Wheelbase.CurrentTire;
        var wheelsSize = Car.Wheelbase.WheelsSize;
        SetDetail(availableRims, rimConfig, currentRimID);
        RimData rimData = new RimData(rimConfig, rimConfig.DefaultColor);
        if (availableRims.ContainsKey(rimConfig.name))
        {
            rimData = availableRims[rimConfig.name];
        }
        Car.Wheelbase.CreateWheels(tire, rimConfig, wheelsSize, rimData);
        if(_installedRim.name == rimConfig.name)
        {
            _painter.gameObject.SetActive(true);
            PainterInit(); 
        }
        else
        {
            _painter.gameObject.SetActive(false);
        }
    }

    public override void PreviousDetail()
    {
        var rimConfig = PreviousDetail(SupportedRims);
        var currentRimID = _installedRim.name;
        var availableRims = Car.Wheelbase.AvailableRims;
        var tire = Car.Wheelbase.CurrentTire;
        var wheelsSize = Car.Wheelbase.WheelsSize;
        SetDetail(availableRims, rimConfig, currentRimID);
        RimData rimData = new RimData(rimConfig, rimConfig.DefaultColor);
        if (availableRims.ContainsKey(rimConfig.name))
        {
            rimData = availableRims[rimConfig.name];
        }
        Car.Wheelbase.CreateWheels(tire, rimConfig, wheelsSize, rimData);
        if (_installedRim.name == rimConfig.name)
        {
            _painter.gameObject.SetActive(true);
            PainterInit();
        }
        else
        {
            _painter.gameObject.SetActive(false);
        }
    }

    private void PainterInit()
    {
        var carWheels = Car.Wheelbase.Wheels;
        var rims = new List<PaintedDetail>();
        foreach(var wheel in carWheels)
        {
            rims.Add(wheel.RimPlace.CurrentRim);
        }
        _painter.Init(rims, _paintingPrice);
    }

    private void SetColor(Color color)
    {
        Car.Wheelbase.SetRimsColor(color);
    }

    public override void Buy()
    {
        var rim = SupportedRims[DetailIndex];
        var isPurchased = Player.TryDecreaseCredits(rim.Price);
        if (isPurchased)
        {
            _installedRim = rim;
            Car.Wheelbase.AddRim(rim);
            DisableButton();
            _painter.gameObject.SetActive(true);
            PainterInit();
        }
        else
        {
            var message = Game.Instance.Localization.GetText("{ui_error_price}");
            ErrorWindow.OpenInfoWindow(message);
        }
    }

    public override void Select()
    {
        var rim = SupportedRims[DetailIndex];
        var tire = Car.Wheelbase.CurrentTire;
        var wheelsSize = Car.Wheelbase.WheelsSize;
        var availableRims = Car.Wheelbase.AvailableRims;
        _installedRim = rim;
        RimData rimData = new RimData(rim, rim.DefaultColor);
        if (availableRims.ContainsKey(rim.name))
        {
            rimData = availableRims[rim.name];
        }
        Car.Wheelbase.CreateWheels(tire, rim, wheelsSize, rimData);
        _painter.gameObject.SetActive(true);
        PainterInit();
        DisableButton();
    }

    private void OnDisable()
    {
        _painter.ColorIsBought -= SetColor;
        var currentRimConfig = SupportedRims[DetailIndex];
        if(_installedRim.name != currentRimConfig.name)
        {
            var rimConfig = _installedRim;
            var tireAsset = Car.Wheelbase.CurrentTire;
            var wheelsSize = Car.Wheelbase.WheelsSize;
            var rimData = Car.Wheelbase.AvailableRims[_installedRim.name];
            Car.Wheelbase.CreateWheels(tireAsset, rimConfig, wheelsSize, rimData);
        }
    }
}
