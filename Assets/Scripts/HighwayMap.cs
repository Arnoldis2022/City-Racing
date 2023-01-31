using System.Collections.Generic;
using UnityEngine;

public class HighwayMap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _mapSize;
    [SerializeField] private List<Transform> _chunks;
    private Car _car;

    private void Start()
    {
        _car = _player.Car;
    }

    private void Update()
    {
        if(_car.transform.position.z >= _chunks[1].position.z)
        {
            var chunk = _chunks[0];
            var newChunkPosition = _chunks[2].position;
            newChunkPosition.z += _mapSize.z;
            chunk.position = newChunkPosition;
            _chunks.Remove(chunk);
            _chunks.Add(chunk);
        }
    }
}
