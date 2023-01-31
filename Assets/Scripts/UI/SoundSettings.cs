using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider _soundEffectsSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private SoundManager _soundManager;

    public void Init()
    {
        _soundEffectsSlider.value = Game.Instance.SoundEffectsVolume;
        _musicSlider.value = Game.Instance.MusicVolume;
    }

    public void SoundEffectsVolumeChange(float value)
    {
        _soundManager.SoundEffectsVolumeChange(value);
    }

    public void MusicVolumeChange(float value)
    {
        _soundManager.MusicVolumeChange(value);
    }
}
