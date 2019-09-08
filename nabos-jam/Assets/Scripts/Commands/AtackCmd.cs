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
        float attackCd = _myUnit.character.attackCooldown;
        //Checks if unit hasn't died
        if (_enemyUnit != null) 
        {
            //Checks if attack cooldown is ready
            if (attackCd <= 0)
            {
                _myUnit.character.PerformAttack(_enemyUnit.character);
                //Reset attack delay
                _myUnit.character.ResetAttackCd();

                //Unit died, destroy it
                if (_enemyUnit.character.health.currentHealth <= 0) 
                {
                    Object.Destroy(_enemyUnit.gameObject);
                }
                else
                {
                    Requeue();
                }
            }
            else
            {
                Requeue();
            }

        }
    }

    /* Add the attack command to the queue again */
    public void Requeue()
    {
        GameManager.Instance.PlayerSquad.AddCommand(new AttackCmd(_myUnit, _enemyUnit));
    }

    override public void Undo()
    {
        
    }       
}