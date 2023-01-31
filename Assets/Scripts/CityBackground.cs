using UnityEngine;

public class CityBackground : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _background;
    [SerializeField] private Camera _camera;
    private Transform _target;

    public void Init()
    {
        if (_camera == null)
            _camera = Camera.main;
        var farClipPlane = _camera.farClipPlane - 5;
        var width = farClipPlane;
        var height = width * 0.08f;
        var backgroundSize = new Vector3(width, height, width);
        _background.transform.localScale = backgroundSize;
        _target = _player.Car.transform;
    }

    private void FixedUpdate()
    {
        transform.position = _target.position;
    }
}
