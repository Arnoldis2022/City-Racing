public class CarControllability : Upgradeable
{
    private float _steerAngleAtSpeed;
    private float _steerSpeed;
    private float _baseSteerAngleAtSpeed;
    private float _maxSteerAngleAtSpeed;
    private float _baseSteerSpeed;
    private float _maxSteerSpeed;
    private int _controllabilityLevel;
    private int _maxControllabilityLevel;

    public float SteerAngleAtSpeed => _steerAngleAtSpeed;
    public float SteerSpeed => _steerSpeed;
    public override int Level => _controllabilityLevel;
    public override int MaxLevel => _maxControllabilityLevel;

    public CarControllability(CarConfig carConfig, int controllabilityLevel)
    {    
        _baseSteerAngleAtSpeed = carConfig.BaseSteerAngleAtSpeed;
        _maxSteerAngleAtSpeed = carConfig.MaxSteerAngleAtSpeed;
        _baseSteerSpeed = carConfig.BaseSteerSpeed;
        _maxSteerSpeed = carConfig.MaxSteerSpeed;
        _maxControllabilityLevel = carConfig.MaxControllabilityLevel;
        _controllabilityLevel = controllabilityLevel;
        _steerAngleAtSpeed = CalculateValue(_baseSteerAngleAtSpeed, _maxSteerAngleAtSpeed);
        _steerSpeed = CalculateValue(_baseSteerSpeed, _maxSteerSpeed);
    }

    public override void LevelUp()
    {
        if (_controllabilityLevel < _maxControllabilityLevel)
        {
            _controllabilityLevel++;
            _steerAngleAtSpeed = CalculateValue(_baseSteerAngleAtSpeed, _maxSteerAngleAtSpeed);
            _steerSpeed = CalculateValue(_baseSteerSpeed, _maxSteerSpeed);
        }
    }
}