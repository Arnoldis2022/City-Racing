using UnityEngine;

[CreateAssetMenu(menuName = "Mega Racing/Create Player Asset")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private CarConfig _defaultCar;
    [SerializeField] private float _defaultCredits;
    [SerializeField] private int _defaultLevel;
    [SerializeField] private float _maxCredits;
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _baseMaxExperience;

    public CarConfig DefaultCar => _defaultCar;
    public int DefaultLevel => _defaultLevel;
    public float DefaultCredits => _defaultCredits;
    public float MaxCredits => _maxCredits;
    public int BaseMaxExperience => _baseMaxExperience;
    public int MaxLevel => _maxLevel;
}
