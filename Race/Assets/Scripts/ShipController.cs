using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerControls _playerControls;

    [SerializeField] private float maximumSpeed;
    [SerializeField] private float brakeSpeed;
    private float _currentSpeed = 0f;

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float rotationBrakeSpeed = 1f;
    private float _rotationInput = 0f;

    public float CurrentSpeed { get { return _currentSpeed; } }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody>();

        _playerControls = new PlayerControls();
        _playerControls.InGame.Enable();
    }

    private void Update()
    {
        HandleAccelerationInput();
        HandleRotationInput();
        HandleBoostInput();
    }

    private void HandleAccelerationInput()
    {
        if (_playerControls.InGame.Accelerate.ReadValue<float>() > 0)
        {
            _currentSpeed = maximumSpeed;
        }
        else
        {
            _currentSpeed = 0f;
        }
    }

    private void HandleRotationInput()
    {
        _rotationInput = _playerControls.InGame.Rotate.ReadValue<Vector2>().x;
    }

    private void HandleBoostInput()
    {
        if (_playerControls.InGame.Boost.ReadValue<float>() > 0)
        {
            _currentSpeed *= 2;
        }
    }

    private void FixedUpdate()
    {
        Accelerate();
        Rotate();
    }

    private void Accelerate()
    {
        _rb.velocity = _currentSpeed * Time.fixedDeltaTime * transform.forward;

        //if (_currentSpeed > 0f)
        //{
        //    _rb.AddForce(_currentSpeed * Time.fixedDeltaTime * transform.forward, ForceMode.Impulse);
        //}

        //Brake();

        //Debug.Log(_rb.velocity.magnitude);
    }

    private void Brake()
    {
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, brakeSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, _rotationInput * rotationSpeed, 0));

        //if (_rotationInput != 0f)
        //{
        //    _rb.AddTorque(_rotationInput * rotationSpeed * Time.fixedDeltaTime * transform.up, ForceMode.Impulse);
        //}
        //StopAngularRotation();
    }

    private void StopAngularRotation()
    {
        _rb.angularVelocity = Vector3.Lerp(_rb.angularVelocity, Vector3.zero, rotationBrakeSpeed * Time.fixedDeltaTime);
        //_rb.angularVelocity = Vector3.zero;
    }
}
