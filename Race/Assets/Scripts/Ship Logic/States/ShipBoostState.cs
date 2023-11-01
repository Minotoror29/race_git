using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipBoostState : ShipState
{
    private float _boostSpeed;
    private float _accelerationTimer;

    public ShipBoostState(ShipController controller, float boostSpeed) : base(controller)
    {
        _boostSpeed = boostSpeed;
    }

    public override void Enter()
    {
        Controller.CurrentSpeed = _boostSpeed;
        _accelerationTimer = 0f;

        if (_boostSpeed == Controller.Boost1Speed)
        {
            Controller.CameraManager.CameraTransition(Controller.Boost1Cam);
        }
        else if (_boostSpeed == Controller.Boost2Speed)
        {
            Controller.CameraManager.CameraTransition(Controller.Boost2Cam);
        }
        else if (_boostSpeed == Controller.Boost3Speed)
        {
            Controller.CameraManager.CameraTransition(Controller.Boost3Cam);
        }
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
            Controller.ChangeState(new ShipIdleState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
    }

    public override void Boost()
    {
        base.Boost();

        if (Controller.CurrentSpeed == Controller.Boost1Speed)
        {
            Controller.ChangeState(new ShipBoostState(Controller, Controller.Boost2Speed));
        } else if (Controller.CurrentSpeed == Controller.Boost2Speed)
        {
            Controller.ChangeState(new ShipBoostState(Controller, Controller.Boost3Speed));
        }
    }
}
