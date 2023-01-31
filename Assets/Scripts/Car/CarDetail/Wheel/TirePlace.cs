using System;
using UnityEngine;

public class TirePlace : MonoBehaviour
{
    [SerializeField] private Transform _tireParent;
    [SerializeField] private Tire _currentTire;

    public Transform TireParent => _tireParent;

    public void CreateTire(Tire tire)
    {
        try
        {
            Destroy(_currentTire.gameObject);
        }
        catch(Exception exception)
        {
            Debug.LogWarning(exception);
        }
        finally
        {
            _currentTire = Instantiate(tire, _tireParent.position, _tireParent.rotation, _tireParent);
        }
    }
}