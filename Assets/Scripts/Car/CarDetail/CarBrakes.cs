public class CarBrakes : Upgradeable
{
    private float _brakeForce;
    private float _baseBrakeForce;
    private float _maxBrakeForce;
    private int _brakesLevel;
    private int _maxBrakesLevel;

    public float BrakeForce => _brakeForce;
    public override int Level => _brakesLevel;
    public override int MaxLevel => _maxBrakesLevel;

    public CarBrakes(CarConfig carConfig, int brakesLevel)
    {
        _baseBrakeForce = carConfig.BaseBrakeForce;
        _maxBrakeForce = carConfig.MaxBrakeForce;
        _brakesLevel = brakesLevel;
        _maxBrakesLevel = carConfig.MaxBrakesLevel;
        _brakeForce = CalculateValue(_baseBrakeForce, _maxBrakeForce);
    }

    public override void LevelUp()
    {
        if (_brakesLevel < _maxBrakesLevel)
        {
            _brakesLevel++;
            _brakeForce = CalculateValue(_baseBrakeForce, _maxBrakeForce);
        }
    }
}