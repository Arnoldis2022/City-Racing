using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musicClips;
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _currentAudioClip;

    private void Awake()
    {
        SetRandomMusic();
    }

    private void Update()
    {
        if(!_audioSource.isPlaying)
        {
            SetRandomMusic();
        }
    }

    private void SetRandomMusic()
    {
        var clipsWithoutCurrentClip = _musicClips.Where(music => music != _currentAudioClip).ToList();
        var randomIndex = Random.Range(0, clipsWithoutCurrentClip.Count);
        _currentAudioClip = clipsWithoutCurrentClip[randomIndex];
        _audioSource.PlayOneShot(_currentAudioClip);
    }
}
