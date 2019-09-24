using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/// <summary>
/// <c> Command </c> that moves a single unit to a given destination
/// </summary>
public class MoveCmd : Command
{
    private Vector3 _myTarget;
    private Unit _myUnit;
    private Quaternion _myRotation;
    private bool _hasRotation;
    // Saves position for the Undo method
    private Vector3 _lastPosition;

    /// <summary>
    /// Constructs a move command given a unit, a position in world space and a rotation in world space.
    /// </summary>
    /// <param name="unit">Unit to move</param>
    /// <param name="targetPos">Target position</param>
    /// <param name="direction">Target rotation</param>
    public MoveCmd(Unit unit, Vector3 targetPos, Quaternion direction)
    {
        this._myTarget = targetPos;
        this._myUnit = unit;
        this._hasRotation = true;
        this._myRotation = direction;
    }

    /// <summary>
    /// Constructs a move command given a unit and a position in world space.
    /// </summary>
    /// <param name="unit">Unit to move</param>
    /// <param name="targetPos">Target position</param>
    public MoveCmd(Unit unit, Vector3 targetPos)
    {
        this._myTarget = targetPos;
        this._myUnit = unit;
        this._hasRotation = false;
    }

    /// <summary>
    /// Executes the movement of the command's unit by calling the unit's movement component function.
    /// </summary>
    override public void Do()
    {
        _lastPosition = _myUnit.transform.position;

        if (!_myUnit.Movements)
            Debug.Log(_myUnit.name + " does not have a Movement component!");
        else if (_hasRotation)
            _myUnit.Movements.Move(_myTarget, _myRotation);
        else
            _myUnit.Movements.Move(_myTarget);
            
    }

    /// <summary>
    /// Undo any movement done by this command. DOES NOTHING!!!
    /// </summary>
    override public void Undo()
    {
        
    }       
}