using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LocalizationLoader : MonoBehaviour
{
    public static LocalizationLoader Instance { get; private set; }
    public Dictionary<string, string> UiLocalization => _localization;
    private Dictionary<string, string> _localization;
    private string _language;

    public void Init()
    {
        if (Instance != null)
        {
            Debug.LogError("Localization object is already exist!");
        }
        else
        {
            Instance = this;
            Instance.LoadLocalization(_language);
            DontDestroyOnLoad(Instance);
        }
    }

    private void LoadLocalization(string language)
    {
        _localization = new Dictionary<string, string>();
        var localizationFile = Resources.Load<TextAsset>("Configs/Localization");
        var localizationXML = new XmlDocument();

        using (var reader = new StringReader(localizationFile.ToString()))
        {
            localizationXML.Load(reader);
        }

        XmlElement rootElement = localizationXML.DocumentElement;

        foreach(XmlNode node in rootElement)
        {
            XmlNode languageAttribute = node.Attributes.GetNamedItem("language");
            if(languageAttribute.Value == language)
            {
                foreach(XmlNode childNode in node.ChildNodes)
                {
                    if(childNode.Name == "element")
                    {
                        XmlNode key = childNode.Attributes.GetNamedItem("key");
                        _localization.Add(key.Value, childNode.InnerText);
                    }
                    else
                    {
                        Debug.LogError("Error read localization settings!");
                    }
                }   
            }
        }
    }

    public void LanguageChange(string language)
    {
        Instance.LoadLocalization(language);
    }
}
