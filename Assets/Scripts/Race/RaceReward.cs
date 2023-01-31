using System;
using UnityEngine;

[Serializable]
public class RaceReward
{
    [SerializeField] private float _credits;
    [SerializeField] private int _experience;

    public float Credits => _credits;
    public int Experience => _experience;
}
