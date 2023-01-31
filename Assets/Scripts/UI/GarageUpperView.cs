using System;
using UnityEngine;

public class GarageUpperView : MonoBehaviour
{
    [SerializeField] private Garage _garage;
    [SerializeField] private Player _player;
    [SerializeField] private InfoWindow _infoWindow;

    private GarageUpper _upper => _garage.Upper;
}
