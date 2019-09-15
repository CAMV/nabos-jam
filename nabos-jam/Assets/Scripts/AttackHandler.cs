using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles attack interactions
/// </summary>
public class AttackHandler : MonoBehaviour
{
    public Character character;
    public bool isAttacking = false;
    private bool _isChasing = false;
    
    private void FixedUpdate() 
    {
        //Reduce character cooldowns
        float tick = Time.fixedDeltaTime;
        character.TickDurations(tick);
    }

    public void Attack(Unit _enemyUnit)
    {
        Unit _myUnit = GetComponent<Unit>();
        float attackCd = character.attackCooldown;
        //Checks if unit hasn't died
        if (_enemyUnit != null) 
        {
            if (character.attackRange > Vector3.Distance(transform.position, _enemyUnit.transform.position))
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
            GameManager.Instance.PlayerSquad.AddCommand(new AttackCmd(_myUnit, _enemyUnit));
        }
    }
}
