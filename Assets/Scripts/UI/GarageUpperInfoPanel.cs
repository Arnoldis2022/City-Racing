using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GarageUpperInfoPanel : MonoBehaviour
{
    public Action PurchaseEvent;

    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private Image _previewImage;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _priceLabel;
    [SerializeField] private TextMeshProUGUI _levelLabel;
    private GarageEquipment _garageEquipment;

    public void Init(GarageEquipment garageEquipment)
    {
        _garageEquipment = garageEquipment;
        var localization = Game.Instance.Localization;
        _previewImage.sprite = _garageEquipment.Config.PreviewSprite;
        _title.text = localization.GetText(_garageEquipment.Config.NameLocalizationKey);
        _description.text = localization.GetText(_garageEquipment.Config.DescriptionLocalizationKey);
        _priceLabel.text = _garageEquipment.Config.OpeningPrice.ToString();
        _levelLabel.text = _garageEquipment.Config.OpeningLevel.ToString();
    }

    public void OpenFunction()
    {
        var localization = Game.Instance.Localization;
        var description = "";
        try
        {
            _garageEquipment.TryOpenEquipment(_player);
            description = localization.GetText("{ui_text_function_unlocked}");
            _infoWindow.OpenInfoWindow(description);
            PurchaseEvent?.Invoke();
            gameObject.SetActive(false);
        }
        catch(Exception exception)
        {
            description = localization.GetText(exception.Message);
        }
        finally
        {
            _infoWindow.OpenInfoWindow(description);
        }
    }
}
