using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public abstract class DetailShop<TDetailAsset> : MonoBehaviour where TDetailAsset : DetailConfig
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Text _priceText;
    [SerializeField] private InfoWindow _errorWindow;
    private Button _currentButton;
    private int _detailIndex = 0;

    public Car Car => _player.Car;
    public Player Player => _player;
    public InfoWindow ErrorWindow => _errorWindow;
    public int DetailIndex => _detailIndex;

    public abstract void Init();
    public abstract void Buy();
    public abstract void Select();
    public abstract void NextDetail();
    public abstract void PreviousDetail();

    private void OnEnable()
    {
        Init();
    }

    protected void SetDetail<TDetailData>(Dictionary<string, TDetailData> availableRimsID, List<TDetailAsset> supportedDetail, string currentDetailID) where TDetailData : DetailData
    {
        for (int i = 0; i < supportedDetail.Count; i++)
        {
            _detailIndex = i;
            if (supportedDetail[i].name == currentDetailID)
            {
                SetDetail(availableRimsID, supportedDetail[i], currentDetailID);
                break;
            }
        }
    }

    protected void SetDetail<TDetailData>(Dictionary<string, TDetailData> availableRimsID, TDetailAsset newDetail, string currentDetailID) where TDetailData : DetailData
    {
        var newDetailID = newDetail.name;
        var detailIsAvailable = availableRimsID.ContainsKey(newDetailID);
        var isCurrentDetail = newDetailID == currentDetailID;
        var detailIsFree = newDetail.Price == 0;
        var detailIsSelectable = (detailIsAvailable || detailIsFree) && !isCurrentDetail;
        DisableButton(); 
        if (detailIsSelectable)
        {
            EnableSelectButton();
        }
        else if(!isCurrentDetail)
        {
            EnableBuyButton(newDetail);
        }
    }

    protected void DisableButton()
    {
        try
        {
            _currentButton.gameObject.SetActive(false);
        }
        catch
        {
            _selectButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
        }
    }

    protected void EnableSelectButton()
    {
        _currentButton = _selectButton;
        _currentButton.gameObject.SetActive(true);
        _currentButton.onClick.RemoveAllListeners();
        _currentButton.onClick.AddListener(Select);
    }

    protected void EnableBuyButton(TDetailAsset detail)
    {
        _currentButton = _buyButton;
        _currentButton.gameObject.SetActive(true);
        _currentButton.onClick.RemoveAllListeners();
        _currentButton.onClick.AddListener(Buy);
        _priceText.text = detail.Price.ToString();
    }

    protected TDetailAsset NextDetail(List<TDetailAsset> details)
    {
        _detailIndex++;
        if(_detailIndex >= details.Count)
            _detailIndex = 0;
        return details[_detailIndex];
    }

    protected TDetailAsset PreviousDetail(List<TDetailAsset> details)
    {
        _detailIndex--;
        if (_detailIndex < 0)
            _detailIndex = details.Count - 1;
        return details[_detailIndex];
    }
}
