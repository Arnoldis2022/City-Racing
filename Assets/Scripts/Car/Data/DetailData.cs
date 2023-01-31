[System.Serializable]
public abstract class DetailData
{
    public string Id;

    public DetailData(DetailConfig detailConfig)
    {
        Id = detailConfig.name;
    }   
}
