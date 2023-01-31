using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Localization texts")]
public class LocalizationTexts : ScriptableObject
{
    [SerializeField] private string _language;
    [SerializeField] private List<LocalizationData> _localizationData;
    private Dictionary<string, LocalizationData> _localizationDataDictionary;

    public string Language => _language;

    public void Init()
    {
        _localizationDataDictionary = new Dictionary<string, LocalizationData>();
        foreach(var data in _localizationData)
        {
            _localizationDataDictionary.Add(data.UiTextKey, data);
        }
    }

    public string GetText(string key)
    {
        return _localizationDataDictionary[key].UiText;
    }
}
