using UnityEngine;
using UnityEngine.AI;

public class MoveCmd : Command
{
    private Vector3 _myTarget;
    private Unit _myUnit;
    
    private Vector3 _lastPosition;

    public MoveCmd(Unit unit, Vector3 targetPos)
    {
        this._myTarget = targetPos;
        this._myUnit = unit;
    }

    override public void Do()
    {
        _lastPosition = _myUnit.transform.position;
        _myUnit.Agent.destination = _myTarget;
    }


    override public void Undo()
    {
        
    }       
}