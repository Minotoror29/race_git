using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateFactory
{
    private ShipState _idleState;
    private ShipState _acceleratingState;
    private List<ShipState> _boostStates;

    public ShipState IdleState { get { return _idleState; } }
    public ShipState AcceleratingState { get { return _acceleratingState; } }
    public List<ShipState> BoostStates { get { return _boostStates; } }

    public ShipStateFactory(ShipController controller)
    {
        _idleState = new ShipIdleState(controller, this);
        _acceleratingState = new ShipAcceleratingState(controller, this);
        _boostStates = new List<ShipState> {
            new ShipBoostState(controller, this, controller.Boost1Speed, controller.Boost1Cam, 0),
            new ShipBoostState(controller, this, controller.Boost2Speed, controller.Boost2Cam, 1),
            new ShipBoostState(controller, this, controller.Boost3Speed, controller.Boost3Cam, 2)
        };
    }
}
