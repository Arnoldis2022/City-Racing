using System.Collections.Generic;
using YG;

public class Localization
{
    private Dictionary<string, LocalizationTexts> _localizationsDictionary;
    private LocalizationTexts _localization;

    public LocalizationTexts CurrentLocalization => _localization;

    public Localization(List<LocalizationTexts> localizationTexts)
    {
        _localizationsDictionary = new Dictionary<string, LocalizationTexts>();
        foreach (var localization in localizationTexts)
        {
            _localizationsDictionary.Add(localization.Language, localization);
        }
    }

    public void SwitchLanguage(string language)
    {
        if(_localizationsDictionary.ContainsKey(language))
        {
            _localization = _localizationsDictionary[language];
            _localization.Init();
        }
    }
}