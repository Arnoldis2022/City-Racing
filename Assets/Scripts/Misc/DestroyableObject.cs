using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _timeToDestroyPerSeconds;
    [SerializeField] private List<int> _collisionLayers;

    private void OnCollisionEnter(Collision collision)
    {
        if(_collisionLayers.Contains(collision.gameObject.layer))
        {
            StartCoroutine(CountdownToDestruction());
        }
    }

    private IEnumerator CountdownToDestruction()
    {
        yield return new WaitForSeconds(_timeToDestroyPerSeconds);
        Destroy(gameObject);
    }
}
