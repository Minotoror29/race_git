using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipBoostState : ShipState
{
    private float _boostSpeed;
    private float _accelerationTimer;
    private CinemachineVirtualCamera _cam;
    private int _index;

    public ShipBoostState(ShipController controller, ShipStateFactory factory, float boostSpeed, CinemachineVirtualCamera cam, int index) : base(controller, factory)
    {
        _boostSpeed = boostSpeed;
        _cam = cam;
        _index = index;
    }

    public override void Enter()
    {
        _accelerationTimer = 0f;

        Controller.CameraManager.CameraTransition(_cam);
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (_accelerationTimer < Controller.BoostAccelerationCurve.keys[Controller.BoostAccelerationCurve.keys.Count() - 1].time)
        {
            _accelerationTimer += Time.deltaTime;
            Controller.CurrentSpeed = Controller.BoostAccelerationCurve.Evaluate(_accelerationTimer) * Controller.MaximumSpeed + (_boostSpeed - Controller.MaximumSpeed);
        }

        if (PlayerControls.InGame.Accelerate.ReadValue<float>() == 0)
        {
            Controller.ChangeState(Factory.IdleState);
        }
    }

    public override void UpdatePhysics()
    {
    }

    public override void Boost()
    {
        base.Boost();

        if (_index < Factory.BoostStates.Count - 1 && Controller.CurrentSpeed == _boostSpeed)
        {
            Controller.ChangeState(Factory.BoostStates[_index + 1]);
        }
    }
}
