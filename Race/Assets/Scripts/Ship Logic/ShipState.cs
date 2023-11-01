using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipState : State
{
    private ShipController _controller;
    private ShipStateFactory _factory;

    private PlayerControls _playerControls;

    public ShipController Controller { get { return _controller; } }
    public ShipStateFactory Factory { get { return _factory; } }
    public PlayerControls PlayerControls { get { return _playerControls; } }

    public ShipState(ShipController controller, ShipStateFactory factory)
    {
        _controller = controller;
        _factory = factory;
        _playerControls = controller.PlayerControls;
    }

    public virtual void Boost()
    {

    }
}
