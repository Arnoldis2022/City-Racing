using System;
using UnityEngine;

public class PlayerLevel
{
    public Action LevelUP;

    private int _level;
    private int _maxLevel;
    private int _experience;
    private int _maxExperience;
    private float _experienceMultiplierPerLevel = 1.2f;

    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public int Experience => _experience;

    public PlayerLevel(PlayerData playerData, PlayerConfig config)
    {
        _level = playerData.Level;
        _experience = playerData.Experience;
        _maxLevel = config.MaxLevel;
        _maxExperience = config.BaseMaxExperience;
        for(int i = 0; i < _level; i++)
        {
            _maxExperience = Mathf.FloorToInt(_maxExperience * _experienceMultiplierPerLevel);
        }
    }

    public PlayerLevel(PlayerConfig config)
    {
        _level = 0;
        _experience = 0;
        _maxLevel = config.MaxLevel;
    }

    public void AddExperience(int experiance)
    {
        _experience += experiance;
        while(_experience >= _maxExperience)
        {
            _level++;
            LevelUP?.Invoke();
            _experience -= _maxExperience;
            _maxExperience = Mathf.FloorToInt(_maxExperience * _experienceMultiplierPerLevel);
        }
    }
}