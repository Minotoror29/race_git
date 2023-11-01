using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerControls _playerControls;

    private ShipState _currentState;

    [SerializeField] private float maximumSpeed = 2500f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private TextMeshProUGUI speedMeter;
    private float _currentSpeed = 0f;
    private float _accelerationTimer = 0f;

    [SerializeField] private float boost1Speed = 5000f;
    [SerializeField] private float boost2Speed = 7500f;
    [SerializeField] private float boost3Speed = 10000f;
    [SerializeField] private AnimationCurve boostAccelerationCurve;

    [SerializeField] private float rotationSpeed = 1f;
    private float _rotationInput = 0f;

    public Rigidbody Rb { get { return _rb; } }
    public PlayerControls PlayerControls { get { return _playerControls; } }
    public float MaximumSpeed { get { return maximumSpeed; } }
    public AnimationCurve AccelerationCurve { get { return accelerationCurve; } }
    public float CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
    public float Boost1Speed { get { return boost1Speed; } }
    public float Boost2Speed { get { return boost2Speed; } }
    public float Boost3Speed { get { return boost3Speed; } }
    public AnimationCurve BoostAccelerationCurve { get { return boostAccelerationCurve; } }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateLogic();
    }

    private void FixedUpdate()
    {
        UpdatePhysics();
    }

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody>();

        _playerControls = new PlayerControls();
        _playerControls.InGame.Enable();
        _playerControls.InGame.Boost.performed += ctx => Boost();

        ChangeState(new ShipIdleState(this));
    }

    public void ChangeState(ShipState nextState)
    {
        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    private void UpdateLogic()
    {
        _currentState.UpdateLogic();

        //HandleAccelerationInput();
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
        _currentState.Boost();

        //if (_currentSpeed == maximumSpeed)
        //{
        //    _currentSpeed = boost1Speed;
        //} else if (_currentSpeed == boost1Speed)
        //{
        //    _currentSpeed = boost2Speed;
        //} else if (_currentSpeed == boost2Speed)
        //{
        //    _currentSpeed = boost3Speed;
        //}
    }

    private void UpdatePhysics()
    {
        _currentState.UpdatePhysics();

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