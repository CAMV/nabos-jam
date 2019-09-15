using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AttackCmd : Command
{
    private Unit _myUnit;
    private Unit _enemyUnit;
    

    public AttackCmd(Unit unit, Unit enemyUnit)
    {
        this._myUnit = unit;
        this._enemyUnit = enemyUnit;
    }


    override public void Do()
    {
        _myUnit.GetComponent<AttackHandler>().SetAttackTarget(_enemyUnit);
    }

    /// <summary>
    /// Readd the attack to the attack queue
    /// </summary>
    public void Requeue()
    {
    }

    override public void Undo()
    {
        
    }       
}