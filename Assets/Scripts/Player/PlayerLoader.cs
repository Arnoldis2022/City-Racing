using System.Collections.Generic;

public static class PlayerLoader
{
    public static PlayerData LoadPlayerData(PlayerConfig playerAsset)
    {
        PlayerData playerData = new PlayerData();
        try
        {
            DataSaver<PlayerData> dataSaver = new DataSaver<PlayerData>("Player");
            playerData = dataSaver.LoadData();
        }
        catch
        {
            playerData.CurrentCarID = playerAsset.DefaultCar.name;
            playerData.PurchasedCarsID = new List<string>() { playerData.CurrentCarID };
            playerData.Credits = playerAsset.DefaultCredits;
        }

        return playerData;
    }
}
