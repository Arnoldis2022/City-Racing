using UnityEngine;

public class DetailConfig : ScriptableObject
{
    [SerializeField] private Detail _prefab;
    [SerializeField] private float _price;

    public Detail Prefab => _prefab;
    public float Price => _price;
}