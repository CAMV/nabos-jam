using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/Target Far Away")]
public class TargetFarAwayCondition : Condition
{
    private Unit target;

    public override void Initialize(Unit unit)
    {
        base.Initialize(unit);
        // AttackHandler ah = _unit.GetComponent<AttackHandler>();
        // if (ah) 
        // {
        //     target = ah.targetUnit;
        // }
    }

    public override bool IsTriggered()
    {
        if (_unit)
        {
            if (!target)
            {
                // AttackHandler ah = _unit.GetComponent<AttackHandler>();
                // if (ah) 
                // {
                //     target = ah.targetUnit;
                // }
            }
            else
            {
                // bool isFarAway = Vector3.Distance(target.transform.position, _unit.transform.position) > _unit.Character.attackRange;
                // if (negateCondition)
                // {
                //     isFarAway = !isFarAway;
                // }
                // return isFarAway;
            }
        }
        return false;
    }
}
