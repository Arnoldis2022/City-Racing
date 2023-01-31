using System;
using UnityEngine;
using UnityEngine.UI;

public class ImproveCarPanel : MonoBehaviour
{
    [SerializeField] private LockableButton _improveButton;
    [SerializeField] private ImproveIndicator _indicatorPrefab;
    [SerializeField] private Transform _indicatorContainer;
    [SerializeField] private GameObject _previewPanel;
    [SerializeField] private Text _pricePreviewText;
    [SerializeField] private Text _levelPreviewText;
    [SerializeField] private InfoWindow _infoWindos;
    [SerializeField] private int _baseImprovePrice;
    [SerializeField] private int _baseRequiredLevel;
    private int _improvePrice;
    private int _requiredLevel;
    private ImproveIndicator[] _indicatorsPool;
    private Player _player;
    private Car _car;
    private Upgradeable _carFeatures;

    public LockableButton ImproveButton => _improveButton;

    public void Init(Player player, Upgradeable carFeatures)
    {
        _player = player;
        _car = _player.Car;
        _carFeatures = carFeatures;

        if (_indicatorsPool == null)
        {
            _indicatorsPool = new ImproveIndicator[carFeatures.MaxLevel];
            for(int i = 0; i < _indicatorsPool.Length; i++)
            {
                _indicatorsPool[i] = Instantiate(_indicatorPrefab, _indicatorContainer);
                if(carFeatures.Level > i)
                {
                    _indicatorsPool[i].Activate();
                }
                else
                {
                    _indicatorsPool[i].ResetIndicator();
                }
            }
        }
        else
        {
            for (int i = 0; i < _indicatorsPool.Length; i++)
            {
                if (carFeatures.Level > i)
                {
                    _indicatorsPool[i].Activate();
                }
                else
                {
                    _indicatorsPool[i].ResetIndicator();
                }
            }
        }
        RecalculateRequirements();

    }

    private void RecalculateRequirements()
    {
        _improvePrice = (int)(_baseImprovePrice + ((_car.Config.Class + _carFeatures.Level + 1) * 13f));
        _pricePreviewText.text = _improvePrice.ToString();
        _requiredLevel = _baseRequiredLevel + _carFeatures.Level;
        _levelPreviewText.text = _requiredLevel.ToString();
        int indicatorPoolIndex = _carFeatures.Level - 1;
        if(indicatorPoolIndex != -1)
            _indicatorsPool[indicatorPoolIndex].Activate();
        if (_carFeatures.Level >= _carFeatures.MaxLevel)
        {
            _previewPanel.SetActive(false);
            _improveButton.gameObject.SetActive(false);
        }
    }

    public void Improve()
    {
        try
        {
            if (_player.Level >= _requiredLevel)
            {
                var purchased = _player.TryDecreaseCredits(_improvePrice);
                if (purchased)
                {
                    _carFeatures.LevelUp();
                    RecalculateRequirements();
                    Game.Instance.UpdatePlayerData(_player);
                    return;
                }
                throw new PriceException();
            }

            throw new LevelException();
        }
        catch(Exception exception)
        {
            var message = Game.Instance.Localization.GetText(exception.Message);
            _infoWindos.OpenInfoWindow(message);
        }  
    }
}
