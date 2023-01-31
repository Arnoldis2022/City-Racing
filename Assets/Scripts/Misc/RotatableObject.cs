using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _rotatableObject;
    [SerializeField] private Vector3 _direction;

    private void Update()
    {
        _rotatableObject.Rotate(_direction, _rotateSpeed * Time.deltaTime);
    }
}