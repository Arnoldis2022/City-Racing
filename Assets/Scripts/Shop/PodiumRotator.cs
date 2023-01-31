using UnityEngine;

public class PodiumRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Vector3 _rotateAxis;

    private void Update()
    {
        transform.Rotate(_rotateAxis, _rotateSpeed * Time.deltaTime, Space.World);
    }
}
