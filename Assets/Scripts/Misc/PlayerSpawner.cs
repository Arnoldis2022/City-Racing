using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;

    public void SetPlayerPosition(Car car)
    {
        var spawnIndex = Random.Range(0, _spawnPoints.Count);
        var transform = _spawnPoints[spawnIndex];
        car.transform.position = transform.position;
        car.transform.rotation = transform.rotation;
    }
}
