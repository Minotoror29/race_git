using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerControls _playerControls;

    [SerializeField] private float maximumSpeed = 2500f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private TextMeshProUGUI speedMeter;
    private float _currentSpeed = 0f;
    private float _accelerationTimer = 0f;

    [SerializeField] private float boost1Speed = 5000f;
    [SerializeField] private float boost2Speed = 7500f;
    [SerializeField] private float boost3Speed = 10000f;

    [SerializeField] private float rotationSpeed = 1f;
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
        _playerControls.InGame.Boost.performed += ctx => Boost();
    }

    private void Update()
    {
        HandleAccelerationInput();
        HandleRotationInput();

        speedMeter.text = ((int)_currentSpeed).ToString();
    }

    private void HandleAccelerationInput()
    {
        if (_playerControls.InGame.Accelerate.ReadValue<float>() > 0)
        {
            if (_accelerationTimer < accelerationCurve.keys[2].time)
            {
                _accelerationTimer += Time.deltaTime;
                _currentSpeed = accelerationCurve.Evaluate(_accelerationTimer) * maximumSpeed;
            }
        }
        else
        {
            _accelerationTimer = 0f;
            _currentSpeed = 0f;
        }
    }

    private void HandleRotationInput()
    {
        _rotationInput = _playerControls.InGame.Rotate.ReadValue<Vector2>().x;
    }

    private void Boost()
    {
        Debug.Log("Boost");

        if (_currentSpeed == maximumSpeed)
        {
            _currentSpeed = boost1Speed;
        } else if (_currentSpeed == boost1Speed)
        {
            _currentSpeed = boost2Speed;
        } else if (_currentSpeed == boost2Speed)
        {
            _currentSpeed = boost3Speed;
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
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, _rotationInput * rotationSpeed, 0));
    }
}
