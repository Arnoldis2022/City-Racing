using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    private UserInput _input;
    private Car _car;

    private void Start()
    {
        _car = _player.Car;
        _car.Rigidbody.centerOfMass = Vector3.zero;
        _input = new UserInput();
        _input.Enable(); 
        _input.Car.Brake.started += context => { SlowDown(float.MaxValue, 1f); };
        _input.Car.Brake.canceled += context => { SlowDown(0f, 0.5f);};
    }

    private void FixedUpdate()
    {
        Turn(_car.Wheelbase.FrontRightWheelColliders);
        Turn(_car.Wheelbase.FrontLeftWheelColliders);
        Move(_car.Wheelbase.FrontRightWheelColliders);
        Move(_car.Wheelbase.FrontLeftWheelColliders);
        RotateWheel(_car.Wheelbase.FrontRightWheelColliders, _car.Wheelbase.FrontRightWheel);
        RotateWheel(_car.Wheelbase.FrontLeftWheelColliders, _car.Wheelbase.FrontLeftWheel);
        RotateWheel(_car.Wheelbase.RearRightWheelColliders, _car.Wheelbase.RearRightWheel);
        RotateWheel(_car.Wheelbase.RearLeftWheelColliders, _car.Wheelbase.RearLeftWheel);
    }

    private void Turn(WheelCollider wheel)
    {
        var moveVector = _input.Car.Move.ReadValue<Vector2>();
        wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, 45f * -moveVector.x, 10f * Time.deltaTime);
    }

    private void RotateWheel(WheelCollider wheelCollider, Wheel wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.gameObject.transform.position = position;
        wheelTransform.gameObject.transform.rotation = rotation;
    }

    private void Move(WheelCollider wheel)
    {
        var moveVector = _input.Car.Move.ReadValue<Vector2>();
        if(moveVector.y != 0)
        {
            _car.Rigidbody.drag = 0.05f;
            if (_car.Rigidbody.velocity.magnitude < _car.Engine.MaxSpeed)
            {
                wheel.motorTorque = 10000f * moveVector.y;
            }
            else
            {
                wheel.motorTorque = 0f;
            }
        }
        else
        {
            wheel.motorTorque = 0f;
        }
    }


    private void SlowDown(float brakeTorque, float drag)
    {
        _car.Wheelbase.RearLeftWheelColliders.brakeTorque = brakeTorque;
        _car.Wheelbase.RearLeftWheelColliders.brakeTorque = brakeTorque;
        _car.Rigidbody.drag = drag;
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}