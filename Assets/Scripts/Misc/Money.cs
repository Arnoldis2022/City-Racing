using System;
using UnityEngine;

public class Money : RotatableObject
{
    public Action<float> TookMoney;

    [SerializeField] private float _money;
    [SerializeField] private string _triggerTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_triggerTag))
        {
            TookMoney?.Invoke(_money);
            gameObject.SetActive(false);
        }
    }
}
