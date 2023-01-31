using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Car _car;

    private void Start()
    {
        _car = _player.Car;
    }

    private void FixedUpdate()
    {
        transform.position = CalculateNewPosition();
    }

    private Vector3 CalculateNewPosition()
    {
        var newPosition = _car.transform.position;
        newPosition.x = transform.position.x;
        newPosition.y = transform.position.y;
        newPosition.z = _car.transform.position.z;
        return newPosition;
    }
}
