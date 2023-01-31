using System.Collections.Generic;

public class SpoilerShop : DetailShop<SpoilerConfig>
{
    private SpoilerConfig _installedSpoiler;

    public List<SpoilerConfig> SupportedSpoilers => Player.Car.Config.SupportedSpoilers;

    public override void Init()
    {
        _installedSpoiler = Car.SpoilerPlace.SpoilerConfig;
        var currentSpoilerID = _installedSpoiler.name;
        var availableSpoilerID = Car.SpoilerPlace.AvailableSpoilers;
        SetDetail(availableSpoilerID, SupportedSpoilers, currentSpoilerID);
    }

    public override void NextDetail()
    {
        var spoiler = NextDetail(SupportedSpoilers);
        SwapDetail(spoiler);
    }

    public override void PreviousDetail()
    {
        var spoiler = PreviousDetail(SupportedSpoilers);
        SwapDetail(spoiler);
    }

    private void SwapDetail(SpoilerConfig spoiler)
    {
        var currentSpoilerID = _installedSpoiler.name;
        var availableSpoilerID = Car.SpoilerPlace.AvailableSpoilers;
        SetDetail(availableSpoilerID, spoiler, currentSpoilerID);
        Car.SpoilerPlace.CreateSpoiler(spoiler, Car.Config.SpoilerSize,Car.Body.Color, Car.Body.MaterialSmoothness);
    }

    public override void Buy()
    {
        var spoilerAsset = SupportedSpoilers[DetailIndex];
        var isPurchased = Player.TryDecreaseCredits(spoilerAsset.Price);
        if (isPurchased)
        {
            _installedSpoiler = spoilerAsset;
            Car.SpoilerPlace.AddSpoiler(spoilerAsset);
            Game.Instance.UpdateCarData(Car);
            DisableButton();
        }
        else
        {
            ErrorWindow.OpenInfoWindow("Not enough credits!");
        }
    }

    public override void Select()
    {
        _installedSpoiler = SupportedSpoilers[DetailIndex];
        Game.Instance.UpdateCarData(Car);
        DisableButton();
    }

    private void OnDisable()
    {
        var carSpoilerAsset = Car.SpoilerPlace.SpoilerConfig;
        if (carSpoilerAsset != _installedSpoiler)
        {
            Car.SpoilerPlace.CreateSpoiler(_installedSpoiler, Car.Config.SpoilerSize, Car.Body.Color, Car.Body.MaterialSmoothness);
        }
    }
}
