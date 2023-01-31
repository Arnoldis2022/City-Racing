using System;
using System.Collections.Generic;
using UnityEngine;

public class SpoilerPlace : MonoBehaviour
{
    [SerializeField] private Transform _spoilerParent;
    private Spoiler _currentSpoiler;
    private SpoilerConfig _spoilerConfig;
    private Dictionary<string, SpoilerData> _availableSpoilers;

    public Spoiler Spoiler => _currentSpoiler;
    public SpoilerConfig SpoilerConfig => _spoilerConfig;
    public string SpoilerID => _spoilerConfig.name;
    public Dictionary<string, SpoilerData> AvailableSpoilers => _availableSpoilers;

    public void Init(List<SpoilerData> availableSpoilers)
    {
        _availableSpoilers = new Dictionary<string, SpoilerData>();
        foreach(var spoiler in availableSpoilers)
        {
            _availableSpoilers.Add(spoiler.Id, spoiler);
        }    
    }

    public void AddSpoiler(SpoilerConfig spoilerConfig)
    {
        SpoilerData spoilerData = new SpoilerData(spoilerConfig);
        _availableSpoilers.Add(spoilerData.Id, spoilerData);
    }

    public void CreateSpoiler(SpoilerConfig spoilerConfig, Vector3 size, Color color, float smoothness)
    {
        try
        {
            DestroySpoiler();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
        finally
        {
            _spoilerConfig = spoilerConfig;
            if(_spoilerConfig.Prefab != null)
            {
                var spoiler = spoilerConfig.Prefab as Spoiler;
                _currentSpoiler = Instantiate(spoiler, _spoilerParent.position, _spoilerParent.rotation, _spoilerParent);
                _currentSpoiler.SetColor(color, smoothness);
                _currentSpoiler.transform.localScale = size;
            }
        }
    }

    public void DestroySpoiler()
    {
        if(_currentSpoiler != null)
        {
            Destroy(_currentSpoiler.gameObject);
        }
        else
        {
            throw new NullReferenceException();
        }
    }
}