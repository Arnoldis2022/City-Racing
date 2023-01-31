using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Car _car;

    public void Init()
    {
        _car = _player.Car;
    }

    private void Update()
    {
        transform.position = _car.transform.position;
    }
}
