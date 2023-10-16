using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerControls _playerControls;

    [SerializeField] private float maximumSpeed;
    private float _currentSpeed = 0f;

    [SerializeField] private float rotationSpeed = 1f;
    private float _rotationInput = 0f;

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

        Rotate();
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

    private void FixedUpdate()
    {
        Accelerate();
    }

    private void Accelerate()
    {
        _rb.velocity = transform.forward * _currentSpeed;
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, _rotationInput * rotationSpeed, 0));
    }
}
