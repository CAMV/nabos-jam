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
    public Unit targetUnit {get; private set;}
    private bool _isChasing = false;
    [SerializeField]
    private SphereCollider losCollider;
    [SerializeField]
    public int aggresiveLayer;    //The layer this Unit will be aggressive towards   

    private void Start()
    {
        if (losCollider)
        {
            losCollider.radius = character.lineOfSight;
        }
    }


    private void FixedUpdate() 
    {
        //Reduce character cooldowns
        float tick = Time.fixedDeltaTime;
        character.TickDurations(tick);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() && other.gameObject.layer == aggresiveLayer)
        {
            SetAttackTarget(other.GetComponent<Unit>());
        }
    }

    public void SetAttackTarget(Unit target)
    {
        targetUnit = target;
        isAttacking = true;
    }

    /// <summary>
    /// Attacks a target enemy unity
    /// </summary>
    /// <param name="_enemyUnit"></param>
    public void Attack(Unit _enemyUnit)
    {
        Unit _myUnit = GetComponent<Unit>();
        float attackCd = character.attackCooldown;
        //Checks if unit hasn't died and attack cd is ready
        if (_enemyUnit != null && attackCd <= 0) 
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

    public void StopAttacking() 
    {
        isAttacking = false;
        targetUnit = null;
    }

}
