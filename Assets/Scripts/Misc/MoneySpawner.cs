using System;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private Money _moneyPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private string _collectMoneyTextKey;
    [SerializeField] private int _moneyQuantityOnMap;

    public void Init()
    {
        var spawnPoint = _spawnPoints;
        for(int i = 0; i < _moneyQuantityOnMap; i++)
        {
            var pointIndex = UnityEngine.Random.Range(0, spawnPoint.Count);
            var point = spawnPoint[pointIndex];
            spawnPoint.RemoveAt(pointIndex);
            var money = Instantiate(_moneyPrefab, point.position, point.rotation, point);
            money.TookMoney += MoneyCollect;
        }
    }

    private void MoneyCollect(float money)
    {
        _soundManager.PlayOneShot(_collectSound);
        _player.IncreaseCredits(money);
    }
}
