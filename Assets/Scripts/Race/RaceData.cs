[System.Serializable]
public class RaceData
{
    public string RaceName;
    public int PlayerTimeRecord;

    public RaceData(string raceName, int playerTimePerMilliseconds)
    {
        RaceName = raceName;
        PlayerTimeRecord = playerTimePerMilliseconds;
    }
}