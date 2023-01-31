using System.Collections;
using UnityEngine;

public class AutoSaver : MonoBehaviour
{
    [SerializeField] private float _intervalPerSecondsBetweenSave = 30f;
    [SerializeField] private Player _player;

    private void Start()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        StartCoroutine(AutoSave());
    }

    private IEnumerator AutoSave()
    {
        while(true)
        {
            yield return new WaitForSeconds(_intervalPerSecondsBetweenSave);
            Game.Instance.UpdatePlayerData(_player);
            Game.Instance.SaveData();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
