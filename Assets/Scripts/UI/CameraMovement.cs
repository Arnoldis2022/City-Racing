using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using System;

public class CameraMovement : MonoBehaviour
{
    public UnityEvent MoveEnd;

    [SerializeField] private float _speed;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private IEnumerator _moveable;

    private void Awake()
    {
        
    }

    public void MoveTo(Transform target)
    {
        try
        {
            StopCoroutine(_moveable);
        }
        catch(Exception exception)
        {
            Debug.LogWarning(exception);
        }
        finally
        {
            _moveable = Move(target);
            StartCoroutine(_moveable);
        }
    }

    private IEnumerator Move(Transform target)
    {
        var distance = Vector3.Distance(target.position, transform.position);
        while (distance > 0.1f)
        {
            transform.position += (target.position - transform.position).normalized * _speed * Time.deltaTime; //Vector3.Lerp(transform.position, target.position, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, _speed * Time.deltaTime);
            distance = Vector3.Distance(target.position, transform.position);
            yield return null;
        }
        MoveEnd?.Invoke();
    }
}
