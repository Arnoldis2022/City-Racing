using UnityEngine;

public class MoveableWindow : Window
{
    [SerializeField] private Transform _viewTransform;
    public Transform ViewTransform => _viewTransform;
}
