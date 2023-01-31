using UnityEngine;

public class CarMarker : MonoBehaviour
{
    [SerializeField] private float _height = 150f;
    [SerializeField] private MeshRenderer _meshRenderer;
    private Transform _carTransform;

    public void Init(Car car)
    {
        _carTransform = car.transform;
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    private void Update()
    {
        var newPosition = _carTransform.position;
        newPosition.y = _height;
        transform.position = newPosition;

        transform.rotation = Quaternion.Slerp(transform.rotation, _carTransform.rotation, 100f * Time.deltaTime);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
}
