using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MoveCmd : Command
{
    private Vector3 _myTarget;
    private Unit _myUnit;
    private Quaternion _myRotation;
    private bool _hasRotation;
    
    private Vector3 _lastPosition;

    public MoveCmd(Unit unit, Vector3 targetPos, Quaternion direction)
    {
        this._myTarget = targetPos;
        this._myUnit = unit;
        this._hasRotation = true;
        this._myRotation = direction;
    }

    public MoveCmd(Unit unit, Vector3 targetPos)
    {
        this._myTarget = targetPos;
        this._myUnit = unit;
        this._hasRotation = false;
    }

    override public void Do()
    {
        _lastPosition = _myUnit.transform.position;

        if (!_myUnit.Movement)
            Debug.Log(_myUnit.name + " can't Move!");
        else if (_hasRotation)
            _myUnit.Movement.Move(_myTarget, _myRotation);
        else
            _myUnit.Movement.Move(_myTarget);
            
    }

    override public void Undo()
    {
        
    }       
}