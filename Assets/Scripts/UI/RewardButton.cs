using UnityEngine;
using YG;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private float _reward;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += RewardADComplete;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RewardADComplete;
    }

    public void ShowRewardAD()
    {
        YandexGame.RewVideoShow(0);
    }

    private void RewardADComplete(int id)
    {
        _player.IncreaseCredits(_reward);
        gameObject.SetActive(false);
    }
}
