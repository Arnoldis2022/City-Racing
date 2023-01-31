using System;

public class FuelChecker : BaseChecker
{
    public Action FuelIsOut;

    private Player _player;
    private Car _car;

    public override void Init(Player player)
    {
        _player = player;
        _car = _player.Car;
    }


    public override void Check()
    {
        if (_car.FuelQuantity <= 0)
        {
            FuelIsOut?.Invoke();
            _car.KillEngine();
        }
    }
}
