using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipIdleState : ShipState
{
    private float _brakingTimer;
    private float _startSpeed;

    public ShipIdleState(ShipController controller, ShipStateFactory factory) : base(controller, factory)
    {

    }

    public override void Enter()
    {
        Controller.CameraManager.CameraTransition(Controller.IdleCam);
        _brakingTimer = 0f;
        _startSpeed = Controller.CurrentSpeed;
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (_brakingTimer < Controller.BrakeCurve.keys[Controller.BrakeCurve.keys.Count() - 1].time)
        {
            _brakingTimer += Time.deltaTime;
            Controller.CurrentSpeed = Controller.BrakeCurve.Evaluate(_brakingTimer) * _startSpeed;
        }

        if (PlayerControls.InGame.Accelerate.ReadValue<float>() > 0 && Controller.CurrentSpeed <= Controller.MaximumSpeed)
        {
            Controller.ChangeState(Factory.AcceleratingState);
        }
    }

    public override void UpdatePhysics()
    {
    }
}
