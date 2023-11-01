using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBoostState : ShipState
{
    private float _boostSpeed;
    private float _accelerationTimer;

    public ShipBoostState(ShipController controller, float boostSPeed) : base(controller)
    {
        _boostSpeed = boostSPeed;
    }

    public override void Enter()
    {
        Controller.CurrentSpeed = _boostSpeed;
        _accelerationTimer = 0f;
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (_accelerationTimer < Controller.BoostAccelerationCurve.keys[2].time)
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
