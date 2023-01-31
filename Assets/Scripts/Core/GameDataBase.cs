using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Game Data Base")]
public class GameDataBase : ScriptableObject
{
    [SerializeField] private List<CarConfig> _cars;
    [SerializeField] private List<LocalizationTexts> _localizationTexts;
    [SerializeField] private List<MusicData> _musics;
    [SerializeField] private List<GraphicsSettings> _graphicsSettings;

    public List<CarConfig> Cars => _cars;
    public List<LocalizationTexts> LocalizationTexts => _localizationTexts;
    public List<GraphicsSettings> GraphicsSettings => _graphicsSettings;
}