public class NitrousOxideSystem : Upgradeable
{
    private int _level;
    private float _nosCapacity;
    private float _baseNOSCapacity;
    private float _maxNOSCapacity;
    private int _maxLevel;

    public override int Level => _level;
    public override int MaxLevel => _maxLevel;
    public float NOSCapacity => _nosCapacity;


    public NitrousOxideSystem(CarConfig config, int nosLevel)
    {
        _level = nosLevel;
        _baseNOSCapacity = config.BaseNOSCapacity;
        _maxNOSCapacity= config.MaxNOSCapacity;
        _maxLevel = config.MaxNOSLevel;
        _nosCapacity = CalculateValue(_baseNOSCapacity, _maxNOSCapacity);
    }

    public override void LevelUp()
    {
        if (_level < _maxLevel)
        {
            _level++;
            _nosCapacity = CalculateValue(_baseNOSCapacity, _maxNOSCapacity);
        }
    }
}
