using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipState : State
{
    private ShipController _controller;

    private PlayerControls _playerControls;

    public ShipController Controller { get { return _controller; } }
    public PlayerControls PlayerControls { get { return _playerControls; } }

    public ShipState(ShipController controller)
    {
        _controller = controller;
        _playerControls = _controller.PlayerControls;
    }

    public virtual void Boost()
    {

    }
}
