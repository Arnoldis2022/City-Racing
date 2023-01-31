public abstract class Upgradeable
{
    public abstract int Level { get; }
    public abstract int MaxLevel { get; }
    public abstract void LevelUp();

    protected float CalculateValue(float baseValue, float maxValue)
    {
        var valueIncrease = (maxValue - baseValue) / MaxLevel;
        return baseValue + Level * valueIncrease;
    }
}