using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAcceleratingState : ShipState
{
    private float _accelerationTimer;

    public ShipAcceleratingState(ShipController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _accelerationTimer = 0f;
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (_accelerationTimer < Controller.AccelerationCurve.keys[2].time)
        {
            _accelerationTimer += Time.deltaTime;
            Controller.CurrentSpeed = Controller.AccelerationCurve.Evaluate(_accelerationTimer) * Controller.MaximumSpeed;
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

        if (Controller.CurrentSpeed == Controller.MaximumSpeed)
        {
            Controller.ChangeState(new ShipBoostState(Controller, Controller.Boost1Speed));
        }
    }
}
