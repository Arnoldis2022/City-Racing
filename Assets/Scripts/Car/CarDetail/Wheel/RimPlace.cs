using System;
using UnityEngine;

public class RimPlace : MonoBehaviour
{
    [SerializeField] private Transform _rimParent;
    [SerializeField] private Rim _currentRim;

    public Transform RimParent => _rimParent;
    public Rim CurrentRim => _currentRim;

    public void CreateRim(Rim rim, float width, float radius)
    {
        try
        {
            Destroy(_currentRim.gameObject);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
        }
        finally
        {
            _currentRim = Instantiate(rim, _rimParent.position, _rimParent.rotation, _rimParent);
            _currentRim?.SetColor(_currentRim.BaseColor, _currentRim.MaterialSmoothness);
            _currentRim.transform.localScale = new Vector3(width, radius, radius);
        }
    }

    public void SetRimSize(float width, float radius)
    {
        _currentRim.transform.localScale = new Vector3(width, radius, radius);
    }
}