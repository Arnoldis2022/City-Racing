using UnityEngine;

public class StaticCarControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _leftBorder;
    [SerializeField] private Vector3 _rightBorder;
    private Car _car;
    private UserInput _userInput;

    private void Start()
    {
        _userInput = new UserInput();
        _userInput.Enable();
        _car = _player.Car;
        _player.transform.LookAt(_target);
    }

    private void Update()
    {
        var moveVector = _userInput.Car.Move.ReadValue<Vector2>();
        if(-moveVector.x > 0)
        {
            var newPosition = _target.position + _rightBorder;
            _target.position = Vector3.Lerp(_target.position, newPosition, _car.Engine.MaxSpeed * Time.deltaTime);
        } 
        else if(-moveVector.x < 0)
        {
            var newPosition = _target.position + _leftBorder;
            _target.position = Vector3.MoveTowards(_target.position, newPosition, _car.Engine.MaxSpeed * Time.deltaTime);
        }
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, _target.position, _car.Engine.MaxSpeed / 2 * Time.deltaTime);

    }
}
