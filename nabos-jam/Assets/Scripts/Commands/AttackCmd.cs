using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AttackCmd : Command
{
    private Unit _myUnit;
    private Unit _enemyUnit;
    private bool _isChasing = false;
    

    public AttackCmd(Unit unit, Unit enemyUnit)
    {
        this._myUnit = unit;
        this._enemyUnit = enemyUnit;
    }

    public AttackCmd(Unit unit, Unit enemyUnit, bool isChasing)
    {
        this._myUnit = unit;
        this._enemyUnit = enemyUnit;
        _isChasing = isChasing;
    }


    override public void Do()
    {
        float attackCd = _myUnit.Character.attackCooldown;
        //Checks if unit hasn't died
        if (_enemyUnit != null) 
        {
            if (_myUnit.Character.attackRange > Vector3.Distance(_myUnit.transform.position, _enemyUnit.transform.position))
            {
                //Stops moving once withing range
                if (_isChasing && _myUnit.Movement)
                {
                    _isChasing = false;
                    _myUnit.Movement.StopMoving();
                }
                //Checks if attack cooldown is ready
                if (attackCd <= 0)
                {
                    _myUnit.Character.PerformAttack(_enemyUnit.Character);
                    //Reset attack delay
                    _myUnit.Character.ResetAttackCd();

                    //Unit died, destroy it
                    if (_enemyUnit.Character.health.currentHealth <= 0) 
                    {
                        Object.Destroy(_enemyUnit.gameObject);
                        _myUnit.GetComponent<AttackHandler>().isAttacking = false;
                    }
                }

            }
            else 
            {
                if (_myUnit.Movement && !_isChasing)
                    _isChasing = true;
                    _myUnit.Movement.Move(_enemyUnit.transform.position);
            }

        }
        if (_myUnit.GetComponent<AttackHandler>().isAttacking)
        {
            Requeue();
        }
    }

    /// <summary>
    /// Readd the attack to the attack queue
    /// </summary>
    public void Requeue()
    {
        GameManager.Instance.PlayerSquad.AddCommand(new AttackCmd(_myUnit, _enemyUnit, _isChasing));
    }

    override public void Undo()
    {
        
    }       
}