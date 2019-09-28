using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/Has Target Enemy")]
public class HasTargetEnemyCondition : Condition
{
    public override bool IsTriggered()
    {
        if (_unit)
        {
            // AttackHandler ah = _unit.GetComponent<AttackHandler>();
            // return (ah && ah.isAttacking);
        }
        return false;
    }
}
