using System.Collections.Generic;

public class TireShop : DetailShop<TireConfig>
{
    private TireConfig _installedTire;

    public List<TireConfig> SupportedTires => Player.Car.Config.SupportedTires;

    public override void Init()
    {
        _installedTire = Car.Wheelbase.CurrentTire;
        var currentTireID = _installedTire.name;
        var availableTiresID = Car.Wheelbase.AvailableTires;
        SetDetail(availableTiresID, SupportedTires, currentTireID);
    }

    public override void NextDetail()
    {
        var tireConfig = NextDetail(SupportedTires);
        SwapDetail(tireConfig);
    }

    public override void PreviousDetail()
    {
        var tireConfig = PreviousDetail(SupportedTires);
        SwapDetail(tireConfig);
    }

    private void SwapDetail(TireConfig tireConfig)
    {
        var currentTireID = _installedTire.name;
        var availableTireID = Car.Wheelbase.AvailableTires;
        SetDetail(availableTireID, tireConfig, currentTireID);
        Car.Wheelbase.CreateWheels(tireConfig);
    }

    public override void Buy()
    {
        var tire = SupportedTires[DetailIndex];
        var isPurchased = Player.TryDecreaseCredits(tire.Price);
        if (isPurchased)
        {
            _installedTire = tire;
            Car.Wheelbase.AddTire(tire);
            DisableButton();
        }
        else
        {
            var message = Game.Instance.Localization.GetText("{ui_error_price}");
            ErrorWindow.OpenInfoWindow(message);
        }
    }

    public override void Select()
    {
        var tire = SupportedTires[DetailIndex];
        _installedTire = tire;
        Car.Wheelbase.CreateWheels(tire);
        DisableButton();
    }

    private void OnDisable()
    {
        var currentTireConfig = SupportedTires[DetailIndex];
        if (_installedTire.name != currentTireConfig.name)
        {
            var rimConfig = Car.Wheelbase.CurrentRim;
            var tireAsset = _installedTire;
            var wheelsSize = Car.Wheelbase.WheelsSize;
            var rimData = Car.Wheelbase.AvailableRims[rimConfig.name];
            Car.Wheelbase.CreateWheels(tireAsset, rimConfig, wheelsSize, rimData);
        }
    }
}
