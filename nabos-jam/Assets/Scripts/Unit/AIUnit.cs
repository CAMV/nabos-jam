using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/// <summary>
/// Models a unit in the game, linking the unit to its different components.
/// </summary>
public class AIUnit : Unit
{
    private bool _isIdle = true;
    public bool IsIdle {
        get 
        {
            if (_isIdle) return _isIdle;    //Returns automatically if already idle

            _isIdle = true;
            //Movement check
            _isIdle = _isIdle && !(_movement && _movement.isMoving());
            
            return _isIdle;
        }
        set 
        {
            _isIdle = value;
        }
    }
}