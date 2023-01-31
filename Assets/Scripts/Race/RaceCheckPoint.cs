using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RaceCheckPoint : MonoBehaviour
{
    public Action<RaceCheckPoint, Car> CheckpointPassed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Car car))
        {
            CheckpointPassed?.Invoke(this, car);
        }
    }
}
