using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _cameraMoveSpeed;
    [SerializeField] private float _cameraRotateSpeed;
    private Car _car;

    public void Init()
    {
        _car = _player.Car;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _car.transform.position, _cameraMoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _car.transform.rotation, _cameraRotateSpeed * Time.deltaTime);
    }
}
