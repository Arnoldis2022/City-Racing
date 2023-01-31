using UnityEngine;

[System.Serializable]
public class LocalizationData
{
    [SerializeField] private string _uiTextKey;
    [TextArea]
    [SerializeField] private string _uiText;

    public string UiTextKey => _uiTextKey;
    public string UiText => _uiText;
}
