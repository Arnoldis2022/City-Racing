using UnityEngine;

public class HighwayCarControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Car _car;
    private UserInput _input;
    private Quaternion _startRotation;
    private Quaternion _leftBorder;
    private Quaternion _rightBorder;

    private void FixedUpdate()
    {
        CarLogic();
    }

    private void CarLogic()
    {
        TurnCar(_car.Wheelbase.FrontRightWheelColliders);
        TurnCar(_car.Wheelbase.FrontLeftWheelColliders);


        CarMove(_car.Wheelbase.RearRightWheelColliders);
        CarMove(_car.Wheelbase.RearLeftWheelColliders);
        RotateWheel(_car.Wheelbase.FrontRightWheelColliders, _car.Wheelbase.FrontRightWheel);
        RotateWheel(_car.Wheelbase.FrontLeftWheelColliders, _car.Wheelbase.FrontLeftWheel);
        RotateWheel(_car.Wheelbase.RearRightWheelColliders, _car.Wheelbase.RearRightWheel);
        RotateWheel(_car.Wheelbase.RearLeftWheelColliders, _car.Wheelbase.RearLeftWheel);
    }

    private void CarMove(WheelCollider wheel)
    {
        if (_car.Rigidbody.velocity.magnitude < _car.Engine.MaxSpeed)
        {
            wheel.motorTorque = 1000;
        }
        else
        {
            wheel.motorTorque = 0f;
        }
    }

    private void TurnCar(WheelCollider wheel)
    {
        var inputAxis = _input.Car.Move.ReadValue<Vector2>();
        var onLeft = -inputAxis.x < 0 && _car.transform.rotation.y > _leftBorder.y;
        var onRight = -inputAxis.x > 0 && _car.transform.rotation.y < _rightBorder.y;
        if (onLeft || onRight)
        {
            var timeForTurn = 50f - ((50f * 0.5f) * _car.Rigidbody.velocity.magnitude / _car.Engine.MaxSpeed);
            var angle = 10f - ((10f * 0.5f) * _car.Rigidbody.velocity.magnitude / _car.Engine.MaxSpeed);
            _leftBorder.y = _startRotation.y + Quaternion.Euler(new Vector3(0f, -angle, 0f)).y;
            _rightBorder.y = _startRotation.y + Quaternion.Euler(new Vector3(0f, angle, 0f)).y;
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, angle * -inputAxis.x, timeForTurn * Time.deltaTime);
        }
        else if((_car.transform.rotation.y >= _startRotation.y + 0.05f || _car.transform.rotation.y <= _startRotation.y - 0.05f) && inputAxis.x == 0f)
        {
            var direction = _startRotation.y - _car.transform.rotation.y;
            if(direction < 0)
            {
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, -10f, 10f * Time.deltaTime);
            }
            else if(direction > 0)
            {
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, 10f, 10f * Time.deltaTime);
            }
        }
        else
        {
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, 0, 50f * Time.deltaTime);
        }
    }

    private void RotateWheel(WheelCollider wheelCollider, Wheel wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.gameObject.transform.position = position;
        wheelTransform.gameObject.transform.rotation = rotation;
    }

    private void OnEnable()
    {
        _input = new UserInput();
        _input.Enable();
        _car = _player.Car;
        _startRotation = _car.transform.rotation;
        _leftBorder.y = _startRotation.y + Quaternion.Euler(new Vector3(0f, -10f, 0f)).y;
        _rightBorder.y = _startRotation.y + Quaternion.Euler(new Vector3(0f, 10f, 0f)).y;
        Debug.Log("Start quaternion rotation " + _car.transform.rotation);
        Debug.Log("Left border quaternion rotation " + _leftBorder);
        Debug.Log("Right  border quaternion rotation " + _rightBorder);
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void Move()
    {
        //_car.Wheelbase.FrontWheels
    }
}
