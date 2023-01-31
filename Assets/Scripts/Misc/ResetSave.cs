using UnityEngine;
using YG;

public class ResetSave : MonoBehaviour
{
    [SerializeField] private Player _player;
    public void SaveReset()
    {
        YandexGame.ResetSaveProgress();
        var playerData = new PlayerData(_player.Config);
        YandexGame.savesData.PlayerData = playerData;
        YandexGame.savesData.GarageData = null;
        YandexGame.savesData.CarsData = null;
        YandexGame.SaveProgress();
    }
}
