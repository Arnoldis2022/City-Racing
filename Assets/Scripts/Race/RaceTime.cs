public struct RaceTime
{
    private int _minute;
    private int _seconds;
    private int _milliseconds;

    public RaceTime(float milliseconds)
    {
        _milliseconds = (int)(milliseconds % 1000) / 10;
        _seconds = (int)(milliseconds / 1000) % 60;
        _minute = (int)((milliseconds / 1000) / 60) % 60;
    }

    public override string ToString()
    {
        return _minute.ToString("D2") + " : " + _seconds.ToString("D2") + " : " + _milliseconds.ToString("D2");
    }
}
