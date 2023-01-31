public class CarEngine : Upgradeable
{
    private float _speed;
    private float _engineTorque;
    private float _baseSpeed;
    private float _maxSpeed;
    private float _baseEngineTorque;
    private float _maxEngineTorque;
    private int _engineLevel;
    private int _maxEngineLevel;

    public float Speed => _speed;
    public float EngineTorque => _engineTorque;
    public float MaxSpeed => _maxSpeed;
    public override int Level => _engineLevel;
    public override int MaxLevel => _maxEngineLevel;

    public CarEngine(CarConfig carConfig, int engineLevel)
    {
        _baseSpeed = carConfig.BaseSpeed;
        _maxSpeed = carConfig.MaxSpeed;
        _baseEngineTorque = carConfig.BaseEngineTorque;
        _maxEngineTorque = carConfig.MaxEngineTorque;
        _engineLevel = engineLevel;
        _maxEngineLevel = carConfig.MaxEngineLevel;
        _speed = CalculateValue(_baseSpeed, _maxSpeed);
        _engineTorque = CalculateValue(_baseEngineTorque, _maxEngineTorque);
    }

    public override void LevelUp()
    {
        if (_engineLevel < _maxEngineLevel)
        {
            _engineLevel++;
            _speed = CalculateValue(_baseSpeed, _maxSpeed);
            _engineTorque = CalculateValue(_baseEngineTorque, _maxEngineTorque);
        }
    }
}