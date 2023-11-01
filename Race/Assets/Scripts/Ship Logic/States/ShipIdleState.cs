using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIdleState : ShipState
{
    public ShipIdleState(ShipController controller) : base(controller)
    {

    }

    public override void Enter()
    {
        Controller.CurrentSpeed = 0f;
        Controller.CameraManager.CameraTransition(Controller.IdleCam);
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (PlayerControls.InGame.Accelerate.ReadValue<float>() > 0)
        {
            Controller.ChangeState(new ShipAcceleratingState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
    }
}
