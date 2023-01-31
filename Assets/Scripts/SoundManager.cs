using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private List<AudioSource> _otherSoundSources;

    public void Init()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        _otherSoundSources = new List<AudioSource>(audioSources);
        _otherSoundSources.Remove(_musicSource);
        _otherSoundSources.Remove(_effectsSource);
        var soundEffectsVolume = Game.Instance.SoundEffectsVolume;
        var musicVolume = Game.Instance.MusicVolume;
        SoundEffectsVolumeChange(soundEffectsVolume);
        MusicVolumeChange(musicVolume);
    }

    public void SoundEffectsVolumeChange(float volume)
    {
        foreach(var soundEffect in _otherSoundSources)
        {
            soundEffect.volume = volume;
            _effectsSource.volume = volume;
            _player.Car.EngineSoundVolume(volume);
        }
        Game.Instance.UpdateSoundEffectsVolumeData(volume);
    }

    public void MusicVolumeChange(float volume)
    {
        _musicSource.volume = volume;
        Game.Instance.UpdateMusicVolumeData(volume);
    }

    public void PlayOneShot(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }
}