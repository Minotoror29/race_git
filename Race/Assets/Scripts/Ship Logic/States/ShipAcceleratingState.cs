using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipAcceleratingState : ShipState
{
    private float _accelerationTimer;
    private float _startSpeed;

    public ShipAcceleratingState(ShipController controller, ShipStateFactory factory) : base(controller, factory)
    {
    }

    public override void Enter()
    {
        _accelerationTimer = (Controller.CurrentSpeed * 5f) / Controller.MaximumSpeed;
        _startSpeed = Controller.CurrentSpeed;
        Controller.CameraManager.CameraTransition(Controller.AccelerationCam);
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (_accelerationTimer < Controller.AccelerationCurve.keys[Controller.AccelerationCurve.keys.Count() - 1].time)
        {
            _accelerationTimer += Time.deltaTime;
            Controller.CurrentSpeed = Controller.AccelerationCurve.Evaluate(_accelerationTimer) * Controller.MaximumSpeed;
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

        if (Controller.CurrentSpeed == Controller.MaximumSpeed)
        {
            Controller.ChangeState(Factory.BoostStates[0]);
        }
    }
}
