using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStunState : ShipState
{
    private Vector3 _collisionNormal;
    private float _stunTimer;
    private float _bounceForce;

    public ShipStunState(ShipController controller, ShipStateFactory factory, Vector3 collisionNormal) : base(controller, factory)
    {
        _collisionNormal = collisionNormal;
    }

    public override void Enter()
    {
        _bounceForce = Controller.CurrentSpeed / 250f;
        Controller.CurrentSpeed = 0f;
        Controller.Rb.velocity = Vector3.zero;
        Controller.Rb.AddForce(_collisionNormal * _bounceForce, ForceMode.Impulse);
        _stunTimer = 0f;
    }

    public override void Exit()
    {
    }

    public override void OnCollisionEnter(Collision collision)
    {
    }

    public override void UpdateLogic()
    {
        if (_stunTimer < Controller.StunTime)
        {
            _stunTimer += Time.deltaTime;
        } else
        {
            Controller.ChangeState(Factory.IdleState);
        }
    }

    public override void UpdatePhysics()
    {
    }
}
