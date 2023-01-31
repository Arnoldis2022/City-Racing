using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MoveableWindow
{
    [SerializeField] private Dropdown _graphicsDropdown;
    [SerializeField] private SoundSettings _soundSettings;

    private void OnEnable()
    {
        _soundSettings.Init();
        _graphicsDropdown.value = Game.Instance.GraphicsQualityIndex;
    }

    public void QualityIndexChange(int index)
    {
        Game.Instance.UpdateGraphicsSettingsData(index);
    }
}
