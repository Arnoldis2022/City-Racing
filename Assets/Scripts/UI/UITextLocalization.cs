using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITextLocalization : MonoBehaviour
{
    [SerializeField] private string _uiTextKey;
    [SerializeField] private TextMeshProUGUI _uiText;
    private LocalizationTexts _localization;

    private void Start()
    {
        try
        {
            _localization = Game.Instance.Localization;
            if (_uiText == null)
                _uiText = GetComponent<TextMeshProUGUI>();
            _uiText.text = _localization.GetText(_uiTextKey);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
