using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class GarageUpgraderButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Garage _garage;
    [SerializeField] private Text _titleLabel;
    [SerializeField] private Text _levelLabel;
    [SerializeField] private Text _priceLabel;
    [SerializeField] private Text _description;
    [SerializeField] private GameObject _checkMark;
    [SerializeField] private GameObject _infoTable;
    [SerializeField] private Button _button;
    [SerializeField] private Image _previewImage;
    [SerializeField] private InfoWindow _infoWindow;
    [SerializeField] private GarageEquipment _garageEquipment;

    public GarageEquipment GarageEquipment => _garageEquipment;

    private void OnEnable()
    {
        var localization = Game.Instance.Localization;
        _titleLabel.text = localization.GetText(_garageEquipment.Config.NameLocalizationKey);
        _description.text = localization.GetText(_garageEquipment.Config.DescriptionLocalizationKey);
        _levelLabel.text = GarageEquipment.Config.OpeningLevel.ToString();
        _priceLabel.text = GarageEquipment.Config.OpeningPrice.ToString();
        _previewImage.sprite = GarageEquipment.Config.PreviewSprite;

        if (_garageEquipment.IsActive)
        {
            DeactivateButton();
        }
    }

    public void OpenFunction()
    {
        var localization = Game.Instance.Localization;
        var description = "";
        try
        {
            _garageEquipment.TryOpenEquipment(_player);
            description = localization.GetText("{ui_text_function_unlocked}");
            Game.Instance.UpdateGarageData(_garage);
            Game.Instance.UpdatePlayerData(_player);
            DeactivateButton();
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
            description = localization.GetText(exception.Message);
        }
        finally
        {
            _infoWindow.OpenInfoWindow(description);
        }
    }

    private void DeactivateButton()
    {
        _button.interactable = false;
        _checkMark.SetActive(true);
        _infoTable.SetActive(false);
    }
}
